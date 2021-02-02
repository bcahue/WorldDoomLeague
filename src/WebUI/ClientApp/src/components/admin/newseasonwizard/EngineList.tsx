import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, FormGroup, Form, Button, Alert } from 'reactstrap';
import {
    IEnginesVm,
    IEnginesDto,
    CreateEngineCommand,
    EngineClient
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const EngineList = (props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [data, setData] = useState<IEnginesDto[]>([]);
    const [index, setIndex] = useState(0);
    const [engineFormName, setEngineFormName] = useState<string>("");
    const [engineFormUrl, setEngineFormUrl] = useState<string>("");
    const [newEngineId, setNewEngineId] = useState(0);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new EngineClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<IEnginesVm>);
                const data = response.engineList;
                setData(data);
            } catch (e) {
                if (e.response) {
                    setErrorMessage(JSON.parse(e.response));
                } else {
                    console.log(e);
                }
            }
            setLoading(false);
        };

        fetchData();
    }, [newEngineId]);

    const handleEngineChange = (e) => {
        setIndex(e.target.value);
        props.update(e.target.name, e.target.value);
    };

    const handleSubmit = async (evt) => {
        try {
            let client = new EngineClient();
            const command = new CreateEngineCommand;
            command.engineName = engineFormName;
            command.engineUrl = engineFormUrl;
            const response = await client.create(command);
            setNewEngineId(response);
            setIndex(response);
            setEngineFormName('');
            setEngineFormUrl('');
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create a list for each engine.
    const renderEngineDropdown = () => {
        let select = null;
        if (!loading) {
            if (data.length > 0) {
                select = (
                    data.map(engine =>
                        <option key={engine.id} value={engine.id}>
                            {engine.engineName}
                        </option>));
            } else {
                select = (
                    <option>
                        No engines currently in the system.
                    </option>);
            }
        } else {
            select = (
                <option>
                    Loading...
                </option>);
        }
        return (select);
    };

    // create a form for entering a new engine.
    const renderNewEngineForm = () => {
        return (
            <React.Fragment>
                <FormGroup>
                    <Label for="engineName">Engine Name</Label>
                    <Input type="text" name="engineName" id="engineName" value={engineFormName} placeholder="Odamex v0.8.3" onChange={e => setEngineFormName(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for="engineUrl">Engine Url</Label>
                    <Input type="text" name="engineUrl" id="engineUrl" value={engineFormUrl} placeholder="https://odamex.net" onChange={e => setEngineFormUrl(e.target.value)} />
                </FormGroup>
                <Button color="primary" size="lg" block disabled={!engineFormUrl || !engineFormName} onClick={handleSubmit}>Create New Engine</Button>
            </React.Fragment>
        );
    };

    return (
        <React.Fragment>
            <Form>
                <FormGroup>
                    <Label for="engine">Engine</Label>
                    <Input type="select" name="engine" id="engineSelect" value={index} onChange={(e) => handleEngineChange(e)}>
                        {renderEngineDropdown()}
                    </Input>
                </FormGroup>
            </Form>
            <Form>
                {renderNewEngineForm()}
            </Form>
        </React.Fragment>
    );
};

export default EngineList