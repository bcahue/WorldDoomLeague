import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    MatchesClient,
    SeasonsClient,
    ForfeitMatchCommand,
    IUnfinishedSeasonsVm,
    IUnfinishedSeasonDto,
    IRegularSeasonWeeksDto,
    WeeksClient,
    ITeamsDto,
    TeamsClient,
    ITeamsVm,
    ITeamPlayersDto,
    ITeamPlayersVm,
    PlayerTransactionsClient,
    TradePlayerToTeamCommand,
    IRegularSeasonWeeksVm
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const TradePlayerToTeam = props => {
    const [loading, setLoading] = useState(true);
    const [tradeSuccessful, setTradeSuccessful] = useState(false);

    const [week, setWeek] = useState(0);
    const [season, setSeason] = useState(0);
    const [redPlayer, setRedPlayer] = useState(0);
    const [bluePlayer, setBluePlayer] = useState(0);
    const [redTeam, setRedTeam] = useState(0);
    const [blueTeam, setBlueTeam] = useState(0);

    const [weeksData, setWeeksData] = useState<IRegularSeasonWeeksDto[]>([]);
    const [seasonsData, setSeasonsData] = useState<IUnfinishedSeasonDto[]>([]);
    const [teamsData, setTeamsData] = useState([]);
    const [redTeamPlayersData, setRedTeamPlayersData] = useState<ITeamPlayersDto>();
    const [blueTeamPlayersData, setBlueTeamPlayersData] = useState<ITeamPlayersDto>();

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

    const updateTeams = async () => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new TeamsClient();
                const response = await client.getTeamsBySeasonId(season)
                    .then(response => response.toJSON() as Promise<ITeamsVm>);
                const data = response.teamList;
                setTeamsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const updateWeeks = async (evt) => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new WeeksClient();
                const response = await client.getRegularSeasonWeeks(season)
                    .then(response => response.toJSON() as Promise<IRegularSeasonWeeksVm>);
                const data = response.weekList;
                setWeeksData(data);
                await updateTeams();
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const updatePlayersFromTeam = async (teamId: number) => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new TeamsClient();
                const response = await client.getTeamPlayers(teamId)
                    .then(response => response.toJSON() as Promise<ITeamPlayersVm>);
                const data = response.teamPlayers;
                setRedTeamPlayersData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const updatePlayersToTeam = async (teamId: number) => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new TeamsClient();
                const response = await client.getTeamPlayers(teamId)
                    .then(response => response.toJSON() as Promise<ITeamPlayersVm>);
                const data = response.teamPlayers;
                setBlueTeamPlayersData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const tradePlayerToTeam = async (evt) => {
        try {
            let client = new PlayerTransactionsClient();
            const command = new TradePlayerToTeamCommand;
            command.season = season;
            command.week = week;
            command.teamTradedFrom = redTeam;
            command.teamTradedTo = blueTeam;
            command.tradedPlayer = redPlayer;
            command.tradedPlayerFor = bluePlayer;
            const response = await client.tradePlayerToTeam(command);

            setWeek(0);
            setBluePlayer(0);
            setRedPlayer(0);
            setBlueTeam(0);
            setRedTeam(0);

            setTradeSuccessful(response);

            setWeeksData([]);
            setTeamsData([]);
            setRedTeamPlayersData(null);
            setBlueTeamPlayersData(null);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const handleTeamTradedFrom = async (teamId: number) => {
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

        newTeams = disabledTeam;

        setTeamsData(newTeams);

        setRedTeam(teamId);

        await updatePlayersFromTeam(teamId);
    };

    const handleTeamTradedTo = async (teamId: number) => {
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

        newTeams = disabledTeam;

        setTeamsData(newTeams);

        setBlueTeam(teamId);

        await updatePlayersToTeam(teamId);
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

    // create a list item for each season
    const renderWeeksList = () => {
        let select = null;
        if (weeksData && weeksData.length > 0) {
            select = (
                <Select
                    options={weeksData}
                    onChange={e => setWeek(e.id)}
                    isSearchable={true}
                    value={weeksData.find(o => o.id == week) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `Week #${label.weekNumber} - ${label.weekStartDate}`}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No weeks!", value: "0" }]} />);
        }

        return (select);
    };

    // create a list item for each team
    const renderTradedFromList = () => {
        let select = null;
        if (teamsData && teamsData.length > 0) {
            select = (
                <Select
                    options={teamsData}
                    onChange={e => handleTeamTradedFrom(e.id)}
                    isSearchable={true}
                    value={teamsData.find(o => o.id == redTeam) || null}
                    isOptionDisabled={value => value.isdisabled}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.teamName} (${label.teamAbbreviation})`}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No teams!", value: "0" }]} />);
        }

        return (select);
    };

    // create a list item for each team
    const renderTradedToList = () => {
        let select = null;
        if (teamsData && teamsData.length > 0) {
            select = (
                <Select
                    options={teamsData}
                    onChange={e => handleTeamTradedTo(e.id)}
                    isSearchable={true}
                    value={teamsData.find(o => o.id == blueTeam) || null}
                    isOptionDisabled={value => value.isdisabled}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.teamName} (${label.teamAbbreviation})`}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No teams!", value: "0" }]} />);
        }

        return (select);
    };

    // create a list item for each player traded from
    const renderPlayerTradedFrom = () => {
        let select = null;
        if (redTeamPlayersData && redTeamPlayersData.teamPlayers && redTeamPlayersData.teamPlayers.length > 0) {
            select = (
                <Select
                    options={redTeamPlayersData.teamPlayers}
                    onChange={e => setRedPlayer(e.id)}
                    isSearchable={true}
                    value={redTeamPlayersData.teamPlayers.find(o => o.id == redPlayer) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.playerName}`}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No players!", value: "0" }]} />);
        }

        return (select);
    };

    // create a list item for each player traded to
    const renderPlayerTradedTo = () => {
        let select = null;
        if (blueTeamPlayersData && blueTeamPlayersData.teamPlayers && blueTeamPlayersData.teamPlayers.length > 0) {
            select = (
                <Select
                    options={blueTeamPlayersData.teamPlayers}
                    onChange={e => setBluePlayer(e.id)}
                    isSearchable={true}
                    value={blueTeamPlayersData.teamPlayers.find(o => o.id == bluePlayer) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.playerName}`}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No players!", value: "0" }]} />);
        }

        return (select);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Trade Player To Team</h3>
                    <p>Please select a season to make a trade..</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={updateWeeks}>Select Season</Button>
                    <br />
                    <Label for="week">Select a week</Label>
                    <br />
                    {renderWeeksList()}
                    <br />
                    <h4 className="text-center text-danger">Warning: This will allow you to create a trade during any regular season week, but be advised the the roster change happens immediately.</h4>
                    <h4 className="text-center text-danger">In other words, don't make this trade if there's an outstanding game to process with this player on this team.</h4>
                </Col>
            </Row>
            <Row>
                <Col xs="6">
                    <h4 className="text-center">Player traded from</h4>
                    {renderTradedFromList()}
                    <br />
                    {renderPlayerTradedFrom()}
                </Col>
                <Col xs="6">
                    <h4 className="text-center">Player traded to</h4>
                    {renderTradedToList()}
                    <br />
                    {renderPlayerTradedTo()}
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <br />
                    {tradeSuccessful && (<h2 className="text-center">Trade completed!</h2>)}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || !(week > 0) || !(redTeam > 0) || !(blueTeam > 0) || !(redPlayer > 0) || !(bluePlayer > 0) || loading} onClick={tradePlayerToTeam}>Finalize Trade</Button>
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default TradePlayerToTeam