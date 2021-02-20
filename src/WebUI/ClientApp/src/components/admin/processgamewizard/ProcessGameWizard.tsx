import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    IRoundObject,
    ISeasonDto,
    ISeasonsVm,
    MatchesClient,
    ProcessMatchCommand,
    SeasonsClient,
    TeamsClient
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const ProcessGameWizard = props => {
    const [loading, setLoading] = useState(true);
    const [season, setSeason] = useState(0);
    const [game, setGame] = useState(0);
    const [flipTeams, setFlipTeams] = useState(false);
    const [canProcessGame, setCanProcessGame] = useState(false);
    const [completedGame, setCompletedGame] = useState(false);
    const [canSelectSeason, setCanSelectSeason] = useState(false);
    const [canSelectGame, setCanSelectGame] = useState(false);
    const [seasonData, setSeasonData] = useState<ISeasonDto[]>();
    const [rounds, setRounds] = useState<IRoundObject[]>();

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

    const processGame = async (evt) => {
        try {
            let client = new MatchesClient();
            const command = new ProcessMatchCommand;
            // Need to do this to get around an NSwag generator bug.
            // https://github.com/RicoSuter/NSwag/issues/2862
            let weekly = [] as WeeklyRequest[];
            for (var idx = 0; idx < games.length; idx++) {
                var addPick = new WeeklyRequest();
                addPick.weekId = games[idx].weekId;
                addPick.mapId = games[idx].mapId;
                addPick.gameList = [] as NewGame[];

                games[idx].gameList.map((s, _sidx) => {
                    var addGame = new NewGame();
                    addGame.blueTeam = s.blueTeam;
                    addGame.redTeam = s.redTeam;
                    addGame.gameDateTime = s.gameDateTime;
                    addPick.gameList.push(addGame);
                });

                weekly.push(addPick);
            }

            command.flipTeams = flipTeams;
            command.matchId = game;
            const response = await client.process(command);
            setCanProcessGame(false);
            setCanSelectGame(false);
        } catch (e) {
            console.log(e);
            console.log(e.response);
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const createGames = () => {
        setCanSelectGame(false);

        const pad_array = (arr, len, fill) => {
            return arr.concat(Array(len).fill(fill)).slice(0, len);
        };

        if (props.form.teams) {
            const maxGamesPerWeek = (props.form.teams.length / 2) * gamesPerWeek;

            var newGames = [] as IWeeklyRequest[];
            var newTeams = [];

            for (var i = 0; i < weeks.length; i++) {
                const buildGames = newGames.concat({
                    weekId: weeks[i].id,
                    mapId: null,
                    gameList: pad_array([] as NewGame[], maxGamesPerWeek, {
                        redTeam: null,
                        blueTeam: null,
                        gameDateTime: null
                    })
                });

                newGames = buildGames;
            };

            const newTeamsArray = props.form.teams.map((s, _idx) => {
                return { ...s, label: s.teamAbbreviation, value: s.id, gamesLeft: gamesPerWeek, isdisabled: false };
            });

            for (var i = 0; i < weeks.length; i++) {
                var weekTeams = [];
                weekTeams.push(...newTeamsArray);
                newTeams.push(weekTeams);
            };

            setGames(newGames);
            props.update("teams", newTeams);
        }
    };

    const handleMapChange = (weekIndex, value) => {
        console.log(value);
        console.log(weekIndex);
        const newWeeks = games.map((game, _idx) => {
            console.log(_idx);
            if (_idx !== weekIndex) return game;
            return { ...game, mapId: value }
        });

        setGames(newWeeks);
        checkIfFormComplete(newWeeks);
    };

    const handleRedTeamSelected = (weekIndex, gameIndex, value) => {
        // subtract the gamesLeft to handle disables
        const newTeamsArray = props.form.teams.map((s, _idx) => {
            if (_idx !== weekIndex) return s;
            return s.map((t, _sidx) => {
                if (t.value !== value.value) return t;
                const left = t.gamesLeft - 1;
                const disable = left <= 0;
                return { ...t, gamesLeft: left, isdisabled: disable };
            });
        });

        // re-enable a captain if they are deselected.
        if (games[weekIndex].gameList[gameIndex].redTeam !== null) {
            const reenabledOldTeam = newTeamsArray.map((s, _idx) => {
                if (_idx !== weekIndex) return s;
                return s.map((t, _sidx) => {
                    if (t.value !== games[weekIndex].gameList[gameIndex].redTeam) return t;
                    const left = t.gamesLeft + 1;
                    const disable = left <= 0;
                    return { ...t, gamesLeft: left, isdisabled: disable };
                });
            });
            props.update("teams", reenabledOldTeam);
        } else {
            const newTeamList = newTeamsArray.map((s, _idx) => {
                if (_idx !== weekIndex) return s;
                return s.map((t, _sidx) => {
                    if (t.value !== value.value) return t;
                    if (t.playersLeft > 0) return { ...t, isdisabled: false };
                    return { ...t, isdisabled: true };
                });
            });
            props.update("teams", newTeamList);
        }

        const newRedTeam = games.map((game, _idx) => {
            if (_idx !== weekIndex) return game;
            const newGameList = game.gameList.map((t, _sidx) => {
                if (_sidx !== gameIndex) return t;
                return { ...t, redTeam: value.value };
            }) as NewGame[];
            return { ...game, gameList: newGameList }
        });

        setGames(newRedTeam);
        checkIfFormComplete(newRedTeam);
    };

    const handleBlueTeamSelected = (weekIndex, gameIndex, value) => {
        // subtract the gamesLeft to handle disables
        const newTeamsArray = props.form.teams.map((s, _idx) => {
            if (_idx !== weekIndex) return s;
            return s.map((t, _sidx) => {
                if (t.value !== value.value) return t;
                const left = t.gamesLeft - 1;
                const disable = left <= 0;
                return { ...t, gamesLeft: left, isdisabled: disable };
            });
        });

        // re-enable a captain if they are deselected.
        if (games[weekIndex].gameList[gameIndex].blueTeam !== null) {
            const reenabledOldTeam = newTeamsArray.map((s, _idx) => {
                if (_idx !== weekIndex) return s;
                return s.map((t, _sidx) => {
                    if (t.value !== games[weekIndex].gameList[gameIndex].blueTeam) return t;
                    const left = t.gamesLeft + 1;
                    const disable = left <= 0;
                    return { ...t, gamesLeft: left, isdisabled: disable };
                });
            });
            props.update("teams", reenabledOldTeam);
        } else {
            const newTeamList = newTeamsArray.map((s, _idx) => {
                if (_idx !== weekIndex) return s;
                return s.map((t, _sidx) => {
                    if (t.value !== value.value) return t;
                    if (t.playersLeft > 0) return { ...t, isdisabled: false };
                    return { ...t, isdisabled: true };
                });
            });
            props.update("teams", newTeamList);
        }

        const newBlueTeam = games.map((game, _idx) => {
            if (_idx !== weekIndex) return game;
            const newGameList = game.gameList.map((t, _sidx) => {
                if (_sidx !== gameIndex) return t;
                return { ...t, blueTeam: value.value };
            }) as NewGame[];
            return { ...game, gameList: newGameList }
        });

        setGames(newBlueTeam);
        checkIfFormComplete(newBlueTeam);
    };

    const checkIfFormComplete = (weeks: IWeeklyRequest[]) => {
        if (weeks.every(element => element.mapId && element.weekId && element.gameList.every(game => game.blueTeam && game.redTeam))) {
            setCanSubmitGames(true);
        } else {
            setCanSubmitGames(false);
        }
    };

    // create a list for each engine.
    const renderMapDropdown = (roundIndex) => {
        let select = null;
        if (data.length > 0) {
            select = (<Select
                options={data}
                onChange={e => handleMapChange(weekIndex, e.id)}
                isOptionDisabled={(option) => option.isdisabled}
                isDisabled={completedGame}
                isSearchable={true}
                getOptionValue={value => value.id}
                getOptionLabel={label => label.mapName + " | " + label.mapPack}
                isLoading={loading}
            />);
        } else {
            select = (<Select options={[{ label: "No maps left!", value: "Not" }]} isDisabled={completedGames} />);
        }
        return (select);
    };

    const renderMapSelect = (weekIndex) => {
        return (
            <React.Fragment>
                <Row>
                    <Col>
                        <Form>
                            <FormGroup>
                                <Label for="engine">Map</Label>
                                    {renderMapDropdown(weekIndex)}
                            </FormGroup>
                        </Form>
                    </Col>
                </Row>
            </React.Fragment>
        );
    };

    // create a list for each nominating captain.
    const renderRedTeamSelection = (weekIndex, gameIndex) => {
        let select = null;
        if (props.form.teams) {
            select = (
                <Select
                    options={props.form.teams[weekIndex]}
                    onChange={e => handleRedTeamSelected(weekIndex, gameIndex, e)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={completedGame}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No teams left!", value: "Not" }]} isDisabled={completedGame} />);
        }

        return (select);
    };

    // create a list for each nominating captain.
    const renderBlueTeamSelection = (weekIndex, gameIndex) => {
        let select = null;
        if (props.form.teams) {
            select = (
                <Select
                    options={props.form.teams[weekIndex]}
                    onChange={e => handleBlueTeamSelected(weekIndex, gameIndex, e)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={completedGame}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No teams left!", value: "Not" }]} isDisabled={completedGame} />);
        }

        return (select);
    };

    // create a list for each nominating captain.
    const renderSeasonList = () => {
        let select = null;
        if (seasonData) {
            select = (
                <Select
                    options={seasonData}
                    onChange={e => setSeason(e)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={completedGame}
                    isSearchable={true}
                    getOptionValue={value => value.id}
                    getOptionLabel={label => label.mapName + " | " + label.mapPack}
                    isLoading={loading}
                />)
        } else {
            select = (<Select options={[{ label: "No seasons!", value: "Not" }]} isDisabled={completedGame} />);
        }

        return (select);
    };

    // render the game list
    const renderGamesList = (weekIndex) => {
        let gameList = (
            <React.Fragment>
                {games[weekIndex] && canCreateGames == false && (games[weekIndex].gameList.map((game, gameIndex) => (
                    <Row>
                        <Col xs="6">
                            <div>Red Team: {renderRedTeamSelection(weekIndex, gameIndex)}</div>
                            <br />
                        </Col>
                        <Col xs="6">
                            <div>Blue Team: {renderBlueTeamSelection(weekIndex, gameIndex)}</div>
                            <br />
                        </Col>
                    </Row>
                )))}
            </React.Fragment>);

        return (gameList);
    };

    // render the game list container.
    const renderGamesListContainer = () => {
        let games = (
            <React.Fragment>
            <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        <h4 className="text-center">Week #{weeks[weekIndex] && weeks[weekIndex].weekNumber}</h4>
                </Col>
            </Row>
                {renderGamesList(weekIndex)}
                <br />
                {renderMapSelect(weekIndex)}
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
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={selectSeason}>Select Season</Button>
                    <hr />
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Label for="gameSelect">Select a Game</Label>
                    {renderGameList()}
                    <Button color="primary" size="lg" block disabled={!canSelectGame} onClick={selectGame}>Select Game</Button>
                </Col>
            </Row>
            {canCreateGame && (
                renderGamesListContainer()
            )}
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!canProcessGame} onClick={processGame}>Process Game</Button>
                </Col>
            </Row>
        <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!completedGame}>Finish</Button>
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default ProcessGameWizard