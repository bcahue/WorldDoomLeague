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
var reactstrap_1 = require("reactstrap");
var PlayerList_1 = require("./PlayerList");
var AddPlayers = function (props) {
    var update = function (e) {
        props.update(e.target.name, e.target.value);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Add Signups"),
                React.createElement("p", null, "This next step will add player signups. This will allow you to search current players, and add a new player if they are not found."))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(PlayerList_1.default, __assign({}, props, { onChange: update })),
                React.createElement(StepButtons_1.default, __assign({ step: 2 }, props, { disabled: !(props.players > 24) }))))));
};
exports.default = AddPlayers;
//# sourceMappingURL=AddPlayers.js.map