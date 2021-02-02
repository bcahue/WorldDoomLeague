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
}) => (
    <Row>
        <Col sm="12" md={{ size: 6, offset: 3 }}>
            <hr />
            { step > 1 &&
                <Button color="primary" size="lg" block onClick={previousStep}>Go Back</Button>
            }
            { step < totalSteps ?
                <Button color="secondary" size="lg" block onClick={nextStep}>Continue</Button>
                :
                <Button color="secondary" size="lg" block onClick={nextStep}>Finish</Button>
            }
        </Col>
    </Row>
);

export default StepButtons