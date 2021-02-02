import StepButtons from './StepButtons'
import * as React from 'react';
import { Label, Input, Row, Col, FormText } from 'reactstrap';
import SeasonList from './SeasonList';
import EngineList from './EngineList';
import WadList from './WadList';

const WelcomeStep = props => {
    const update = (e) => {
        props.update(e.target.name, e.target.value);
    };

    return (
        <React.Fragment>
        <Row>
            <Col sm="12" md={{ size: 6, offset: 3 }}>
                <h3 className='text-center'>New Season Basics</h3>
                <p>This first step will fill out the basic details for the new season.</p>
            </Col>
        </Row>
        <Row>
            <Col xs="6" sm="4">
                <EngineList {...props} onChange={update} />
            </Col>
            <Col xs="6" sm="4">
                <Label for='seasonname'>Season Name</Label>
                <Input type='text' className='form-control' id='seasonname' name='seasonname' placeholder='Season Name' onChange={update} />
                <FormText color="muted">Here is a list of current and former seasons for convenience.</FormText>
                <SeasonList />
            </Col>
            <Col sm="4">
                <WadList {...props} onChange={update} />
            </Col>
        </Row>
        <Row>
            <Col sm="12" md={{ size: 6, offset: 3 }}>
                <StepButtons step={1} {...props} />
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default WelcomeStep