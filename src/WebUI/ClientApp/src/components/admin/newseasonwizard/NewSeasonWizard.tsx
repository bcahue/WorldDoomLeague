import { Fragment, useState, useEffect } from 'react';
import * as React from 'react';
import StepWizard from 'react-step-wizard';
import SeasonBasics from './SeasonBasics';
import AddPlayers from './AddPlayers';
import AddTeams from './AddTeams';
import CreateWeeks from './CreateWeeks';
import RegisterDraft from './RegisterDraft';
import CreateGames from './CreateGames';
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
                    <RegisterDraft form={state.form} update={updateForm} />
                    <CreateWeeks form={state.form} update={updateForm} />
                </StepWizard>
            </Jumbotron>
        </Container>
    );
};

export default NewSeasonWizard;