import StepButtons from './StepButtons'
import * as React from 'react';
import { Label, Input, Row, Col, FormText } from 'reactstrap';
import PlayerList from './PlayerList';

const AddPlayers = props => {
    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    return (
        <React.Fragment>
        <Row>
            <Col sm="12" md={{ size: 6, offset: 3 }}>
                <h3 className='text-center'>Add Signups</h3>
                <p>This next step will add player signups. This will allow you to search current players, and add a new player if they are not found.</p>
            </Col>
        </Row>
        <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <PlayerList {...props} onChange={update} />
                    <StepButtons step={2} {...props} disabled={props.form.players && (!(props.form.players.length > 24) )}/>
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default AddPlayers