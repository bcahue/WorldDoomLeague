import * as React from 'react';
import AuthorizeRoute from '../api-authorization/AuthorizeRoute';
import Roles from '../api-authorization/Roles'
import NewSeasonWizard from './newseasonwizard/NewSeasonWizard';
import ProcessGameWizard from './processgamewizard/ProcessGameWizard';
import UndoGame from './undogame/UndoGame';
import ForfeitGame from './forfeitgame/ForfeitGame';
import ScheduleGames from './schedulegames/ScheduleGames';
import CreatePlayoffGame from './createplayoffgame/CreatePlayoffGame';
import SelectHomefieldMaps from './selecthomefieldmaps/SelectHomefieldMaps';
import TradePlayerToTeam from './tradeplayertoteam/TradePlayerToTeam';
import TradePlayerToFreeAgency from './tradeplayertofreeagency/TradePlayerToFreeAgency';
import PromotePlayerToCaptain from './promoteplayertocaptain/PromotePlayerToCaptain';
import ReverseLastTrade from './reverselasttrade/ReverseLastTrade';
import DeletePlayoffGame from './deleteplayoffgame/DeletePlayoffGame';
import EditPlayers from './editplayers/EditPlayers';
import EditTeams from './editteams/EditTeams';

export default () => (
    <React.Fragment>
        <AuthorizeRoute exact path='/admin/newseasonwizard' component={NewSeasonWizard} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/processgamewizard' component={ProcessGameWizard} componentroles={[Roles.Admin, Roles.StatsRunner]} />
        <AuthorizeRoute exact path='/admin/undogame' component={UndoGame} componentroles={[Roles.Admin, Roles.StatsRunner]} />
        <AuthorizeRoute exact path='/admin/forfeitgame' component={ForfeitGame} componentroles={[Roles.Admin, Roles.StatsRunner]} />
        <AuthorizeRoute exact path='/admin/schedulegames' component={ScheduleGames} componentroles={[Roles.Admin, Roles.StatsRunner]} />
        <AuthorizeRoute exact path='/admin/createplayoffgame' component={CreatePlayoffGame} componentroles={[Roles.Admin, Roles.StatsRunner]} />
        <AuthorizeRoute exact path='/admin/deleteplayoffgame' component={DeletePlayoffGame} componentroles={[Roles.Admin, Roles.StatsRunner]} />
        <AuthorizeRoute exact path='/admin/selecthomefieldmaps' component={SelectHomefieldMaps} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/tradeplayertoteam' component={TradePlayerToTeam} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/tradeplayertofreeagency' component={TradePlayerToFreeAgency} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/promoteplayertocaptain' component={PromotePlayerToCaptain} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/reverselasttrade' component={ReverseLastTrade} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/editplayers' component={EditPlayers} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/editteams' component={EditTeams} componentroles={[Roles.Admin]} />
    </React.Fragment>
);