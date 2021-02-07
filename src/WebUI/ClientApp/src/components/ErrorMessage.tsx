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
            {!(Object.keys(error).length === 0 && error.constructor === Object) && ( // javascript checking for empty object...
                <Fade in={!hidden}>
                    <Alert color="danger" hidden={hidden}>
                        {error.title}
                        <br />
                        HTTP Status: {error.status}
                        <br />
                        {error.errors !== undefined && (Object.values(error.errors).map((value) => <p>{value}</p>))}
                        {error.detail && (<p>{error.detail}</p>)}
                    </Alert>
                </Fade>)}
        </React.Fragment>
    );
};

export default ErrorMessage