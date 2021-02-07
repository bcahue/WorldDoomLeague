"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var reactstrap_1 = require("reactstrap");
var React = require("react");
/* eslint react/prop-types: 0 */
var StepButtons = function (_a) {
    var currentStep = _a.currentStep, firstStep = _a.firstStep, nextStep = _a.nextStep, previousStep = _a.previousStep, totalSteps = _a.totalSteps, step = _a.step, disabled = _a.disabled;
    return (React.createElement(reactstrap_1.Row, null,
        React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
            React.createElement("hr", null),
            step < totalSteps ?
                React.createElement(reactstrap_1.Button, { color: "secondary", size: "lg", block: true, onClick: nextStep, disabled: disabled }, "Continue")
                :
                    React.createElement(reactstrap_1.Button, { color: "secondary", size: "lg", block: true, onClick: nextStep, disabled: disabled }, "Finish"))));
};
exports.default = StepButtons;
//# sourceMappingURL=StepButtons.js.map