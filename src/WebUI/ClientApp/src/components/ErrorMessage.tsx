import { useGlobalState } from '../state';
import * as React from 'react';
import { useEffect, useState } from 'react';
import { Alert, Fade } from 'reactstrap';

const ErrorMessage = () => {
    const [error] = useGlobalState('errorMessage');
    const [hidden, setHidden] = useState<boolean>(true);

    useEffect(() => {
        setHidden(false);
        const timer = setTimeout(() => {
            setHidden(true);
        }, 5000);
        return () => {
            clearTimeout(timer);
        };
    }, [error]);

    return (
        <React.Fragment>
            { error.errors !== undefined && (
                <Fade out={hidden} in={!hidden}>
                    <Alert color="danger" hidden={hidden}>
                        {error.title}
                        <br />
                        HTTP Status: {error.status}
                        <br />
                        {Object.values(error.errors).map((value) => <p>{value}</p>)}
                    </Alert>
                </Fade>)}
        </React.Fragment>
    );
};

export default ErrorMessage