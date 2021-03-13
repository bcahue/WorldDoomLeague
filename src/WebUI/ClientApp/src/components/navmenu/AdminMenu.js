"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var react_1 = require("react");
var React = require("react");
var Roles_1 = require("../api-authorization/Roles");
var reactstrap_1 = require("reactstrap");
var react_router_dom_1 = require("react-router-dom");
var AuthorizeService_1 = require("../api-authorization/AuthorizeService");
require("./NavMenu.css");
/* eslint-disable no-unused-expressions */
function AdminMenu(props) {
    var _a = react_1.useState(false), showAdminMenu = _a[0], setShowAdminMenu = _a[1];
    var _b = react_1.useState(false), showAdmin = _b[0], setShowAdmin = _b[1];
    var _c = react_1.useState(false), showEditGames = _c[0], setShowEditGames = _c[1];
    function populateState() {
        return __awaiter(this, void 0, void 0, function () {
            var _a, isAuthenticated, user;
            return __generator(this, function (_b) {
                switch (_b.label) {
                    case 0: return [4 /*yield*/, Promise.all([AuthorizeService_1.default.isAuthenticated(), AuthorizeService_1.default.getUser()])];
                    case 1:
                        _a = _b.sent(), isAuthenticated = _a[0], user = _a[1];
                        if (isAuthenticated) {
                            toggleShowAdminMenu(user.role);
                            toggleShowEditGames(user.role);
                            toggleShowAdmin(user.role);
                        }
                        return [2 /*return*/];
                }
            });
        });
    }
    ;
    function toggleShowAdminMenu(roles) {
        if (roles.includes(Roles_1.default.Admin) ||
            roles.includes(Roles_1.default.DemoAdmin) ||
            roles.includes(Roles_1.default.NewsEditor) ||
            roles.includes(Roles_1.default.StatsRunner)) {
            setShowAdminMenu(true);
        }
    }
    function toggleShowEditGames(roles) {
        if (roles.includes(Roles_1.default.Admin) ||
            roles.includes(Roles_1.default.StatsRunner)) {
            setShowEditGames(true);
        }
    }
    function toggleShowAdmin(roles) {
        if (roles.includes(Roles_1.default.Admin)) {
            setShowAdmin(true);
        }
    }
    react_1.useEffect(function () {
        var _subscription = AuthorizeService_1.default.subscribe(function () { return populateState(); });
        populateState();
        return function () {
            AuthorizeService_1.default.unsubscribe(_subscription);
        };
    }, []);
    return (React.createElement(React.Fragment, null, showAdminMenu && (React.createElement(reactstrap_1.UncontrolledDropdown, { nav: true, inNavbar: true },
        React.createElement(reactstrap_1.DropdownToggle, { className: "text-dark", nav: true, caret: true }, "Admin"),
        React.createElement(reactstrap_1.DropdownMenu, { right: true },
            showEditGames && (React.createElement(reactstrap_1.DropdownItem, { header: true }, "Wizards")),
            showEditGames && (React.createElement(reactstrap_1.DropdownItem, { divider: true })),
            showAdmin && (React.createElement(reactstrap_1.DropdownItem, null,
                React.createElement(reactstrap_1.NavItem, null,
                    React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/newseasonwizard" }, "New Season Wizard")))),
            showEditGames && (React.createElement(reactstrap_1.DropdownItem, null,
                React.createElement(reactstrap_1.NavItem, null,
                    React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/processgamewizard" }, "Process Game Wizard")))),
            showEditGames && (React.createElement("div", null,
                React.createElement(reactstrap_1.DropdownItem, { divider: true }),
                React.createElement(reactstrap_1.DropdownItem, { header: true }, "Manage games"),
                React.createElement(reactstrap_1.DropdownItem, { divider: true }),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/undogame" }, "Undo Game"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/forfeitgame" }, "Forfeit Game"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/schedulegames" }, "Schedule Games"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/createplayoffgames" }, "Create Playoff Game"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/deleteplayoffgame" }, "Delete Playoff Game"))))),
            showAdmin && (React.createElement("div", null,
                React.createElement(reactstrap_1.DropdownItem, { divider: true }),
                React.createElement(reactstrap_1.DropdownItem, { header: true }, "Manage Rosters"),
                React.createElement(reactstrap_1.DropdownItem, { divider: true }),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/tradeplayertoteam" }, "Trade Player To Team"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/tradeplayertofreeagency" }, "Trade Player To Free Agency"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/promoteplayertocaptain" }, "Promote Player to Captain"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/reverselasttrade" }, "Reverse Last Trade"))),
                React.createElement(reactstrap_1.DropdownItem, { divider: true }),
                React.createElement(reactstrap_1.DropdownItem, { header: true }, "Modify League Data"),
                React.createElement(reactstrap_1.DropdownItem, { divider: true }),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/editseasons" }, "Edit Seasons"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/editplayers" }, "Edit Players"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/editteams" }, "Edit Teams"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/editmaps" }, "Edit Maps"))),
                React.createElement(reactstrap_1.DropdownItem, null,
                    React.createElement(reactstrap_1.NavItem, null,
                        React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/admin/selecthomefieldmaps" }, "Select Homefield Maps"))))))))));
}
exports.default = AdminMenu;
//# sourceMappingURL=AdminMenu.js.map