"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var state_1 = require("../state");
var React = require("react");
var react_1 = require("react");
var reactstrap_1 = require("reactstrap");
var ErrorMessage = function () {
    var error = state_1.useGlobalState('errorMessage')[0];
    var _a = react_1.useState(true), hidden = _a[0], setHidden = _a[1];
    react_1.useEffect(function () {
        setHidden(false);
        var timer = setTimeout(function () {
            setHidden(true);
        }, 5000);
        return function () {
            clearTimeout(timer);
        };
    }, [error]);
    return (React.createElement(React.Fragment, null, error.errors !== undefined && (React.createElement(reactstrap_1.Fade, { out: hidden, in: !hidden },
        React.createElement(reactstrap_1.Alert, { color: "danger", hidden: hidden },
            error.title,
            React.createElement("br", null),
            "HTTP Status: ",
            error.status,
            React.createElement("br", null),
            Object.values(error.errors).map(function (value) { return React.createElement("p", null, value); }))))));
};
exports.default = ErrorMessage;
//# sourceMappingURL=ErrorMessage.js.map