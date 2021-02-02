import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './navmenu/NavMenu';
import ErrorMessage from './ErrorMessage';

function Layout(props: { children?: React.ReactNode }) {
    return (
    <React.Fragment>
        <NavMenu />
            <Container>
                <ErrorMessage />
                {props.children}
            </Container>
    </React.Fragment>
    );
}

export default Layout