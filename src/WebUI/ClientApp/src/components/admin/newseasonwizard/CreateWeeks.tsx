import StepButtons from './StepButtons'
import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Card, CardBody, CardTitle, CardSubtitle, CardText, Button } from 'reactstrap';
import Select from 'react-select';
import { ITeamsRequest, TeamsRequest, TeamsClient, CreateTeamsCommand } from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const CreateWeeks = props => {
    const [teamList, setTeamList] = useState<ITeamsRequest[]>([{
        teamName: null,
        teamAbbreviation: null,
        teamCaptain: null
    }]);
    const [amountTeams, setAmountTeams] = useState(6);
    const [createdTeams, setCreatedTeams] = useState(0);
    const [toggle, setToggle] = useState(false);
    const [canSubmitTeams, setCanSubmitTeams] = useState(false);

    useEffect(() => {
        const pad_array = (arr, len, fill) => {
            return arr.concat(Array(len).fill(fill)).slice(0, len);
        };

        const newTeamList = pad_array(teamList, amountTeams, {
            teamName: null,
            teamAbbreviation: null,
            teamCaptain: null
        });
        setTeamList(newTeamList);
    }, [amountTeams]);

    const enableTeamList = () => {
        setToggle(true);
    };

    const handleTeamNameChange = (idx, value) => {
        const newTeamName = teamList.map((team, sidx) => {
            if (idx !== sidx) return team;
            return { ...team, teamName: value };
        });

        setTeamList(newTeamName);
        checkIfFormComplete(newTeamName);
    };

    const handleTeamAbvChange = (idx, value) => {
        const newTeamName = teamList.map((team, sidx) => {
            if (idx !== sidx) return team;
            return { ...team, teamAbbreviation: value };
        });

        setTeamList(newTeamName);
        checkIfFormComplete(newTeamName);
    };

    const handlePlayerSelected = (idx, value) => {
        const newPlayerArray = props.form.players.map((s, _idx) => {
            if (s.value !== value.value) return s;
            return { ...s, isdisabled: true };
        })

        if (teamList[idx].teamCaptain !== null) {
            const reenabledOldPlayer = newPlayerArray.map((s, _idx) => {
                if (s.value !== teamList[idx].teamCaptain) return s;
                return { ...s, isdisabled: false };
            });
            props.update("players", reenabledOldPlayer);
        } else {
            props.update("players", newPlayerArray);
        }

        const newCaptain = teamList.map((team, sidx) => {
            if (idx !== sidx) return team;
            return { ...team, teamCaptain: value.value };
        });

        setTeamList(newCaptain);
        checkIfFormComplete(newCaptain);
    };

    const createTeams = async (evt) => {
        try {
            let client = new TeamsClient();
            const command = new CreateTeamsCommand;
            // Need to do this to get around an NSwag generator bug.
            // https://github.com/RicoSuter/NSwag/issues/2862
            let teams: TeamsRequest[] = [];
            for (var idx = 0; idx < teamList.length; idx++) {
                var addTeam = new TeamsRequest();
                addTeam.teamName = teamList[idx].teamName;
                addTeam.teamAbbreviation = teamList[idx].teamAbbreviation;
                addTeam.teamCaptain = teamList[idx].teamCaptain;
                teams.push(addTeam);
            }

            command.season = props.form.season;
            command.teamsRequestList = teams;
            const response = await client.createTeams(command);
            setCreatedTeams(response);
            const captains = createCaptainsList();
            setCanSubmitTeams(false);
            setAmountTeams(response);
            props.update("captains", captains);
            const players = createPlayersList();
            props.update("players", players);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const createCaptainsList = () => {
        return teamList.map((s, _idx) => {
            const captain = props.form.players.find(i => i.value === s.teamCaptain);
            if (s.teamCaptain !== captain.value) return s;
            // this should add the captain names to the objects.
            return { ...s, label: captain.label, value: captain.value };
        });
    };

    const createPlayersList = () => {
        return props.form.players.filter((s, _idx) => s.isdisabled !== true);
    };

    const checkIfFormComplete = (teams) => {
        if (teams.every(element => element.teamAbbreviation && element.teamCaptain && element.teamName &&
            element.teamAbbreviation.length > 0 && element.teamName.length > 0)) {
            setCanSubmitTeams(true);
        } else {
            setCanSubmitTeams(false);
        }
    };

    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    // create a list for each engine.
    const renderPlayerDropdown = (idx) => {
        let select = null;
        if (props.form.players) {
            select = (
                <Select
                    options={props.form.players}
                    onChange={e => handlePlayerSelected(idx, e)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={(createdTeams === amountTeams)}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No players left!", value: "Not" }]} isDisabled={(createdTeams === amountTeams)} />);
        }

        return (select);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Add Teams</h3>
                    <p>This step will add the teams that will play during the season.</p>
                    <Label for="amountTeams">Amount of teams</Label>
                    <Input placeholder="Amount" name="amountTeams" min={4} max={512} type="number" step="2" value={amountTeams} disabled={toggle} onChange={e => setAmountTeams(parseInt(e.target.value, 10))} />
                    <br />
                    <Button color="primary" size="lg" block disabled={toggle} onClick={enableTeamList}>Generate Team Cards</Button>
                    <hr />
                </Col>
            </Row>
            <Row>
                {teamList && toggle && (teamList.map((team, index) => (
                    <Col xs="6" sm="4">
                        <Card>
                            <CardBody>
                                <CardTitle tag="h5">Team</CardTitle>
                                <Label for="teamName">Team Name</Label>
                                <Input type="text" value={team.teamName} placeholder="Super Chargers" disabled={(createdTeams === amountTeams)} onChange={e => handleTeamNameChange(index, e.target.value)} />
                                <Label for="teamName">Team Abbreviation</Label>
                                <Input type="text" value={team.teamAbbreviation} placeholder="SUC" disabled={(createdTeams === amountTeams)} onChange={e => handleTeamAbvChange(index, e.target.value)} />
                                <Label for="teamName">Team Captain</Label>
                                {renderPlayerDropdown(index)}
                            </CardBody>
                        </Card>
                        <br />
                    </Col>
                    )))}
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!canSubmitTeams} onClick={createTeams}>Create Teams</Button>
                </Col>
            </Row>
        <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <StepButtons step={3} {...props} disabled={!(createdTeams === amountTeams)} />
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default CreateWeeks