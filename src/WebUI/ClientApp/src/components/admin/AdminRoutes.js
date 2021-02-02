"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var AuthorizeRoute_1 = require("../api-authorization/AuthorizeRoute");
var Roles_1 = require("../api-authorization/Roles");
var NewSeasonWizard_1 = require("./newseasonwizard/NewSeasonWizard");
exports.default = (function () { return (React.createElement(AuthorizeRoute_1.default, { exact: true, path: '/admin/newseasonwizard', component: NewSeasonWizard_1.default, componmentroles: [Roles_1.default.Admin] })); });
//# sourceMappingURL=AdminRoutes.js.map