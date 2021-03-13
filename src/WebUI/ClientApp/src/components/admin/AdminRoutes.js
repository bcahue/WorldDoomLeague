"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var AuthorizeRoute_1 = require("../api-authorization/AuthorizeRoute");
var Roles_1 = require("../api-authorization/Roles");
var NewSeasonWizard_1 = require("./newseasonwizard/NewSeasonWizard");
var ProcessGameWizard_1 = require("./processgamewizard/ProcessGameWizard");
var UndoGame_1 = require("./undogame/UndoGame");
var ForfeitGame_1 = require("./forfeitgame/ForfeitGame");
var ScheduleGames_1 = require("./schedulegames/ScheduleGames");
var CreatePlayoffGame_1 = require("./createplayoffgame/CreatePlayoffGame");
var SelectHomefieldMaps_1 = require("./selecthomefieldmaps/SelectHomefieldMaps");
var TradePlayerToTeam_1 = require("./tradeplayertoteam/TradePlayerToTeam");
var TradePlayerToFreeAgency_1 = require("./tradeplayertofreeagency/TradePlayerToFreeAgency");
var PromotePlayerToCaptain_1 = require("./promoteplayertocaptain/PromotePlayerToCaptain");
var ReverseLastTrade_1 = require("./reverselasttrade/ReverseLastTrade");
var DeletePlayoffGame_1 = require("./deleteplayoffgame/DeletePlayoffGame");
var EditPlayers_1 = require("./editplayers/EditPlayers");
var EditTeams_1 = require("./editteams/EditTeams");
exports.default = (function () { return (React.createElement(React.Fragment, null,
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/newseasonwizard', component: NewSeasonWizard_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/processgamewizard', component: ProcessGameWizard_1.default, componentroles: [Roles_1.default.Admin, Roles_1.default.StatsRunner] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/undogame', component: UndoGame_1.default, componentroles: [Roles_1.default.Admin, Roles_1.default.StatsRunner] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/forfeitgame', component: ForfeitGame_1.default, componentroles: [Roles_1.default.Admin, Roles_1.default.StatsRunner] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/schedulegames', component: ScheduleGames_1.default, componentroles: [Roles_1.default.Admin, Roles_1.default.StatsRunner] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/createplayoffgame', component: CreatePlayoffGame_1.default, componentroles: [Roles_1.default.Admin, Roles_1.default.StatsRunner] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/deleteplayoffgame', component: DeletePlayoffGame_1.default, componentroles: [Roles_1.default.Admin, Roles_1.default.StatsRunner] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/selecthomefieldmaps', component: SelectHomefieldMaps_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/tradeplayertoteam', component: TradePlayerToTeam_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/tradeplayertofreeagency', component: TradePlayerToFreeAgency_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/promoteplayertocaptain', component: PromotePlayerToCaptain_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/reverselasttrade', component: ReverseLastTrade_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/editplayers', component: EditPlayers_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/editteams', component: EditTeams_1.default, componentroles: [Roles_1.default.Admin] }))); });
//# sourceMappingURL=AdminRoutes.js.map