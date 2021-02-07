import StepButtons from './StepButtons'
import * as React from 'react';
import { useState } from 'react';
import { Label, Input, Row, Col, Card, CardBody, CardTitle, CardSubtitle, CardText } from 'reactstrap';

const AddTeams = props => {
    const [teamList, setTeamList] = useState([{
        teamName: null,

    }]);
    const [amountTeams, setAmountTeams] = useState(6);
    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    return (
        <React.Fragment>
            <Row>
                <Col xs="6" sm="4">
                    <Label for="amountTeams">Amount of teams</Label>
                    <Input placeholder="Amount" name="amountTeams" min={4} max={512} type="number" step="1" value={amountTeams} onChange={e => setAmountTeams(parseInt(e.target.value, 10))} />
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Add Teams</h3>
                    <p>This step will add the teams that will play during the season.</p>
                    {teamList.map((team, index) => (
                        <Card>
                            <CardBody>
                                <CardTitle tag="h5">TeamName</CardTitle>
                                <Input type="text" value={team.teamName} placeholder="Odamex v0.8.3" onChange={e => setEngineFormName(e.target.value)} />
                            </CardBody>
                        </Card>
                        ))}
                </Col>
             </Row>
        <Row>
            <Col sm="12" md={{ size: 6, offset: 3 }}>
                <StepButtons step={3} {...props} disabled={!(props.players > 24) }/>
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default AddTeams