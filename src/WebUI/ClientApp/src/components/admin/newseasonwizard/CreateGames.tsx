import StepButtons from './StepButtons'
import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, Pagination, PaginationLink, PaginationItem, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    ITeamsVm,
    ITeamsDto,
    IWeeklyRequest,
    IRegularSeasonWeeksVm,
    IRegularSeasonWeeksDto,
    MatchesClient,
    WeeksClient,
    CreateMatchesCommand,
    WeeklyRequest,
    NewGame,
    TeamsClient,
    INewGame,
    IMapsVm,
    IMapsDto,
    MapsClient,
    CreateMapCommand,
    MapsDto
} from '../../../WorldDoomLeague';
import { useHistory } from 'react-router-dom';
import { setErrorMessage } from '../../../state';

const CreateGames = props => {
    const [games, setGames] = useState<IWeeklyRequest[]>([{
        weekId: null,
        mapId: null,
        gameList: [] as NewGame[]
    }]);
    const [weeks, setWeeks] = useState<IRegularSeasonWeeksDto[]>([{
        id: null,
        weekNumber: null,
        weekStartDate: null
    }]);
    const [loading, setLoading] = useState(true);
    const [gamesPerWeek, setGamesPerWeek] = useState(1);
    const [currentWeek, setCurrentWeek] = useState(0);
    const [canSubmitGames, setCanSubmitGames] = useState(false);
    const [completedGames, setCompletedGames] = useState(false);
    const [canCreateGames, setCanCreateGames] = useState(true);
    const [data, setData] = useState<IMapsDto[]>([]);
    const [index, setIndex] = useState(0);
    const [mapPack, setMapPack] = useState<string>("");
    const [mapName, setMapName] = useState<string>("");
    const [mapNumber, setMapNumber] = useState(0);
    const [newMapId, setNewMapId] = useState(0);
    let history = useHistory();

    const redirect = () => {
        history.push('/');
    };

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MapsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<IMapsVm>);
                const data = response.mapList;
                setData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [newMapId]);

    const getWeeks = async () => {
        const fetchData = async () => {
                setLoading(true);
                try {
                    let client = new WeeksClient();
                    const response = await client.getRegularSeasonWeeks(props.form.season)
                        .then(response => response.toJSON() as Promise<IRegularSeasonWeeksVm>);
                    const weekData = response.weekList;
                    setLoading(false);
                    return weekData;
                } catch (e) {
                    setErrorMessage(JSON.parse(e.response));
                }
            };

            return fetchData();
    };

    const getTeams = async () => {
        const fetchData = async () => {
                setLoading(true);
                try {
                    let client = new TeamsClient();
                    const response = await client.getTeamsBySeasonId(props.form.season)
                        .then(response => response.toJSON() as Promise<ITeamsVm>);
                    const teamData = response.teamList;
                    setLoading(false);
                    return teamData;
                } catch (e) {
                    setErrorMessage(JSON.parse(e.response));
                }
            };

            return fetchData();
    };

    const submitGames = async (evt) => {
        try {
            let client = new MatchesClient();
            const command = new CreateMatchesCommand;
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

            command.seasonId = props.form.season;
            command.weeklyGames = weekly;
            const response = await client.createRegularSeason(command);
            setCanSubmitGames(false);
            setCompletedGames(true);
        } catch (e) {
            console.log(e);
            console.log(e.response);
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const submitMaps = async (evt) => {
        try {
            let client = new MapsClient();
            const command = new CreateMapCommand;
            command.mapName = mapName;
            command.mapPack = mapPack;
            command.mapNumber = mapNumber;
            const response = await client.create(command);
            setNewMapId(response);
            setMapPack('');
            setMapName('');
            setMapNumber(0);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const createGames = async () => {
        const pad_array = (arr, len, fill) => {
            return arr.concat(Array(len).fill(fill)).slice(0, len);
        };

        setCanCreateGames(false);
        const teams = await getTeams();
        const gameWeeks = await getWeeks();

        if (teams) {
            const maxGamesPerWeek = (teams.length / 2) * gamesPerWeek;

            var newGames = [] as IWeeklyRequest[];
            var newTeams = [];

            for (var i = 0; i < gameWeeks.length; i++) {
                const buildGames = newGames.concat({
                    weekId: gameWeeks[i].id,
                    mapId: null,
                    gameList: pad_array([] as NewGame[], maxGamesPerWeek, {
                        redTeam: null,
                        blueTeam: null,
                        gameDateTime: null
                    })
                });

                newGames = buildGames;
            };

            const newTeamsArray = teams.map((s, _idx) => {
                return { ...s, label: s.teamAbbreviation, value: s.id, gamesLeft: gamesPerWeek, isdisabled: false };
            });

            for (var i = 0; i < gameWeeks.length; i++) {
                var weekTeams = [];
                weekTeams.push(...newTeamsArray);
                newTeams.push(weekTeams);
            };

            setGames(newGames);
            setWeeks(gameWeeks);
            props.update("teams", newTeams);
        }
    };

    const handleMapChange = (weekIndex, value) => {
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

    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    // create a list for each engine.
    const renderMapDropdown = (weekIndex) => {
        let select = null;
        console.log("map dropdown rerender!!");
        if (data.length > 0) {
            let maps = [];

            for (var idx = 0; idx < data.length; idx++) {
                var map = new MapsDto();
                map.id = data[idx].id;
                map.mapName = data[idx].mapName;
                map.mapNumber = data[idx].mapNumber;
                map.mapPack = data[idx].mapPack;
                maps.push(map);
            };

            select = (<Select
                options={maps}
                onChange={e => handleMapChange(weekIndex, e.id)}
                isOptionDisabled={(option) => option.isdisabled}
                isDisabled={completedGames}
                isSearchable={true}
                value={maps.find(o => o.id == games[weekIndex].mapId) || null}
                getOptionValue={value => value.id}
                getOptionLabel={label => label.mapName + " | " + label.mapPack }
                isLoading={loading}
            />);
        } else {
            select = (<Select options={[{ label: "No maps left!", value: "Not" }]} isDisabled={completedGames} />);
        }
        return (select);
    };

    // create a form for entering a new engine.
    const renderNewMapForm = () => {
        return (
            <React.Fragment>
                <FormGroup>
                    <Label for="mapName">Map Name</Label>
                    <Input type="text" name="mapName" id="mapName" value={mapName} placeholder="N's Base of Boppin'" onChange={e => setMapName(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for="mapPack">Pack</Label>
                    <Input type="text" name="mapPack" id="mapPack" value={mapPack} placeholder="32in24-4" onChange={e => setMapPack(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for="engineUrl">Map Number</Label>
                    <Input placeholder="Amount" min={1} max={32} type="number" step="1" value={mapNumber} onChange={e => setMapNumber(parseInt(e.target.value, 10))} />
                </FormGroup>
                <Button color="primary" size="lg" block disabled={!mapName || !mapName || !mapNumber || completedGames} onClick={submitMaps}>Create New Map</Button>
            </React.Fragment>
        );
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

    const renderMapCreate = () => {
        return (
            <React.Fragment>
                <Row>
                    <Col>
                        <Form>
                            {renderNewMapForm()}
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
                    isDisabled={completedGames}
                    value={props.form.teams[weekIndex].find(o => o.id == games[weekIndex].gameList[gameIndex].redTeam) || null}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No teams left!", value: "Not" }]} isDisabled={completedGames} />);
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
                    isDisabled={completedGames}
                    value={props.form.teams[weekIndex].find(o => o.id == games[weekIndex].gameList[gameIndex].blueTeam) || null}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No teams left!", value: "Not" }]} isDisabled={completedGames} />);
        }

        return (select);
    };

    const renderPagination = (weekIndex) => {
        let select = null;
        select = (
            <Row>
                <Col sm="3" md={{ size: 6, offset: 3 }}>
                    <Pagination size="lg" aria-label="Page navigation example">
                        {weeks && weeks.map((week, idx) =>
                            <PaginationItem active={week.weekNumber - 1 == weekIndex}>
                                <PaginationLink key={week.id} onClick={e => setCurrentWeek(week.weekNumber - 1)}>
                                    {week.weekNumber}
                                </PaginationLink>
                            </PaginationItem>
                        )}
                        </Pagination>
                </Col>
            </Row>);
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
    const renderGamesListContainer = (weekIndex) => {
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
                {renderPagination(weekIndex)}
                <br />
                {renderMapCreate()}
                <br />
            </React.Fragment>);
        return (games);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Create Regular Season Games</h3>
                    <p>Please input the games that will be scheduled for each week in the regular season.</p>
                    <Label for="amountTeams">Amount of games per team</Label>
                    <Input placeholder="Amount" name="gamesPerTeam" min={1} max={4} type="number" step="1" value={gamesPerWeek} disabled={true} />
                    <br />
                    <Button color="primary" size="lg" block disabled={!canCreateGames || loading} onClick={createGames}>Create Games</Button>
                    <hr />
                </Col>
            </Row>
            {games && canCreateGames == false && (
                renderGamesListContainer(currentWeek)
            )}
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!canSubmitGames} onClick={submitGames}>Finalize Games</Button>
                </Col>
            </Row>
        <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="secondary" size="lg" block disabled={!completedGames} onClick={redirect}>Finish</Button>
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default CreateGames