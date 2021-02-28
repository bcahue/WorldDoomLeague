import { useState, useEffect } from 'react';
import * as React from 'react';
import Roles from '../api-authorization/Roles';
import {
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
    NavLink,
    NavItem
} from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from '../api-authorization/AuthorizeService';
import './NavMenu.css';

/* eslint-disable no-unused-expressions */

function AdminMenu(props) {
    const [showAdminMenu, setShowAdminMenu] = useState(false);
    const [showAdmin, setShowAdmin] = useState(false);
    const [showEditGames, setShowEditGames] = useState(false);

    async function populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()]);

        if (isAuthenticated) {
            toggleShowAdminMenu(user.role);
            toggleShowEditGames(user.role);
            toggleShowAdmin(user.role);
        }
    };

    function toggleShowAdminMenu(roles) {
        if (roles.includes(Roles.Admin) ||
            roles.includes(Roles.DemoAdmin) ||
            roles.includes(Roles.NewsEditor) ||
            roles.includes(Roles.StatsRunner)) {
            setShowAdminMenu(true);
        }
    }

    function toggleShowEditGames(roles) {
        if (roles.includes(Roles.Admin) ||
            roles.includes(Roles.StatsRunner)) {
            setShowEditGames(true);
        }
    }

    function toggleShowAdmin(roles) {
        if (roles.includes(Roles.Admin)) {
            setShowAdmin(true);
        }
    }

    useEffect(() => {
        var _subscription = authService.subscribe(() => populateState());
        populateState();
        return () => {
            authService.unsubscribe(_subscription);
        }
    }, []);

    return (
        <React.Fragment>
            {showAdminMenu && (
                <UncontrolledDropdown nav inNavbar>
                    <DropdownToggle className="text-dark" nav caret>
                        Admin
                </DropdownToggle>
                    <DropdownMenu right>
                        {showEditGames && (
                            <DropdownItem header>
                                Wizards
                            </DropdownItem>
                        )}
                        {showEditGames && (
                            <DropdownItem divider />
                        )}
                        {showAdmin && (
                            <DropdownItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/admin/newseasonwizard">New Season Wizard</NavLink>
                                </NavItem>
                            </DropdownItem>
                        )}
                        {showEditGames && (
                            <DropdownItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/admin/processgamewizard">Process Game Wizard</NavLink>
                                </NavItem>
                            </DropdownItem>
                        )}
                        {showEditGames && (
                            <DropdownItem divider />
                        )}
                        {showEditGames && (
                            <DropdownItem header>
                                Modify League Data
                            </DropdownItem>
                        )}
                        {showAdmin && (
                            <DropdownItem>
                                Edit Seasons
                            </DropdownItem>
                        )}
                        {showAdmin && (
                            <DropdownItem>
                                Edit Players
                            </DropdownItem>
                        )}
                        {showAdmin && (
                            <DropdownItem>
                                Edit Teams
                            </DropdownItem>
                        )}
                        {showEditGames && (
                            <DropdownItem>
                                Edit Games
                            </DropdownItem>
                        )}
                    </DropdownMenu>
                </UncontrolledDropdown>
                )}
        </React.Fragment>
    );
}

export default AdminMenu