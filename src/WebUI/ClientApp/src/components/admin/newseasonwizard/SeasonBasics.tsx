import StepButtons from './StepButtons'
import * as React from 'react';
import { Label, Input, Row, Col, FormText } from 'reactstrap';
import SeasonList from './SeasonList';
import EngineList from './EngineList';
import WadList from './WadList';

const SeasonBasics = props => {
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
                <SeasonList {...props} onChange={update} />
            </Col>
            <Col sm="4">
                <WadList {...props} onChange={update} />
            </Col>
        </Row>
        <Row>
            <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <StepButtons step={1} {...props} disabled={!props.form.engine || !props.form.wad || !props.form.season }/>
            </Col>
        </Row>
        </React.Fragment>
    );
};

export default SeasonBasics