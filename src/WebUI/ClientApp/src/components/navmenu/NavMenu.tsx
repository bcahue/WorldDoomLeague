import { useState, useEffect } from 'react';
import * as React from 'react';
import {
    Collapse,
    Container,
    Nav,
    Navbar,
    NavbarBrand,
    NavbarToggler,
    NavItem,
    NavLink,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem} from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from '../api-authorization/LoginMenu';
import AdminMenu from './AdminMenu'
import './NavMenu.css';

function NavMenu(props) {
    const [isOpen, setIsOpen] = useState(false);
    
    function toggle() {
        setIsOpen(!isOpen);
    };

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/">World Doom League</NavbarBrand>
                    <NavbarToggler onClick={toggle} className="mr-2"/>
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={isOpen} navbar>
                        <Nav className="navbar-nav flex-grow container-fluid" navbar>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/matches">Matches</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/results">Results</NavLink>
                            </NavItem>
                            <UncontrolledDropdown nav inNavbar>
                                <DropdownToggle className="text-dark" nav caret>
                                    Seasons
                                </DropdownToggle>
                                <DropdownMenu right>
                                <DropdownItem header>
                                    Current Seasons
                                </DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem>
                                    WDL Winter 2019
                                </DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem>
                                        Previous Seasons
                                </DropdownItem>
                                <DropdownItem divider />
                                </DropdownMenu>
                            </UncontrolledDropdown>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/stats">Stats</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/players">Players</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/demos">Demos</NavLink>
                            </NavItem>
                            <AdminMenu />
                            <LoginMenu />
                        </Nav>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default NavMenu