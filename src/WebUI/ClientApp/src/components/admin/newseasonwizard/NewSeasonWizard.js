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
var WelcomeStep_1 = require("./WelcomeStep");
var reactstrap_1 = require("reactstrap");
var reactstrap_2 = require("reactstrap");
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
    return (React.createElement(reactstrap_2.Container, null,
        React.createElement("h3", null, "New Season Wizard"),
        React.createElement(reactstrap_2.Jumbotron, null,
            React.createElement(react_step_wizard_1.default, { onStepChange: onStepChange, isHashEnabled: true, 
                //transitions={state.transitions}
                instance: setInstance },
                React.createElement(WelcomeStep_1.default, { hashKey: 'Welcome', update: updateForm }),
                React.createElement(Second, { form: state.form }),
                React.createElement(Progress, null),
                null /* will be ignored */,
                React.createElement(Last, { hashKey: 'Complete' })))));
};
exports.default = NewSeasonWizard;
/**
 * Stats Component - to illustrate the possible functions
 * Could be used for nav buttons or overview
 */
var Stats = function (_a) {
    var currentStep = _a.currentStep, firstStep = _a.firstStep, goToStep = _a.goToStep, lastStep = _a.lastStep, nextStep = _a.nextStep, previousStep = _a.previousStep, totalSteps = _a.totalSteps, step = _a.step;
    return (React.createElement("div", null,
        React.createElement("hr", null),
        step > 1 &&
            React.createElement("button", { className: 'btn btn-default btn-block', onClick: previousStep }, "Go Back"),
        step < totalSteps ?
            React.createElement("button", { className: 'btn btn-primary btn-block', onClick: nextStep }, "Continue")
            :
                React.createElement("button", { className: 'btn btn-success btn-block', onClick: nextStep }, "Finish")));
};
/** Steps */
var Second = function (props) {
    var validate = function () {
        if (confirm('Are you sure you want to go back?')) { // eslint-disable-line
            props.previousStep();
        }
    };
    return (React.createElement("div", null,
        props.form.firstname && React.createElement("h3", null,
            "Hey ",
            props.form.firstname,
            "!"),
        "I've added validation to the previous button.",
        React.createElement(Stats, __assign({ step: 2 }, props, { previousStep: validate }))));
};
var Progress = function (props) {
    var _a = react_1.useState({
        timeout: null,
    }), state = _a[0], updateState = _a[1];
    react_1.useEffect(function () {
        var timeout = state.timeout;
        if (props.isActive && !timeout) {
            updateState({
                timeout: setTimeout(function () {
                    props.nextStep();
                }, 3000),
            });
        }
        else if (!props.isActive && timeout) {
            clearTimeout(timeout);
            updateState({
                timeout: null
            });
        }
    });
    return (React.createElement("div", null,
        React.createElement("p", { className: 'text-center' }, "Automated Progress..."),
        React.createElement(reactstrap_1.Progress, { value: "25" }, "25%")));
};
var Last = function (props) {
    var submit = function () {
        alert('You did it! Yay!'); // eslint-disable-line
    };
    return (React.createElement("div", null,
        React.createElement("div", { className: 'text-center' },
            React.createElement("h3", null, "This is the last step in this example!"),
            React.createElement("hr", null)),
        React.createElement(Stats, __assign({ step: 4 }, props, { nextStep: submit }))));
};
//# sourceMappingURL=NewSeasonWizard.js.map