import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    MatchesClient,
    SeasonsClient,
    IUnfinishedSeasonsVm,
    IUnfinishedSeasonDto,
    CreateMatchCommand,
    IPlayoffWeeksVm,
    IPlayoffWeeksDto,
    WeeksClient,
    ITeamsDto,
    TeamsClient,
    ITeamsVm,
    MapsClient,
    IMapsVm,
    IMapsDto
} from '../../../WorldDoomLeague';
import DateTimePicker from 'react-datetime-picker';
import { setErrorMessage } from '../../../state';

const CreatePlayoffGame = props => {
    const [loading, setLoading] = useState(true);
    const [useHomefields, setUseHomefields] = useState(false);
    const [allowHomefields, setAllowHomefields] = useState(false);
    const [season, setSeason] = useState(0);
    const [week, setWeek] = useState(0);
    const [blueTeam, setBlueTeam] = useState(0);
    const [redTeam, setRedTeam] = useState(0);
    const [newGame, setNewGame] = useState(0);
    const [date, setDate] = useState<Date>(null);
    const [mapIds, setMapIds] = useState<number[]>([]);
    const [mapId, setMapId] = useState(0);
    const [gameMapId, setGameMapId] = useState(0);

    const [seasonsData, setSeasonsData] = useState<IUnfinishedSeasonDto[]>([]);
    const [weeksData, setWeeksData] = useState<IPlayoffWeeksDto[]>([]);
    const [mapsData, setMapsData] = useState([]);
    const [gameMapsData, setGameMapsData] = useState([]);
    const [teamsData, setTeamsData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.getUnfinishedSeasons()
                    .then(response => response.toJSON() as Promise<IUnfinishedSeasonsVm>);
                const data = response.seasonList;
                setSeasonsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MapsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<IMapsVm>);
                const data = response.mapList;
                setMapsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [newGame]);

    const createPlayoffWeeks = async () => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new WeeksClient();
                const response = await client.getPlayoffWeeks(season)
                    .then(response => response.toJSON() as Promise<IPlayoffWeeksVm>);
                const data = response.weekList;
                setWeeksData(data);
                await getTeams();
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const getTeams = async () => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new TeamsClient();
                const response = await client.getTeamsBySeasonId(season)
                    .then(response => response.toJSON() as Promise<ITeamsVm>);
                const data = response.teamList as [];
                setTeamsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const createGame = async () => {
        try {
            let client = new MatchesClient();
            const command = new CreateMatchCommand;
            command.season = season;
            command.week = week;
            command.gameDateTime = date;
            command.blueTeam = blueTeam;
            command.redTeam = redTeam;
            command.gameMapIds = mapIds;
            const response = await client.create(command);

            setSeason(0);
            setWeek(0);
            setRedTeam(0);
            setBlueTeam(0);
            setMapId(0);
            setGameMapId(0);
            setMapIds([]);
            setGameMapsData([]);
            setUseHomefields(false);
            setAllowHomefields(false);
            setNewGame(response);
            setWeeksData(null);
            setDate(null);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const addMap = async (evt) => {
        var gameMaps = mapIds;
        var newGameMapsData = gameMapsData;
        var newMapsData = mapsData;

        // disable the map that was selected.
        const disabledMap = newMapsData.map((s, _idx) => {
            if (s.id !== mapId) return s;
            return { ...s, isdisabled: true };
        });

        newMapsData = disabledMap;

        // add the map that was selected to the game maps list.
        const newGameMap = newGameMapsData.concat({
            id: mapsData.find(o => o.id == mapId).id,
            mapName: mapsData.find(o => o.id == mapId).mapName,
            mapPack: mapsData.find(o => o.id == mapId).mapPack,
            mapNumber: mapsData.find(o => o.id == mapId).mapNumber
        });

        newGameMapsData = newGameMap;

        // add the map that was selected to the game maps list.
        const newGameMapIds = gameMaps.concat(mapId);

        gameMaps = newGameMapIds;

        setMapIds(gameMaps);
        setGameMapsData(newGameMapsData);
        setMapsData(newMapsData);

        setMapId(0);
        setGameMapId(0);
    };

    const removeMap = async (evt) => {
        var gameMaps = mapIds;
        var newGameMapsData = gameMapsData;
        var newMapsData = mapsData;

        // re-enable the map that was selected.
        const disabledMap = newMapsData.map((s, _idx) => {
            if (s.id !== gameMapId) return s;
            return { ...s, isdisabled: false };
        });

        newMapsData = disabledMap;

        // remove the map that was selected to the game maps list.
        const newGameMap = newGameMapsData.filter((gameMap, idx) => gameMap.id !== gameMapId);

        newGameMapsData = newGameMap;

        // remove the map that was selected to the game maps list.
        const newGameMapIds = gameMaps.filter((num, idx) => num !== gameMapId);

        gameMaps = newGameMapIds;

        setMapIds(gameMaps);
        setGameMapsData(newGameMapsData);
        setMapsData(newMapsData);

        setMapId(0);
        setGameMapId(0);
    };

    const handleRedTeamSelected = (teamId: number) => {
        var newTeams = teamsData;
        // re-enable a team if it is deselected.
        if (redTeam !== 0) {
            const reenabledOldTeam = teamsData.map((s, _idx) => {
                if (s.id !== redTeam) return s;
                return { ...s, isdisabled: false };
            });

            newTeams = reenabledOldTeam;
        }
        // disable the team that was selected.
        const disabledTeam = newTeams.map((s, _idx) => {
            if (s.id !== teamId) return s;
            return { ...s, isdisabled: true };
        });

        if (teamsData.find(o => o.id === teamId).homeFieldMapId !== null) {
            if (teamsData.find(o => o.id === blueTeam).homeFieldMapId !== null) {
                setAllowHomefields(true);
            } else {
                setAllowHomefields(false);
            }
        } else {
            setAllowHomefields(false);
        }

        newTeams = disabledTeam;

        setTeamsData(newTeams);

        setRedTeam(teamId);
    };

    const handleBlueTeamSelected = (teamId: number) => {
        var newTeams = teamsData;
        // re-enable a team if it is deselected.
        if (blueTeam !== 0) {
            const reenabledOldTeam = teamsData.map((s, _idx) => {
                if (s.id !== blueTeam) return s;
                return { ...s, isdisabled: false };
            });

            newTeams = reenabledOldTeam;
        }
        // disable the team that was selected.
        const disabledTeam = newTeams.map((s, _idx) => {
            if (s.id !== teamId) return s;
            return { ...s, isdisabled: true };
        });

        if (teamsData.find(o => o.id === teamId).homeFieldMapId !== null) {
            if (teamsData.find(o => o.id === redTeam).homeFieldMapId !== null) {
                setAllowHomefields(true);
            } else {
                setAllowHomefields(false);
            }
        } else {
            setAllowHomefields(false);
        }


        newTeams = disabledTeam;

        setTeamsData(newTeams);

        setBlueTeam(teamId);
    };

    const handleUseHomefieldsChecked = (value: boolean) => {
        var gameMaps = [] as number[];

        if (value === true) {
            gameMaps.push(teamsData.find(o => o.id == redTeam).homeFieldMapId);
            gameMaps.push(teamsData.find(o => o.id == blueTeam).homeFieldMapId);
            setUseHomefields(true);
        }
        else {
            setUseHomefields(false);
            setGameMapsData([]);
        }

        setMapIds(gameMaps);
    };

    // create a list item for each playoff week
    const renderWeeksList = () => {
        let select = null;

        if (weeksData && weeksData.length > 0) {
            select = (
                    <Select
                        options={weeksData}
                        onChange={e => setWeek(e.id)}
                        isSearchable={true}
                        getOptionValue={value => value.id.toString()}
                        getOptionLabel={label => `Week #${label.weekNumber} - Week Type: ${label.weekType}`}
                        isLoading={loading}
                        />);
        } else {
            select = (<Select options={[{ label: "No weeks!", value: "0" }]} />);
        }

        return (select);
    };

    // create a list item for each season
    const renderSeasonList = () => {
        let select = null;
        if (seasonsData && seasonsData.length > 0) {
            select = (
                <Select
                    options={seasonsData}
                    onChange={e => setSeason(e.id)}
                    isSearchable={true}
                    value={seasonsData.find(o => o.id == season) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => label.seasonName}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No seasons!", value: "0" }]} />);
        }

        return (select);
    };

    // create a list for each team, and the date select.
    const renderTeamsList = () => {
        let select = null;
        select = (
                <div>
                    <Row>
                    <Col xs="6">
                        <h2 className={"text-center text-danger"}>Red Team</h2>
                        <br />
                        <Select
                        options={teamsData}
                        onChange={e => handleRedTeamSelected(e.id)}
                        isSearchable={true}
                        getOptionValue={value => value.id.toString()}
                        getOptionLabel={label => `${label.teamName} - Homefield: ${label.homeFieldMapName}`}
                        isOptionDisabled={d => d.isdisabled}
                            isLoading={loading}
                        />
                        </Col>
                    <Col xs="6">
                        <h2 className={"text-center text-primary"}>Blue Team</h2>
                        <br />
                        <Select
                            options={teamsData}
                            onChange={e => handleBlueTeamSelected(e.id)}
                            isSearchable={true}
                            getOptionValue={value => value.id.toString()}
                            getOptionLabel={label => `${label.teamName} - Homefield: ${label.homeFieldMapName}`}
                            isOptionDisabled={d => d.isdisabled}
                            isLoading={loading}
                        />
                    </Col>
                </Row>
                <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        <br />
                            <DateTimePicker
                                id='gameDate'
                                name='signupDates'
                                onChange={e => setDate(e)}
                                value={typeof date === "string" ? new Date(date) : date} />
                        <br />
                        <br />
                    </Col>
                </Row>
                <Row>
                    <Col sm="12" md={{ size: 6, offset: 3 }}>
                        <p>If the Use Homefield Maps checkmark is disabled, its because the teams selected do not have homefield associated with them.</p>
                        <p>Make homefield selections for these teams and come back to this, or manually set the maps that will be played during the game.</p>
                        <FormGroup check>
                            <Label check>
                                <Input type="checkbox" onChange={e => handleUseHomefieldsChecked(e.target.checked)} disabled={!allowHomefields} />
                                Use Homefield Maps
                            </Label>
                        </FormGroup>
                        {useHomefields == false && (
                        <div>
                        <Label for="mapSelect">Add at least 1 game map to this game.</Label>
                        <Select
                            options={mapsData}
                            onChange={e => setMapId(e.id)}
                            isSearchable={true}
                            value={mapsData.find(o => o.id == mapId) || null}
                            getOptionValue={value => value.id.toString()}
                            getOptionLabel={label => label.mapName}
                            isOptionDisabled={d => d.isdisabled}
                            isLoading={loading}
                        />
                        <br />
                        <Button color="primary" size="lg" block disabled={!(mapId > 0)} onClick={addMap}>Add Map to Game</Button>
                        <br />
                        <Label for="mapSelect">Game Maps</Label>
                        <Select
                            options={gameMapsData}
                            onChange={e => setGameMapId(e.id)}
                            isSearchable={true}
                            value={gameMapsData.find(o => o.id == gameMapId) || null}
                            getOptionValue={value => value.id.toString()}
                            getOptionLabel={label => label.mapName}
                            isLoading={loading}
                                />
                                <br />
                                <Button color="primary" size="lg" block disabled={!(gameMapId > 0)} onClick={removeMap}>Remove Map from Game</Button>
                                <br />
                            </div>
                        )}
                    </Col>
                </Row>
                </div>
            );

        return (select);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Create Playoff Game</h3>
                    <p>Please select a season to create a playoff game.</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={createPlayoffWeeks}>Select Season</Button>
                    <br />
                    {newGame > 0 && (
                        <h2>Game #{newGame} has been created!</h2>
                    )}
                    {weeksData && weeksData.length > 0 && (
                        renderWeeksList()
                    )}
                    <br />
                </Col>
            </Row>
            {week > 0 && (
                renderTeamsList()
            )}
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!(blueTeam > 0) || !(redTeam > 0) || !(mapIds.length > 0) || loading} onClick={createGame}>Create Playoff Game</Button>
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default CreatePlayoffGame