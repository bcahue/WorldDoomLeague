"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
var StepButtons_1 = require("./StepButtons");
var React = require("react");
var react_1 = require("react");
var reactstrap_1 = require("reactstrap");
var AddTeams = function (props) {
    var _a = react_1.useState([{}]), teamList = _a[0], setTeamList = _a[1];
    var _b = react_1.useState(6), amountTeams = _b[0], setAmountTeams = _b[1];
    var update = function (e) {
        props.update(e.target.name, e.target.value);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { xs: "6", sm: "4" },
                React.createElement(reactstrap_1.Label, { for: "amountTeams" }, "Amount of teams"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", name: "amountTeams", min: 4, max: 512, type: "number", step: "1", value: amountTeams, onChange: function (e) { return setAmountTeams(parseInt(e.target.value, 10)); } }))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Add Teams"),
                React.createElement("p", null, "This step will add the teams that will play during the season."))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(StepButtons_1.default, __assign({ step: 3 }, props, { disabled: !(props.players > 24) }))))));
};
exports.default = AddTeams;
//# sourceMappingURL=AddTeams.js.map