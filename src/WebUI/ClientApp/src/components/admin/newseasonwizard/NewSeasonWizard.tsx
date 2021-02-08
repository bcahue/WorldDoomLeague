import { Fragment, useState, useEffect } from 'react';
import * as React from 'react';
import StepWizard from 'react-step-wizard';
import SeasonBasics from './SeasonBasics';
import AddPlayers from './AddPlayers';
import AddTeams from './AddTeams';
import CreateWeeks from './CreateWeeks';
import RegisterDraft from './RegisterDraft';
import { Progress as ProgressBar } from 'reactstrap';
import { Jumbotron, Container, Row, Col } from 'reactstrap';

/* eslint react/prop-types: 0 */

/**
 * A basic demonstration of how to use the step wizard
 */
const NewSeasonWizard = () => {
    const [state, updateState] = useState({
        form: {}
        // demo: true, // uncomment to see more
    });

    const updateForm = (key, value) => {
        const { form } = state;

        form[key] = value;
        updateState({
            ...state,
            form,
        });
    };

    // Do something on step change
    const onStepChange = (stats) => {
        // console.log(stats);
    };

    const setInstance = () => updateState({
        ...state
    });

    return (
        <Container>
            <h3>New Season Wizard</h3>
            <Jumbotron>
                <StepWizard
                    onStepChange={onStepChange}
                    isHashEnabled={false}
                    //transitions={state.transitions}
                    instance={setInstance}
                >
                    <SeasonBasics form={state.form} update={updateForm} />
                    <AddPlayers form={state.form} update={updateForm} />
                    <AddTeams form={state.form} update={updateForm} />
                </StepWizard>
            </Jumbotron>
        </Container>
    );
};

export default NewSeasonWizard;

/**
 * Stats Component - to illustrate the possible functions
 * Could be used for nav buttons or overview
 */
const Stats = ({
    currentStep,
    firstStep,
    goToStep,
    lastStep,
    nextStep,
    previousStep,
    totalSteps,
    step,
}) => (
    <div>
        <hr />
        { step > 1 &&
            <button className='btn btn-default btn-block' onClick={previousStep}>Go Back</button>
        }
        { step < totalSteps ?
            <button className='btn btn-primary btn-block' onClick={nextStep}>Continue</button>
            :
            <button className='btn btn-success btn-block' onClick={nextStep}>Finish</button>
        }
    </div>
);

/** Steps */

const Progress = (props) => {
    const [state, updateState] = useState({
        timeout: null,
    });

    useEffect(() => {
        const { timeout } = state;

        if (props.isActive && !timeout) {
            updateState({
                timeout: setTimeout(() => {
                    props.nextStep();
                }, 3000),
            });
        } else if (!props.isActive && timeout) {
            clearTimeout(timeout);
            updateState({
                timeout: null
            });
        }
    });

    return (
        <div>
            <p className='text-center'>Automated Progress...</p>
            <ProgressBar value={"25"}>25%</ProgressBar>
        </div>
    );
};

const Last = (props) => {
    const submit = () => {
        alert('You did it! Yay!') // eslint-disable-line
    };

    return (
        <div>
            <div className={'text-center'}>
                <h3>This is the last step in this example!</h3>
                <hr />
            </div>
            <Stats step={4} {...props} nextStep={submit} />
        </div>
    );
};