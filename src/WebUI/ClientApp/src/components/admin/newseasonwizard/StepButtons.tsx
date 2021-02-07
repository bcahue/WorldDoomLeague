import { Progress as ProgressBar } from 'reactstrap';
import { Jumbotron, Container, Row, Col, Button } from 'reactstrap';
import * as React from 'react';

/* eslint react/prop-types: 0 */



const StepButtons = ({
    currentStep,
    firstStep,
    nextStep,
    previousStep,
    totalSteps,
    step,
    disabled
}) => (
    <Row>
        <Col sm="12" md={{ size: 6, offset: 3 }}>
            <hr />
            { step < totalSteps ?
                <Button color="secondary" size="lg" block onClick={nextStep} disabled={disabled}>Continue</Button>
                :
                <Button color="secondary" size="lg" block onClick={nextStep} disabled={disabled}>Finish</Button>
            }
        </Col>
    </Row>
);

export default StepButtons