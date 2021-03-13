import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    SeasonsClient,
    IUnfinishedSeasonsVm,
    IUnfinishedSeasonDto,
    IRegularSeasonWeeksDto,
    IRegularSeasonWeeksVm,
    IFreeAgencyPlayersVm,
    IFreeAgencyPlayersDto,
    WeeksClient,
    ITeamsDto,
    TeamsClient,
    ITeamsVm,
    ITeamPlayersDto,
    ITeamPlayersVm,
    PlayerTransactionsClient,
    TradePlayerToFreeAgencyCommand,
    INonCaptainTeamPlayersDto,
    INonCaptainTeamPlayersVm,
    PromotePlayerToCaptainCommand,
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const PromotePlayerToCaptain = props => {
    const [loading, setLoading] = useState(true);
    const [tradeSuccessful, setTradeSuccessful] = useState(false);

    const [week, setWeek] = useState(0);
    const [season, setSeason] = useState(0);
    const [promotedPlayer, setPromotedPlayer] = useState(0);
    const [promotedTeam, setPromotedTeam] = useState(0);

    const [weeksData, setWeeksData] = useState<IRegularSeasonWeeksDto[]>([]);
    const [seasonsData, setSeasonsData] = useState<IUnfinishedSeasonDto[]>([]);
    const [teamsData, setTeamsData] = useState <ITeamsDto[]>([]);
    const [nonCaptains, setNonCaptains] = useState<INonCaptainTeamPlayersDto>();

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

    const updateTeamPlayers = async (teamId: number) => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new TeamsClient();
                const response = await client.getNonCaptainPlayers(teamId)
                    .then(response => response.toJSON() as Promise<INonCaptainTeamPlayersVm>);
                const data = response.teamPlayers;
                setNonCaptains(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const tradePlayerToFreeAgency = async (evt) => {
        try {
            let client = new PlayerTransactionsClient();
            const command = new PromotePlayerToCaptainCommand;
            command.season = season;
            command.week = week;
            command.team = promotedTeam;
            command.playerPromotedCaptain = promotedPlayer;
            const response = await client.promotePlayerToCaptain(command);

            setWeek(0);
            setPromotedPlayer(0);
            setPromotedTeam(0);

            setTradeSuccessful(response);

            setWeeksData([]);
            setTeamsData([]);
            setNonCaptains(null);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const handleTeamTradedFrom = async (teamId: number) => {
        setPromotedPlayer(teamId);

        await updateTeamPlayers(teamId);
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
    const renderPromotedTeamList = () => {
        let select = null;
        if (teamsData && teamsData.length > 0) {
            select = (
                <Select
                    options={teamsData}
                    onChange={e => handleTeamTradedFrom(e.id)}
                    isSearchable={true}
                    value={teamsData.find(o => o.id == promotedTeam) || null}
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
    const renderPromotedPlayerList = () => {
        let select = null;
        if (nonCaptains && nonCaptains.teamPlayers && nonCaptains.teamPlayers.length > 0) {
            select = (
                <Select
                    options={nonCaptains.teamPlayers}
                    onChange={e => setPromotedPlayer(e.id)}
                    isSearchable={true}
                    value={nonCaptains.teamPlayers.find(o => o.id == promotedPlayer) || null}
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
                    <h3 className='text-center'>Promote Player to Captain</h3>
                    <p>Please select a season to promote a player.</p>
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
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h4 className="text-center">Team to promote player</h4>
                    {renderPromotedTeamList()}
                    <br />
                    {renderPromotedPlayerList()}
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <br />
                    {tradeSuccessful && (<h2 className="text-center">Trade completed!</h2>)}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || !(week > 0) || !(promotedPlayer > 0) || !(promotedTeam > 0) || loading} onClick={tradePlayerToFreeAgency}>Finalize Promotion</Button>
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default PromotePlayerToCaptain