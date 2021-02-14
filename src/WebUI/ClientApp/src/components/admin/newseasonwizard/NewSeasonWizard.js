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
var react_1 = require("react");
var React = require("react");
var react_step_wizard_1 = require("react-step-wizard");
var SeasonBasics_1 = require("./SeasonBasics");
var AddPlayers_1 = require("./AddPlayers");
var AddTeams_1 = require("./AddTeams");
var CreateWeeks_1 = require("./CreateWeeks");
var RegisterDraft_1 = require("./RegisterDraft");
var reactstrap_1 = require("reactstrap");
/* eslint react/prop-types: 0 */
/**
 * A basic demonstration of how to use the step wizard
 */
var NewSeasonWizard = function () {
    var _a = react_1.useState({
        form: {}
        // demo: true, // uncomment to see more
    }), state = _a[0], updateState = _a[1];
    var updateForm = function (key, value) {
        var form = state.form;
        form[key] = value;
        updateState(__assign(__assign({}, state), { form: form }));
    };
    // Do something on step change
    var onStepChange = function (stats) {
        // console.log(stats);
    };
    var setInstance = function () { return updateState(__assign({}, state)); };
    return (React.createElement(reactstrap_1.Container, null,
        React.createElement("h3", null, "New Season Wizard"),
        React.createElement(reactstrap_1.Jumbotron, null,
            React.createElement(react_step_wizard_1.default, { onStepChange: onStepChange, isHashEnabled: false, 
                //transitions={state.transitions}
                instance: setInstance },
                React.createElement(SeasonBasics_1.default, { form: state.form, update: updateForm }),
                React.createElement(AddPlayers_1.default, { form: state.form, update: updateForm }),
                React.createElement(AddTeams_1.default, { form: state.form, update: updateForm }),
                React.createElement(RegisterDraft_1.default, { form: state.form, update: updateForm }),
                React.createElement(CreateWeeks_1.default, { form: state.form, update: updateForm })))));
};
exports.default = NewSeasonWizard;
//# sourceMappingURL=NewSeasonWizard.js.map