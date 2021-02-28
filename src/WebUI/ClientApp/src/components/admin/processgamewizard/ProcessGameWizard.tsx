import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    FilesClient,
    IPlayerLineupDto,
    IPlayerLineupVm,
    IRoundObject,
    ISeasonDto,
    ISeasonsVm,
    IUnplayedGamesDto,
    IUnplayedGamesVm,
    IGameMapsDto,
    IRound,
    MatchesClient,
    ProcessMatchCommand,
    RoundObject,
    SeasonsClient,
    TeamsClient,
    IGameMapsVm
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const ProcessGameWizard = props => {
    const [loading, setLoading] = useState(true);
    const [season, setSeason] = useState(0);
    const [game, setGame] = useState(0);
    const [processedGame, setProcessedGame] = useState(0);
    const [numRounds, setNumRounds] = useState(1);
    const [flipTeams, setFlipTeams] = useState(false);
    const [canProcessGame, setCanProcessGame] = useState(false);
    const [completedGame, setCompletedGame] = useState(false);
    const [canSelectSeason, setCanSelectSeason] = useState(true);
    const [canSelectGame, setCanSelectGame] = useState(false);
    const [canCreateRounds, setCanCreateRounds] = useState(false);

    const [seasonData, setSeasonData] = useState<ISeasonDto[]>();
    const [rounds, setRounds] = useState<IRoundObject[]>();
    const [roundData, setRoundData] = useState<IRound[]>();
    const [maps, setMaps] = useState<IGameMapsDto[]>();
    const [unplayedGames, setUnplayedGames] = useState<IUnplayedGamesDto[]>();
    const [playerLineup, setPlayerLineup] = useState<IPlayerLineupDto>();
    const [roundFiles, setRoundFiles] = useState([{
        fileName: "",
        isdisabled: false
    }]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<ISeasonsVm>);
                const data = response.seasonList;
                setSeasonData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    const pad_array = (arr, len, fill) => {
        return arr.concat(Array(len).fill(fill)).slice(0, len);
    };

    const getRound = async (roundFile: string) => {
        const fetchData = async (roundFile: string) => {
            setLoading(true);
            try {
                let client = new FilesClient();
                const response = await client.getRoundObject(roundFile)
                    .then(response => response.toJSON() as Promise<IRound>);
                const roundData = response;
                setLoading(false);
                return roundData;
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
        };

        return fetchData(roundFile);
    };

    const processGame = async (evt) => {
        try {
            let client = new MatchesClient();
            const command = new ProcessMatchCommand;
            // Need to do this to get around an NSwag generator bug.
            // https://github.com/RicoSuter/NSwag/issues/2862
            let gameRounds = [] as RoundObject[];
            for (var idx = 0; idx < rounds.length; idx++) {
                var round = new RoundObject();
                round.roundFileName = rounds[idx].roundFileName;
                round.map = rounds[idx].map;
                round.redTeamPlayerIds = [] as number[];
                round.blueTeamPlayerIds = [] as number[];

                rounds[idx].redTeamPlayerIds.map((s, _sidx) => {
                    round.redTeamPlayerIds.push(s);
                });

                rounds[idx].blueTeamPlayerIds.map((s, _sidx) => {
                    round.blueTeamPlayerIds.push(s);
                });

                gameRounds.push(round);
            }

            command.flipTeams = flipTeams;
            command.matchId = game;
            command.gameRounds = gameRounds;
            const response = await client.process(command);
            setCanProcessGame(false);
            setCanSelectGame(false);
            setCanSelectSeason(true);
            setCanCreateRounds(false);
            setRounds(null);
            setRoundData(null);
            setRoundFiles([{fileName:"", isdisabled: false}]);
            setMaps(null);
            setFlipTeams(false);
            setGame(0);
            setProcessedGame(response);
            setSeason(0);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const selectSeason = async (evt) => {
        try {
            let client = new MatchesClient();
            const response = await client.getUnplayedGames(season)
                .then(response => response.toJSON() as Promise<IUnplayedGamesVm>);
            setUnplayedGames(response.unplayedGameList);
            setCanSelectSeason(false);
            setCanSelectGame(true);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const selectGame = async (evt) => {
        try {
            let matchClient = new MatchesClient();
            let fileClient = new FilesClient();
            const matchResponse = await matchClient.getPlayerLineup(game)
                .then(response => response.toJSON() as Promise<IPlayerLineupVm>);
            const fileResponse = await fileClient.getRoundJsonFiles();
            const mapResponse = await matchClient.getGameMaps(game)
                .then(response => response.toJSON() as Promise<IGameMapsVm>);
            const fileNames = fileResponse.map((s, _idx) => {
                return { fileName: s, isdisabled: false };
            });

            const newRound = pad_array([] as IRoundObject[], 1,
            {
                redTeamPlayerIds: [] as number[],
                blueTeamPlayerIds: [] as number[],
                map: null,
                roundFileName: ""
                });

            setRoundFiles(fileNames);
            setPlayerLineup(matchResponse.gamePlayerLineup);
            setMaps(mapResponse.gameMaps);
            setRounds(newRound);

            setCanSelectGame(false);
            setCanCreateRounds(true);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const checkIfFormComplete = (rounds: IRoundObject[]) => {
        if (rounds.every(element => element.map && element.roundFileName && element.blueTeamPlayerIds.every(number => number != null) && element.blueTeamPlayerIds.every(number => number != null))) {
            setCanProcessGame(true);
        } else {
            setCanProcessGame(false);
        }
    };

    const handleMapChange = (roundIndex, value) => {
        const newRounds = rounds.map((round, _idx) => {
            if (_idx !== roundIndex) return round;
            return { ...round, map: value }
        });

        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };

    const handleRoundFileSelected = async (roundIndex, roundFile) => {
        // re-enable a round if it is deselected.
        var roundFileNames = roundFiles;
        if (rounds[roundIndex].roundFileName !== "") {
            const reenabledOldRound = roundFileNames.map((s, _idx) => {
                if (s.fileName !== rounds[roundIndex].roundFileName) return s;
                return { ...s, isdisabled: false };
            });

            roundFileNames = reenabledOldRound;
        }
        // disable the round that was selected.
        const disabledRound = roundFileNames.map((s, _idx) => {
            if (s.fileName !== roundFile) return s;
            return { ...s, isdisabled: true };
        });

        setRoundFiles(disabledRound);

        const round = await getRound(roundFile);
        var newRoundData = null;

        if (roundData) {
            if (roundData[roundIndex]) {
                newRoundData = roundData.map((gameRound, idx) => {
                    if (idx !== roundIndex) return gameRound;
                    return round;
                });
            }
            else
            {
                newRoundData = roundData.concat(round);
            }
        } else {
            var newData = [] as IRound[];
            newRoundData = newData.concat(round);
        }

        setRoundData(newRoundData);

        const newRounds = rounds.map((round, _idx) => {
            if (_idx !== roundIndex) return round;

            return {
                ...round,
                roundFileName: roundFile,
                redTeamPlayerIds: pad_array([] as number[], flipTeams ? newRoundData[_idx].blueTeamStats.teamPlayers.length : newRoundData[_idx].redTeamStats.teamPlayers.length, null),
                blueTeamPlayerIds: pad_array([] as number[], flipTeams ? newRoundData[_idx].redTeamStats.teamPlayers.length : newRoundData[_idx].blueTeamStats.teamPlayers.length, null)
            }
        });

        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };

    const handleFlipTeamsChecked = (value: boolean) => {
        const newRounds = rounds.map((round, _idx) => {
            if (roundData && roundData[_idx]) {
                return {
                    ...round,
                    redTeamPlayerIds: pad_array([] as number[], value ? roundData[_idx].blueTeamStats.teamPlayers.length : roundData[_idx].redTeamStats.teamPlayers.length, null),
                    blueTeamPlayerIds: pad_array([] as number[], value ? roundData[_idx].redTeamStats.teamPlayers.length : roundData[_idx].blueTeamStats.teamPlayers.length, null)
                }
            } else {
                return round;
            }
        });

        setRounds(newRounds);
        setFlipTeams(value);
        checkIfFormComplete(newRounds);
    };

    const handleRedPlayerSelected = (roundIndex: number, playerIndex: number, value: number) => {
        const newRounds = rounds.map((round, idx) => {
            if (idx !== roundIndex) { return round }
            const redIds = round.redTeamPlayerIds.map((id, _idx) => {
                if (playerIndex !== _idx) { return id; }
                return value;
            });
            return { ...round, redTeamPlayerIds: redIds };
        });

        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };

    const handleBluePlayerSelected = (roundIndex: number, playerIndex: number, value: number) => {
        const newRounds = rounds.map((round, idx) => {
            if (idx !== roundIndex) { return round }
            const redIds = round.blueTeamPlayerIds.map((id, _idx) => {
                if (playerIndex !== _idx) { return id; }
                return value;
            });
            return { ...round, blueTeamPlayerIds: redIds };
        });

        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };

    const handleRemoveRound = (roundIndex: number) => {
        if (rounds[roundIndex].roundFileName !== "") {
            const reenabledOldRound = roundFiles.map((s, _idx) => {
                if (s.fileName !== rounds[roundIndex].roundFileName) return s;
                return { ...s, isdisabled: false };
            });

            setRoundFiles(reenabledOldRound);
        }

        const newRounds = rounds.filter((round, idx) => idx !== roundIndex);

        if (roundData && roundData[roundIndex])
        {
            const newRoundData = roundData.filter((round, idx) => idx !== roundIndex);
            setRoundData(newRoundData);
        }

        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };

    const handleAddRound = () => {
        const newRounds = rounds.concat({
            roundFileName: "",
            map: null,
            blueTeamPlayerIds: [] as number[],
            redTeamPlayerIds: [] as number[]
        });

        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };

    // create a list for each game map.
    const renderFlipTeamsCheckbox = () => {
        return (
            <FormGroup check>
                <Label check>
                    <Input type="checkbox" onChange={e => handleFlipTeamsChecked(e.target.checked)} />{' '}
                    Flip Teams
                </Label>
            </FormGroup>);
    };

    // create a list for each game map.
    const renderMapSelect = (roundIndex) => {
        let select = null;
        if (maps.length > 0) {
            select = (<Select
                options={maps}
                onChange={e => handleMapChange(roundIndex, e.id)}
                isDisabled={completedGame}
                isSearchable={true}
                value={maps.find(o => o.id == rounds[roundIndex].map) || null}
                getOptionValue={value => value.id.toString()}
                getOptionLabel={label => label.mapName + " | " + label.mapPack}
                isLoading={loading}
            />);
        } else {
            select = (<Select options={[{ label: "No maps left!", value: "Not" }]} isDisabled={completedGame} />);
        }
        return (select);
    };

    // create a list for the round files.
    const renderRedPlayerSelection = (roundIndex) => {
        let select = [];
        let players = [] as string[];

        if (roundData && roundData[roundIndex]) {
            if (flipTeams) {
                players = roundData[roundIndex].blueTeamStats.teamPlayers;
            } else {
                players = roundData[roundIndex].redTeamStats.teamPlayers;
            }

            if (playerLineup.redTeamPlayers) {
                players.map((player, idx) => {
                    select = select.concat(
                        <div>
                            <h4>{player}</h4>
                            <Select
                                options={playerLineup.redTeamPlayers}
                                onChange={e => handleRedPlayerSelected(roundIndex, idx, e.id)}
                                isDisabled={completedGame}
                                getOptionValue={value => value.id.toString()}
                                getOptionLabel={label => label.playerName}
                                value={playerLineup.redTeamPlayers.find(o => o.id == rounds[roundIndex].redTeamPlayerIds[idx]) || null}
                                isSearchable={true}
                                isLoading={loading}
                            />
                        </div>);
                });
            } else {
                select.concat(<Select options={[{ label: "No players left!", value: "Not" }]} isDisabled={completedGame} />);
            }
        }

        return (select);
    };

    // create a list for each nominating captain.
    const renderBluePlayerSelection = (roundIndex) => {
        let select = [];
        let players = [] as string[];

        if (roundData && roundData[roundIndex]) {
            if (flipTeams) {
                players = roundData[roundIndex].redTeamStats.teamPlayers;
            } else {
                players = roundData[roundIndex].blueTeamStats.teamPlayers;
            }

            if (playerLineup.blueTeamPlayers) {
                players.map((player, idx) => {
                    select = select.concat(
                        <div>
                            <h4>{player}</h4>
                            <Select
                                options={playerLineup.blueTeamPlayers}
                                onChange={e => handleBluePlayerSelected(roundIndex, idx, e.id)}
                                isDisabled={completedGame}
                                getOptionValue={value => value.id.toString()}
                                getOptionLabel={label => label.playerName}
                                value={playerLineup.blueTeamPlayers.find(o => o.id == rounds[roundIndex].blueTeamPlayerIds[idx]) || null}
                                isSearchable={true}
                                isLoading={loading}
                            />
                        </div>);
                });
            } else {
                select.concat(<Select options={[{ label: "No players left!", value: "Not" }]} isDisabled={completedGame} />);
            }
        }

        return (select);
    };

    // create a list for each nominating captain.
    const renderRoundFileSelect = (roundIndex) => {
        let select = null;
        if (roundFiles) {
            select = (
                <Select
                    options={roundFiles}
                    onChange={e => handleRoundFileSelected(roundIndex, e.fileName)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={completedGame}
                    value={roundFiles.find(o => o.fileName == rounds[roundIndex].roundFileName) || null}
                    getOptionValue={value => value.fileName}
                    getOptionLabel={label => label.fileName}
                    isSearchable={true}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No teams left!", value: "Not" }]} isDisabled={completedGame} />);
        }

        return (select);
    };

    // create a list for each season
    const renderSeasonList = () => {
        let select = null;
        if (seasonData && seasonData.length > 0) {
            select = (
                <Select
                    options={seasonData}
                    onChange={e => setSeason(e.id)}
                    isDisabled={completedGame || !canSelectSeason}
                    isSearchable={true}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => label.seasonName}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No seasons!", value: "Not" }]} isDisabled={completedGame} />);
        }

        return (select);
    };

    const renderRemoveRoundButton = (roundIndex) => {
        return (<Button color="primary" size="lg" block disabled={!(rounds.length > 1) || loading} onClick={e => handleRemoveRound(roundIndex)}>Remove Round #{roundIndex + 1}</Button>);
    };

    const renderAddRoundButton = () => {
        return (<Button color="primary" size="lg" block disabled={!(rounds.every(element => element.roundFileName !== "")) || loading} onClick={e => handleAddRound()}>Add Round</Button>);
    };

    // create a list for each season
    const renderGameList = () => {
        let select = null;
        if (unplayedGames) {
            select = (
                <Select
                    options={unplayedGames}
                    onChange={e => setGame(e.id)}
                    isDisabled={completedGame || !canSelectGame}
                    isSearchable={true}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.redTeamName} vs ${label.blueTeamName} - Week ${label.weekNumber}`}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No games!", value: "Not" }]} isDisabled={completedGame} />);
        }

        return (select);
    };

    // render the game list
    const renderRoundList = () => {
        let gameList = (
            <React.Fragment>
                <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        {renderFlipTeamsCheckbox()}
                        <br />
                    </Col>
                </Row>
                {rounds.map((round, roundIndex) => (
                    <div>
                        <Row>
                            <Col sm="12" md={{ size: 6, offset: 3 }}>
                                <h2 className={"text-center"}>Round #{roundIndex + 1}</h2>
                                <br />
                                <h4 className={"text-center"}>Round File</h4>
                                {renderRoundFileSelect(roundIndex)}
                                <br />
                            </Col>
                        </Row>
                        <Row>
                            <Col xs="6">
                                <div>
                                    <h2 className={flipTeams ? "text-primary text-center" : "text-danger text-center"}>{playerLineup.redTeamName}</h2>
                                    {renderRedPlayerSelection(roundIndex)}
                                    <br />
                                </div>
                                <br />
                            </Col>
                            <Col xs="6">
                                <div>
                                    <h2 className={flipTeams ? "text-danger text-center" : "text-primary text-center"}>{playerLineup.blueTeamName}</h2>
                                    {renderBluePlayerSelection(roundIndex)}
                                    <br />
                                </div>
                                <br />
                            </Col>
                        </Row>
                        <Row>
                            <Col sm="12" md={{ size: 6, offset: 3 }}>
                                <h4 className={"text-center"}>Map</h4>
                                {renderMapSelect(roundIndex)}
                                <br />
                            </Col>
                        </Row>
                        <Row>
                            <Col sm="12" md={{ size: 6, offset: 3 }}>
                                {renderRemoveRoundButton(roundIndex)}
                                <hr />
                            </Col>
                        </Row>
                    </div>
                ))}
                <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        {renderAddRoundButton()}
                        <br />
                    </Col>
                </Row>
            </React.Fragment>);

        return (gameList);
    };

    // render the game list container.
    const renderRoundListContainer = () => {
        let games = (
            <React.Fragment>
            <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        <h4 className="text-center">{playerLineup.redTeamName} vs. {playerLineup.blueTeamName}</h4>
                </Col>
            </Row>
                {renderRoundList()}
                <br />
            </React.Fragment>);
        return (games);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Process Game</h3>
                    <p>Please select the ongoing season where a game needs to be played.</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading || !canSelectSeason} onClick={selectSeason}>Select Season</Button>
                    <hr />
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Label for="gameSelect">Select a Game</Label>
                    {renderGameList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(game > 0) || loading || !canSelectGame} onClick={selectGame}>Select Game</Button>
                    <hr />
                </Col>
            </Row>
            {canCreateRounds && (
                renderRoundListContainer()
            )}
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!canProcessGame} onClick={processGame}>Process Game</Button>
                    <br />
                </Col>
            </Row>
            {processedGame > 0 && ( 
                <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        <h3 className='text-center'>Game #{processedGame} has been processed!</h3>
                    </Col>
                </Row>
            )}
        </React.Fragment>
    );
};

export default ProcessGameWizard