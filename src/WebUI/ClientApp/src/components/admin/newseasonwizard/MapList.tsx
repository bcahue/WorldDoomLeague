import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, FormGroup, Form, Button } from 'reactstrap';
import {
    IMapsVm,
    IMapsDto,
    MapsClient,
    CreateMapCommand
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const MapList = (props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [data, setData] = useState<IMapsDto[]>([]);
    const [index, setIndex] = useState(0);
    const [mapPack, setMapPack] = useState<string>("");
    const [mapName, setMapName] = useState<string>("");
    const [mapNumber, setMapNumber] = useState(0);
    const [newMapId, setNewMapId] = useState(0);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MapsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<IMapsVm>);
                const data = response.mapList;
                setData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [newMapId]);

    const handleMapChange = (name, value) => {
        setIndex(value);
        props.update(name, value);
    };

    const handleSubmit = async (evt) => {
        try {
            let client = new MapsClient();
            const command = new CreateMapCommand;
            command.mapName = mapName;
            command.mapNumber = mapNumber;
            const response = await client.create(command);
            setNewMapId(response);
            setMapPack('');
            setMapName('');
            setMapNumber(0);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create a list for each engine.
    const renderMapDropdown = () => {
        let select = null;
        if (!loading) {
            if (data.length > 0) {
                select = (
                    data.map(map =>
                        <option key={map.id} value={map.id}>
                            {map.mapName} | {map.mapPack}
                        </option>));
            } else {
                select = (
                    <option>
                        No maps currently in the system.
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
    const renderNewMapForm = () => {
        return (
            <React.Fragment>
                <FormGroup>
                    <Label for="mapName">Map Name</Label>
                    <Input type="text" name="mapName" id="mapName" value={mapName} placeholder="N's Base of Boppin'" onChange={e => setMapName(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for="mapPack">Pack</Label>
                    <Input type="text" name="mapPack" id="mapPack" value={mapPack} placeholder="32in24-4" onChange={e => setMapPack(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for="engineUrl">Map Number</Label>
                    <Input placeholder="Amount" min={1} max={30} type="number" step="1" value={mapNumber} onChange={e => setMapNumber(parseInt(e.target.value, 10))} />
                </FormGroup>
                <Button color="primary" size="lg" block disabled={!mapName || !mapName || !mapNumber} onClick={handleSubmit}>Create New Map</Button>
            </React.Fragment>
        );
    };

    return (
        <React.Fragment>
            <Form>
                <FormGroup>
                    <Label for="engine">Map</Label>
                    <Input type="select" name="engine" id="engineSelect" value={index} onChange={(e) => handleMapChange(e.target.name, e.target.value)}>
                        {renderMapDropdown()}
                    </Input>
                </FormGroup>
            </Form>
            <Form>
                {renderNewMapForm()}
            </Form>
        </React.Fragment>
    );
};

export default MapList