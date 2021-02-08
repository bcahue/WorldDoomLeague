import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, FormGroup, Form, Button, Alert } from 'reactstrap';
import Select from 'react-select';

import {
    IPlayersVm,
    IPlayerDto,
    CreatePlayerCommand,
    PlayersClient
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const PlayerList = (props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [data, setData] = useState([]);
    const [playerFormName, setPlayerFormName] = useState<string>("");
    const [playerFormAlias, setPlayerFormAlias] = useState<string>("");
    const [newPlayerId, setNewPlayerId] = useState(0);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new PlayersClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<IPlayersVm>);
                const data = response.playerList;
                let optionsList = [];
                data.forEach(function (element) {
                    optionsList.push({ label: element.playerName, value: element.id })
                });
                setData(optionsList);
                props.update("players", optionsList);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [newPlayerId]);

    const handleSubmit = async (evt) => {
        try {
            let client = new PlayersClient();
            const command = new CreatePlayerCommand;
            command.playerName = playerFormName;
            command.playerAlias = playerFormAlias;
            const response = await client.create(command);
            setNewPlayerId(response);
            setPlayerFormName('');
            setPlayerFormAlias('');
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create a list for each engine.
    const renderPlayerDropdown = () => {
        let select = null;
        if (!loading) {
            if (data.length > 0) {
                select = (
                    <Select
                        options={data}
                    />)
            } else {
                select = (<Select options={[{ label: "No players found in the system.", value: "Not" }]} />);
            }
        } else {
            select = (<Select isLoading={true}/>);
        }
        return (select);
    };

    // create a form for entering a new engine.
    const renderNewPlayerForm = () => {
        return (
            <React.Fragment>
                <FormGroup>
                    <Label for='playername'>Player Name</Label>
                    <Input type='text' className='form-control' id='playername' name='playername' placeholder='Player Name' value={playerFormName} onChange={e => setPlayerFormName(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for='playeralias'>Player Alias</Label>
                    <Input type='text' className='form-control' id='playeralias' name='playeralias' placeholder='Player Alias' value={playerFormAlias} onChange={e => setPlayerFormAlias(e.target.value)} />
                </FormGroup>
                <Button color="primary" size="lg" block disabled={!playerFormName} onClick={handleSubmit}>Create New Player</Button>
            </React.Fragment>
        );
    };

    return (
        <React.Fragment>
            <Form>
                <FormGroup>
                    <Label for="player">Player</Label>
                        {renderPlayerDropdown()}
                </FormGroup>
            </Form>
            <Form>
                {renderNewPlayerForm()}
            </Form>
        </React.Fragment>
    );
};

export default PlayerList