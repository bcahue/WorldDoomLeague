import * as React from 'react';
import { useState, useEffect } from 'react';
import { Spinner, ListGroup, ListGroupItem, Label, Input, FormText, Button } from 'reactstrap';
import DateTimePicker from 'react-datetime-picker';
import { setErrorMessage } from '../../../state';
import {
    ISeasonsVm,
    ISeasonDto,
    SeasonsClient,
    CreateSeasonCommand
} from '../../../WorldDoomLeague';

const SeasonList = (props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [data, setData] = useState<ISeasonDto[]>([]);
    const [seasonName, setSeasonName] = useState<string>("");
    const [newSeasonId, setNewSeasonId] = useState(0);
    const [signupDate, setSignupDate] = useState<Date>(null);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<ISeasonsVm>);
                const data = await response.seasonList;
                setData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [newSeasonId]);

    const handleSeasonChange = (name, value) => {
        setNewSeasonId(value);
        props.update(name, value);
    };

    const handleSubmit = async (evt) => {
        try {
            let client = new SeasonsClient();
            const command = new CreateSeasonCommand;
            command.seasonName = seasonName;
            command.wadId = props.form.wad;
            command.enginePlayed = props.form.engine;
            command.seasonDateStart = signupDate;
            const response = await client.create(command);
            setNewSeasonId(response);
            setSeasonName('');
            handleSeasonChange("season", response);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const renderNewSeasonForm = () => {
        return (
            <React.Fragment>
                <Label for='seasonname'>Season Name</Label>
                <Input type='text' className='form-control' id='seasonname' name='seasonname' placeholder='Season Name' onChange={e => setSeasonName(e.target.value)} />
                <Label for='signupDates'>Signups Begin</Label>
                <DateTimePicker id='signupDates' name='signupDates' onChange={setSignupDate} value={signupDate} />
                <FormText color="muted">Here is a list of current and former seasons for convenience.</FormText>
                <ListGroup>
                    {renderSeasonList()}
                </ListGroup>
                <br />
                <Button color="primary" size="lg" block disabled={!seasonName || !props.form.wad || !props.form.engine || !signupDate} onClick={handleSubmit}>Create New Season</Button>
            </React.Fragment>
        );
    };

    // create a list for each season.
    const renderSeasonList = () => {
        var listArray = [];
        if (!loading) {
            const seasonObject = data;
            if (seasonObject.length > 0) {
                seasonObject.forEach(function (value) {
                    listArray.push(
                        <ListGroupItem key={value.id}>
                            {value.seasonName}
                            <br />
                            Signups Begin: {new Date(value.dateStart).toString()}
                        </ListGroupItem>);
                });
            } else {
                listArray.push(<ListGroupItem key="none">There are currently no seasons recorded in the system.</ListGroupItem>);
            }
        } else {
            listArray.push(<ListGroupItem key="spinner"><Spinner size="sm" color="primary" /></ListGroupItem>);
        }
        return (listArray);
    };

    return (
        <React.Fragment>
            {renderNewSeasonForm()}
        </React.Fragment>
    );
};

export default SeasonList