import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, FormGroup, Form, Button, FormText } from 'reactstrap';
import {
    IWadFilesVm,
    IWadFilesDto,
    FileParameter,
    FilesClient
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const WadList = (props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [data, setData] = useState<IWadFilesDto[]>([]);
    const [index, setIndex] = useState(0);
    const [file, setFile] = useState<FileParameter>(null);
    const [newWadId, setNewWadId] = useState(0);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new FilesClient();
                const response = await client.getWadFiles()
                    .then(response => response.toJSON() as Promise<IWadFilesVm>);
                const data = response.wadList;
                setData(data);
                if (data.length > 0) {
                    handleWadChange("wad", 1);
                }
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [newWadId]);

    const handleWadChange = (name, value) => {
        setIndex(value);
        props.update(name, value);
    };

    const handleSubmit = async (evt) => {
        try {
            let client = new FilesClient();
            const response = await client.createWadFile(file);
            setNewWadId(response);
            setFile(null);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const handleUpload = async (evt) => {
        const upload: FileParameter = {
            data: evt.target.files[0],
            fileName: evt.target.files[0].name
        };
        setFile(upload);
    };

    // create a list for each wad.
    const renderWadDropdown = () => {
        let select = null;
        if (!loading) {
            if (data.length > 0) {
                select = (
                    data.map(wad =>
                        <option key={wad.id} value={wad.id}>
                            {wad.fileName}
                        </option>));
            } else {
                select = (
                    <option>
                        No wad files currently in the system.
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
    const renderNewWadForm = () => {
        return (
            <React.Fragment>
                <FormGroup>
                    <Label for="File">Zipped Wad File</Label>
                    <Input type="file" name="file" id="file" key={newWadId} onChange={handleUpload} />
                    <FormText color="muted">
                        Max upload: 200MB
                        <br />
                        All files MUST be zipped.
                    </FormText>
                </FormGroup>
                <Button color="primary" size="lg" block disabled={!file} onClick={handleSubmit}>Upload new WAD</Button>
            </React.Fragment>
        );
    };

    return (
        <React.Fragment>
            <Form>
                <FormGroup>
                    <Label for="engine">Wad Played</Label>
                    <Input type="select" name="wad" id="wadSelect" value={index} onChange={(e) => handleWadChange(e.target.name, e.target.value)}>
                        {renderWadDropdown()}
                    </Input>
                </FormGroup>
            </Form>
            <Form>
                {renderNewWadForm()}
            </Form>
        </React.Fragment>
    );
};

export default WadList