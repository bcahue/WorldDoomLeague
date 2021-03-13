import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, FormGroup, Button, Row, Col } from 'reactstrap';
import Select from 'react-select';

import {
    ITeamsDto,
    ISeasonDto,
    SeasonsClient,
    ISeasonsVm,
    TeamsClient,
    UpdateTeamCommand,
    ITeamsVm
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const EditTeams = (props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [teamDataChanged, setTeamDataChanged] = useState<boolean>(false);
    const [teamChangedName, setTeamChangedName] = useState<string>("");
    const [teamFormName, setTeamFormName] = useState<string>("");
    const [teamFormAbv, setTeamFormAbv] = useState<string>("");
    const [team, setTeam] = useState(0);
    const [season, setSeason] = useState(0);

    const [teamsData, setTeamsData] = useState<ITeamsDto[]>([]);
    const [seasonsData, setSeasonsData] = useState<ISeasonDto[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<ISeasonsVm>);
                const data = response.seasonList;
                setSeasonsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    const getTeams = async (seasonId: number) => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new TeamsClient();
                const response = await client.getTeamsBySeasonId(seasonId)
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

    const checkIfTeamDataChanged = (teamName: string, teamAbbv: string) => {
        if (teamName == teamsData.find(o => o.id == team).teamName && teamAbbv == teamsData.find(o => o.id == team).teamAbbreviation) {
            setTeamDataChanged(false);
        } else {
            setTeamDataChanged(true);
        }
    };

    const handleFormTeamNameChanged = (teamName: string) => {
        checkIfTeamDataChanged(teamName, teamFormAbv);

        setTeamFormName(teamName);
    };

    const handleFormTeamAbvChanged = (teamAbv: string) => {
        checkIfTeamDataChanged(teamFormName, teamAbv);

        setTeamFormAbv(teamAbv);
    };

    const handleTeamListSelected = (teamId: number) => {
        setTeamFormName(teamsData.find(o => o.id == teamId).teamName);
        setTeamFormAbv(teamsData.find(o => o.id == teamId).teamAbbreviation);
        setTeam(teamId);
    };

    const handleSeasonSelected = async (seasonId: number) => {
        await getTeams(seasonId);

        setSeason(seasonId);
    };

    const editPlayer = async (evt) => {
        try {
            let client = new TeamsClient();
            const command = new UpdateTeamCommand;
            command.teamId = team;
            command.teamName = teamFormName;
            command.teamAbbreviation = teamFormAbv;
            const response = await client.update(team, command);
            setTeam(0);
            setTeamChangedName(teamFormName);
            setTeamFormName('');
            setTeamFormAbv('');
            setTeamDataChanged(false);
            await getTeams(season);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create a list for each season.
    const renderSeasonDropdown = () => {
        let select = null;
        if (seasonsData.length > 0) {
            select = (
                <Select
                    options={seasonsData}
                    onChange={e => handleSeasonSelected(e.id)}
                    isSearchable={true}
                    value={seasonsData.find(o => o.id == season) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.seasonName}`}
                    isLoading={loading}
                />)
        } else {
            select = (<Select options={[{ label: "No seasons found in the system.", value: "Not" }]} />);
        }
        return (select);
    };

    // create a list for each team.
    const renderTeamDropdown = () => {
        let select = null;
        if (teamsData.length > 0) {
            select = (
                <Select
                    options={teamsData}
                    onChange={e => handleTeamListSelected(e.id)}
                    isSearchable={true}
                    value={teamsData.find(o => o.id == team) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.teamName}`}
                    isLoading={loading}
                />)
        } else {
            select = (<Select options={[{ label: "No teams found in the system.", value: "Not" }]} />);
        }
        return (select);
    };

    // create a form for editing teams.
    const renderEditPlayerForm = () => {
        return (
            <React.Fragment>
                <FormGroup>
                    <Label for='playername'>Player Name</Label>
                    <Input type='text' className='form-control' id='teamname' name='teamname' placeholder='Team Name' maxLength={64} value={teamFormName} disabled={!(team > 0)} onChange={e => handleFormTeamNameChanged(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for='playeralias'>Player Alias</Label>
                    <Input type='text' className='form-control' id='teamabv' name='teamabv' placeholder='Team Abbreviation' maxLength={5} value={teamFormAbv} disabled={!(team > 0)} onChange={e => handleFormTeamAbvChanged(e.target.value)} />
                </FormGroup>
                <Button color="primary" size="lg" block disabled={!teamDataChanged} onClick={editPlayer}>Edit Team</Button>
            </React.Fragment>
        );
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Edit Team</h3>
                    <p>Please select a season.</p>
                    {renderSeasonDropdown()}
                    <br />
                    <p>Please select a team to make changes to.</p>
                    {renderTeamDropdown()}
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    {renderEditPlayerForm()}
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    {teamChangedName !== "" && (<h3 className='text-center'>{teamChangedName} has been successfully changed!</h3>)}
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default EditTeams