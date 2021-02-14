import StepButtons from './StepButtons'
import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button } from 'reactstrap';
import { CreateSeasonWeeksCommand, SeasonsClient } from '../../../WorldDoomLeague';
import DateTimePicker from 'react-datetime-picker';
import { setErrorMessage } from '../../../state';

const CreateWeeks = props => {
    const [amountWeeksRegularSeason, setAmountWeeksRegularSeason] = useState(5);
    const [amountWeeksPlayoffs, setAmountWeeksPlayoffs] = useState(1);
    const [weekOneDateStart, setWeekOneDateStart] = useState<Date>(null);
    const [amountRegSeasonWeeksCreated, setAmountRegSeasonWeeksCreated] = useState(0);
    const [toggle, setToggle] = useState(true);

    const createWeeks = async (evt) => {
        try {
            let client = new SeasonsClient();
            const command = new CreateSeasonWeeksCommand;
            command.seasonId = props.form.season;
            command.numWeeksPlayoffs = amountWeeksPlayoffs;
            command.numWeeksRegularSeason = amountWeeksRegularSeason;
            command.weekOneDateStart = weekOneDateStart;
            const response = await client.createWeeks(command);
            setAmountRegSeasonWeeksCreated(response);
            props.update("regSeasonWeeks", response);
            setToggle(false);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Add Weeks</h3>
                    <p>This step will add the weeks which games will be played.</p>
                    <Label for="amountWeeksRegularSeason">Regular season weeks</Label>
                    <Input placeholder="Amount" name="amountWeeksRegularSeason" min={4} max={26} type="number" step="1" value={amountWeeksRegularSeason} disabled={!toggle} onChange={e => setAmountWeeksRegularSeason((parseInt(e.target.value, 10)))} />
                    <br />
                    <Label for="amountWeeksPlayoffs">Playoff weeks</Label>
                    <Input placeholder="Amount" name="amountWeeksPlayoffs" min={1} max={6} type="number" step="1" value={amountWeeksPlayoffs} disabled={!toggle} onChange={e => setAmountWeeksPlayoffs((parseInt(e.target.value, 10)))} />
                    <Label for="amountWeeksRegularSeason">Note: 1 finals game will be added to the end of the playoff weeks.</Label>
                    <br />
                    <Label for="weekOneDateStart">Week 1 Start Date</Label>
                    <DateTimePicker id='weekOneDateStart' name='signupDates' disabled={!toggle} onChange={setWeekOneDateStart} value={weekOneDateStart} />
                    <br />
                    <Button color="primary" size="lg" block disabled={!toggle} onClick={createWeeks}>Create Weeks</Button>
                    <hr />
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <StepButtons step={4} {...props} disabled={!(amountRegSeasonWeeksCreated > 0)} />
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default CreateWeeks