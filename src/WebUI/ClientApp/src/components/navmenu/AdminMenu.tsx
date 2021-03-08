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
                            <div>
                                <DropdownItem divider />
                                <DropdownItem header>
                                        Manage games
                                </DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/undogame">Undo Game</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/forfeitgame">Forfeit Game</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/schedulegames">Schedule Games</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/createplayoffgames">Create Playoff Game</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/deletegame">Delete Game</NavLink>
                                    </NavItem>
                                </DropdownItem>
                            </div>
                        )}
                        {showAdmin && (
                            <div>
                                <DropdownItem divider />
                                <DropdownItem header>
                                    Manage Rosters
                                </DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/tradeplayertoteam">Trade Player To Team</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/tradeplayertofreeagency">Trade Player To Free Agency</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/promoteplayertocaptain">Promote Player to Captain</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/reverselasttrade">Reverse Last Trade</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem header>
                                    Modify League Data
                                </DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/editseasons">Edit Seasons</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/editplayers">Edit Players</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/editteams">Edit Teams</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/editmaps">Edit Maps</NavLink>
                                    </NavItem>
                                </DropdownItem>
                                <DropdownItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/admin/selecthomefieldmaps">Select Homefield Maps</NavLink>
                                    </NavItem>
                                </DropdownItem>
                            </div>
                        )}
                    </DropdownMenu>
                </UncontrolledDropdown>
                )}
        </React.Fragment>
    );
}

export default AdminMenu