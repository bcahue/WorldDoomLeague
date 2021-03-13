import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, FormGroup, Button, Row, Col } from 'reactstrap';
import Select from 'react-select';

import {
    IPlayersVm,
    IPlayerDto,
    PlayersClient,
    UpdatePlayerCommand
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const EditPlayers = (props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [playerDataChanged, setPlayerDataChanged] = useState<boolean>(false);
    const [playerChangedName, setPlayerChangedName] = useState<string>("");
    const [playerFormName, setPlayerFormName] = useState<string>("");
    const [playerFormAlias, setPlayerFormAlias] = useState<string>("");
    const [editedPlayerId, setEditedPlayerId] = useState(0);
    const [player, setPlayer] = useState(0);

    const [data, setData] = useState<IPlayerDto[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new PlayersClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<IPlayersVm>);
                const data = response.playerList;
                setData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [editedPlayerId]);

    const checkIfPlayerDataChanged = (playerName: string, playerAlias: string) => {
        if (playerName == data.find(o => o.id == player).playerName && playerAlias == data.find(o => o.id == player).playerAlias) {
            setPlayerDataChanged(false);
        } else {
            setPlayerDataChanged(true);
        }
    };

    const handleFormPlayerNameChanged = (playerName: string) => {
        checkIfPlayerDataChanged(playerName, playerFormAlias);

        setPlayerFormName(playerName);
    };

    const handleFormPlayerAliasChanged = (playerAlias: string) => {
        checkIfPlayerDataChanged(playerFormName, playerAlias);

        setPlayerFormAlias(playerAlias);
    };

    const handlePlayerListSelected = (playerId: number) => {
        setPlayerFormName(data.find(o => o.id == playerId).playerName);
        setPlayerFormAlias(data.find(o => o.id == playerId).playerAlias);
        setPlayer(playerId);
    };

    const editPlayer = async (evt) => {
        try {
            let client = new PlayersClient();
            const command = new UpdatePlayerCommand;
            command.playerId = player;
            command.playerName = playerFormName;
            command.playerAlias = playerFormAlias;
            const response = await client.update(player, command);
            setPlayer(0);
            setEditedPlayerId(response);
            setPlayerChangedName(playerFormName);
            setPlayerFormName('');
            setPlayerFormAlias('');
            setPlayerDataChanged(false);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create a list for each engine.
    const renderPlayerDropdown = () => {
        let select = null;
        if (data.length > 0) {
            select = (
                <Select
                    options={data}
                    onChange={e => handlePlayerListSelected(e.id)}
                    isSearchable={true}
                    value={data.find(o => o.id == player) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.playerName}`}
                    isLoading={loading}
                />)
        } else {
            select = (<Select options={[{ label: "No players found in the system.", value: "Not" }]} />);
        }
        return (select);
    };

    // create a form for entering a new engine.
    const renderEditPlayerForm = () => {
        return (
            <React.Fragment>
                <FormGroup>
                    <Label for='playername'>Player Name</Label>
                    <Input type='text' className='form-control' id='playername' name='playername' placeholder='Player Name' value={playerFormName} disabled={!(player > 0)} onChange={e => handleFormPlayerNameChanged(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for='playeralias'>Player Alias</Label>
                    <Input type='text' className='form-control' id='playeralias' name='playeralias' placeholder='Player Alias' value={playerFormAlias} disabled={!(player > 0)} onChange={e => handleFormPlayerAliasChanged(e.target.value)} />
                </FormGroup>
                <Button color="primary" size="lg" block disabled={!playerDataChanged} onClick={editPlayer}>Edit Player</Button>
            </React.Fragment>
        );
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Edit Player</h3>
                    <p>Please select a player to make changes to.</p>
                    {renderPlayerDropdown()}
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    {renderEditPlayerForm()}
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    {playerChangedName !== "" && (<h3 className='text-center'>{playerChangedName} has been successfully changed!</h3>)}
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default EditPlayers