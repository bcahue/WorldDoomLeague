"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var react_1 = require("react");
var React = require("react");
var reactstrap_1 = require("reactstrap");
var react_router_dom_1 = require("react-router-dom");
require("./NavMenu.css");
function NavMenu() {
    var _a = react_1.useState(false), isOpen = _a[0], setIsOpen = _a[1];
    function toggle() {
        setIsOpen(!isOpen);
    }
    return (React.createElement("header", null,
        React.createElement(reactstrap_1.Navbar, { className: "navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3", light: true },
            React.createElement(reactstrap_1.Container, null,
                React.createElement(reactstrap_1.NavbarBrand, { tag: react_router_dom_1.Link, to: "/" }, "World Doom League"),
                React.createElement(reactstrap_1.NavbarToggler, { onClick: toggle, className: "mr-2" }),
                React.createElement(reactstrap_1.Collapse, { className: "d-sm-inline-flex flex-sm-row-reverse", isOpen: isOpen, navbar: true },
                    React.createElement("ul", { className: "navbar-nav flex-grow" },
                        React.createElement(reactstrap_1.NavItem, null,
                            React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/" }, "Home")),
                        React.createElement(reactstrap_1.NavItem, null,
                            React.createElement(reactstrap_1.NavLink, { tag: react_router_dom_1.Link, className: "text-dark", to: "/leaderboard-all-time" }, "Leaderboards (All Time)"))))))));
}
exports.default = NavMenu;
//# sourceMappingURL=NavMenu.js.map