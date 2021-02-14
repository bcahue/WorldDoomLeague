import StepButtons from './StepButtons'
import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Card, CardBody, CardTitle, CardSubtitle, CardText, Button, InputGroupAddon, InputGroup, InputGroupText } from 'reactstrap';
import Select from 'react-select';
import { TeamsRequest, IDraftRequest, DraftClient, CreateDraftCommand, DraftRequest } from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const RegisterDraft = props => {
    const [draftList, setDraftList] = useState<IDraftRequest[]>([{
        nominatedPlayer: null,
        nominatingPlayer: null,
        playerSoldTo: null,
        sellPrice: 0
    }]);
    const [playersPerTeam, setPlayersPerTeam] = useState(3);
    const [canSubmitDraft, setCanSubmitDraft] = useState(false);
    const [completedDraft, setCompletedDraft] = useState(false);
    const [canCreateDraft, setCanCreateDraft] = useState(true);

    const createDraftPicks = () => {
        setCanCreateDraft(false);

        const pad_array = (arr, len, fill) => {
            return arr.concat(Array(len).fill(fill)).slice(0, len);
        };

        if (props.form.captains) {
            const maxPlayersDrafted = props.form.captains.length * playersPerTeam;

            const newDraftList = pad_array(draftList, maxPlayersDrafted, {
                nominatedPlayer: null,
                nominatingPlayer: null,
                playerSoldTo: null,
                sellPrice: 0
            });

            const playersPerTeamMinusCaptain = playersPerTeam;

            const newCaptainArray = props.form.captains.map((s, _idx) => {
                return { ...s, playersLeft: playersPerTeamMinusCaptain };
            });

            props.update("captains", newCaptainArray);

            setDraftList(newDraftList);
        }
    };

    const handleNominatingSelected = (idx, value) => {
        const newNominatingPlayer = draftList.map((draft, sidx) => {
            if (idx !== sidx) return draft;
            return { ...draft, nominatingPlayer: value.value };
        });

        setDraftList(newNominatingPlayer);
        checkIfFormComplete(newNominatingPlayer);
    };

    // Handle the sell price
    const handleSoldForInput = (idx, value) => {
        const newSellPrice = draftList.map((draft, sidx) => {
            if (idx !== sidx) return draft;
            return { ...draft, sellPrice: value };
        });

        setDraftList(newSellPrice);
        checkIfFormComplete(newSellPrice);
    };

    const handleSoldToSelected = (idx, value) => {
        // subtract the playersleft to handle disables
        const newCaptainsArray = props.form.captains.map((s, _idx) => {
            if (s.value !== value.value) return s;
            const left = s.playersLeft - 1;
            return { ...s, playersLeft: left};
        });

        // re-enable a captain if they are deselected.
        if (draftList[idx].playerSoldTo !== null) {
            const reenabledOldCaptain = newCaptainsArray.map((s, _idx) => {
                if (s.value !== draftList[idx].playerSoldTo) return s;
                const left = s.playersLeft + 1;
                return { ...s, playersLeft: left };
            });
            const newCaptainList = reenabledOldCaptain.map((s, _idx) => {
                if (s.playersLeft > 0) return { ...s, isdisabled: false };
                return { ...s, isdisabled: true };
            });
            props.update("captains", newCaptainList);
        } else {
            const newCaptainList = newCaptainsArray.map((s, _idx) => {
                if (s.playersLeft > 0) return { ...s, isdisabled: false };
                return { ...s, isdisabled: true };
            });
            props.update("captains", newCaptainList);
        }

        const newCaptainSoldTo = draftList.map((draft, sidx) => {
            if (idx !== sidx) return draft;
            return { ...draft, playerSoldTo: value.value };
        });

        setDraftList(newCaptainSoldTo);
        checkIfFormComplete(newCaptainSoldTo);
    };

    const handleNominatedSelected = (idx, value) => {
        const newPlayerArray = props.form.players.map((s, _idx) => {
            if (s.value !== value.value) return s;
            return { ...s, isdisabled: true };
        });

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
            let client = new DraftClient();
            const command = new CreateDraftCommand;
            // Need to do this to get around an NSwag generator bug.
            // https://github.com/RicoSuter/NSwag/issues/2862
            let draft: DraftRequest[] = [];
            for (var idx = 0; idx < draftList.length; idx++) {
                var addPick = new DraftRequest();
                addPick.nominatedPlayer = draftList[idx].nominatedPlayer;
                addPick.nominatingPlayer = draftList[idx].nominatingPlayer;
                addPick.playerSoldTo = draftList[idx].playerSoldTo;
                addPick.sellPrice = draftList[idx].sellPrice;
                draft.push(addPick);
            }

            command.season = props.form.season;
            command.draftRequestList = draft;
            const response = await client.create(command);
            setCanSubmitDraft(false);
            setCompletedDraft(true);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const checkIfFormComplete = (draft) => {
        if (draft.every(element => element.nominatedPlayer && element.nominatingPlayer && element.playerSoldTo &&
            element.sellPrice.length > 0)) {
            setCanSubmitDraft(true);
        } else {
            setCanSubmitDraft(false);
        }
    };

    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    // create a list for each captain who bought a player.
    const renderSoldForInput = (idx) => {
        let select = null;
            select = (
                <InputGroup>
                    <InputGroupAddon addonType="prepend">
                        <InputGroupText>$</InputGroupText>
                    </InputGroupAddon>
                    <Input placeholder="Amount" min={1} max={28} type="number" step="1" disabled={completedDraft} onChange={e => handleSoldForInput(idx, e.target.value)} />
                </InputGroup>)

        return (select);
    };

    // create a list for each captain who bought a player.
    const renderSoldToDropdown = (idx) => {
        let select = null;
        if (props.form.captains) {
            select = (
                <Select
                    options={props.form.captains}
                    onChange={e => handleSoldToSelected(idx, e)}
                    isOptionDisabled={(option) => option.isdisabled}
                    isDisabled={completedDraft}
                    isSearchable={true}
                />)
        } else {
            select = (<Select options={[{ label: "No captains left!", value: "Not" }]} isDisabled={completedDraft} />);
        }

        return (select);
    };

    // create a list for each nominating captain.
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

    // create a list for each nominated player.
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
                    <Button color="primary" size="lg" block disabled={!canCreateDraft} onClick={createDraftPicks}>Create Draft Picks</Button>
                    <hr />
                </Col>
            </Row>
            <Row>
                {draftList && canCreateDraft == false && (draftList.map((draft, index) => (
                    <Col xs="6" sm="4">
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
                        <br />
                    </Col>
                )))}
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <Button color="primary" size="lg" block disabled={!canSubmitDraft} onClick={createDraft}>Finalize Draft</Button>
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