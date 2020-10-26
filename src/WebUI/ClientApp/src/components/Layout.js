"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var reactstrap_1 = require("reactstrap");
var NavMenu_1 = require("./NavMenu");
function Layout(props) {
    return (React.createElement(React.Fragment, null,
        React.createElement(NavMenu_1.default, null),
        React.createElement(reactstrap_1.Container, null, props.children)));
}
exports.default = Layout;
//# sourceMappingURL=Layout.js.map