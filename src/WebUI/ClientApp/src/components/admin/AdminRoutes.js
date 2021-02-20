"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var AuthorizeRoute_1 = require("../api-authorization/AuthorizeRoute");
var Roles_1 = require("../api-authorization/Roles");
var NewSeasonWizard_1 = require("./newseasonwizard/NewSeasonWizard");
var ProcessGameWizard_1 = require("./processgamewizard/ProcessGameWizard");
exports.default = (function () { return (React.createElement(React.Fragment, null,
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/newseasonwizard', component: NewSeasonWizard_1.default, componentroles: [Roles_1.default.Admin] }),
    React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/processgamewizard', component: ProcessGameWizard_1.default, componentroles: [Roles_1.default.Admin, Roles_1.default.StatsRunner] }))); });
//# sourceMappingURL=AdminRoutes.js.map