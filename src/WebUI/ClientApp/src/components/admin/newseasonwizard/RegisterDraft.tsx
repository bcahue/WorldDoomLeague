import StepButtons from './StepButtons'
import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Card, CardBody, CardTitle, CardSubtitle, CardText, Button } from 'reactstrap';
import Select from 'react-select';
import { ITeamsRequest, TeamsRequest, TeamsClient, CreateTeamsCommand, IDraftRequest } from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const RegisterDraft = props => {
    const [draftList, setDraftList] = useState<IDraftRequest[]>([{
        nominatedPlayer: null,
        nominatingPlayer: null,
        playerSoldTo: null,
        sellPrice: 0
    }]);
    const [playersPerTeam, setPlayersPerTeam] = useState(4);
    const [canSubmitDraft, setCanSubmitDraft] = useState(false);
    const [completedDraft, setCompletedDraft] = useState(false);

    const maxPlayersDrafted = props.form.captains.length * playersPerTeam;

    useEffect(() => {
        const pad_array = (arr, len, fill) => {
            return arr.concat(Array(len).fill(fill)).slice(0, len);
        };

        const newTeamList = pad_array(draftList, maxPlayersDrafted, {
            nominatedPlayer: null,
            nominatingPlayer: null,
            playerSoldTo: null,
            sellPrice: 0
        });
        setDraftList(newTeamList);
    }, []);

    const handleNominatingSelected = (idx, value) => {
        const newTeamName = draftList.map((draft, sidx) => {
            if (idx !== sidx) return draft;
            return { ...draft, nominatingPlayer: value };
        });

        setDraftList(newTeamName);
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

    const handleNominatedSelected = (idx, value) => {
        const newPlayerArray = props.form.players.map((s, _idx) => {
            if (s.value !== value.value) return s;
            return { ...s, isdisabled: true };
        })

        if (draftList[idx].nominatedPlayer !== null) {
            const reenabledOldPlayer = newPlayerArray.map((s, _idx) => {
                if (s.value !== draftList[idx].nominatedPlayer) return s;
                return { ...s, isdisabled: false };
            });
            props.update("players", reenabledOldPlayer);
        } else {
            props.update("players", newPlayerArray);
        }

        const newCaptain = draftList.map((draft, sidx) => {
            if (idx !== sidx) return draft;
            return { ...draft, nominatedPlayer: value.value };
        });

        setDraftList(newCaptain);
        checkIfFormComplete(newCaptain);
    };

    const createDraft = async (evt) => {
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
            setCanSubmitDraft(false);
            setCompletedDraft(true);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const createPlayersList = () => {
        return props.form.players.filter((s, _idx) => s.isdisabled !== true);
    };

    const checkIfFormComplete = (draft) => {
        if (draft.every(element => element.teamAbbreviation && element.teamCaptain && element.teamName &&
            element.teamAbbreviation.length > 0 && element.teamName.length > 0)) {
            setCanSubmitDraft(true);
        } else {
            setCanSubmitDraft(false);
        }
    };

    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    // create a list for each engine.
    const renderNominatingCaptainDropdown = (idx) => {
        let select = null;
        if (props.form.captains) {
            select = (
                <Select
                    options={props.form.captains}
                    onChange={e => handleNominatingSelected(idx, e)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={completedDraft}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No captains left!", value: "Not" }]} isDisabled={completedDraft} />);
        }

        return (select);
    };

    // create a list for each engine.
    const renderNominatedPlayerDropdown = (idx) => {
        let select = null;
        if (props.form.players) {
            select = (
                <Select
                    options={props.form.players}
                    onChange={e => handleNominatedSelected(idx, e)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={completedDraft}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No players left!", value: "Not" }]} isDisabled={completedDraft} />);
        }

        return (select);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Register Draft</h3>
                    <p>Please input the draft information. When completed, the team rosters will be finalized.</p>
                    <Label for="amountTeams">Amount of players per team</Label>
                    <Input placeholder="Amount" name="amountPlayers" min={4} max={4} type="number" step="1" value={4} disabled={true} />
                    <br />
                    <hr />
                </Col>
                {draftList && (draftList.map((draft, index) => (
                        <Card>
                        <CardBody>
                            <CardTitle tag="h5">Draft Pick #{index + 1}</CardTitle>
                            Captain 
                            {renderNominatingCaptainDropdown(index)}
                            nominates player
                            {renderNominatedPlayerDropdown(index)}
                            who is bought by
                            {renderSoldToDropdown(index)}
                            for
                            {renderSoldForInput(index)}
                            </CardBody>
                        </Card>
                    )))}
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!completedDraft} onClick={createDraft}>Finalize Draft</Button>
                </Col>
            </Row>
        <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <StepButtons step={3} {...props} disabled={!completedDraft} />
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default RegisterDraft