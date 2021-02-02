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
var SeasonList_1 = require("./SeasonList");
var EngineList_1 = require("./EngineList");
var WadList_1 = require("./WadList");
var WelcomeStep = function (props) {
    var update = function (e) {
        props.update(e.target.name, e.target.value);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "New Season Basics"),
                React.createElement("p", null, "This first step will fill out the basic details for the new season."))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { xs: "6", sm: "4" },
                React.createElement(EngineList_1.default, __assign({}, props, { onChange: update }))),
            React.createElement(reactstrap_1.Col, { xs: "6", sm: "4" },
                React.createElement(reactstrap_1.Label, { for: 'seasonname' }, "Season Name"),
                React.createElement(reactstrap_1.Input, { type: 'text', className: 'form-control', id: 'seasonname', name: 'seasonname', placeholder: 'Season Name', onChange: update }),
                React.createElement(reactstrap_1.FormText, { color: "muted" }, "Here is a list of current and former seasons for convenience."),
                React.createElement(SeasonList_1.default, null)),
            React.createElement(reactstrap_1.Col, { sm: "4" },
                React.createElement(WadList_1.default, __assign({}, props, { onChange: update })))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(StepButtons_1.default, __assign({ step: 1 }, props))))));
};
exports.default = WelcomeStep;
//# sourceMappingURL=WelcomeStep.js.map