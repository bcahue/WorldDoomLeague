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
var StepButtons_1 = require("./StepButtons");
var React = require("react");
var react_1 = require("react");
var reactstrap_1 = require("reactstrap");
var WorldDoomLeague_1 = require("../../../WorldDoomLeague");
var react_datetime_picker_1 = require("react-datetime-picker");
var state_1 = require("../../../state");
var CreateWeeks = function (props) {
    var _a = react_1.useState(5), amountWeeksRegularSeason = _a[0], setAmountWeeksRegularSeason = _a[1];
    var _b = react_1.useState(1), amountWeeksPlayoffs = _b[0], setAmountWeeksPlayoffs = _b[1];
    var _c = react_1.useState(null), weekOneDateStart = _c[0], setWeekOneDateStart = _c[1];
    var _d = react_1.useState(0), amountRegSeasonWeeksCreated = _d[0], setAmountRegSeasonWeeksCreated = _d[1];
    var _e = react_1.useState(true), toggle = _e[0], setToggle = _e[1];
    var createWeeks = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.SeasonsClient();
                    command = new WorldDoomLeague_1.CreateSeasonWeeksCommand;
                    command.seasonId = props.form.season;
                    command.numWeeksPlayoffs = amountWeeksPlayoffs;
                    command.numWeeksRegularSeason = amountWeeksRegularSeason;
                    command.weekOneDateStart = weekOneDateStart;
                    return [4 /*yield*/, client.createWeeks(command)];
                case 1:
                    response = _a.sent();
                    setAmountRegSeasonWeeksCreated(response);
                    props.update("regSeasonWeeks", response);
                    setToggle(false);
                    return [3 /*break*/, 3];
                case 2:
                    e_1 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_1.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var update = function (e) {
        props.update(e.target.name, e.target.value);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Add Weeks"),
                React.createElement("p", null, "This step will add the weeks which games will be played."),
                React.createElement(reactstrap_1.Label, { for: "amountWeeksRegularSeason" }, "Regular season weeks"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", name: "amountWeeksRegularSeason", min: 4, max: 26, type: "number", step: "1", value: amountWeeksRegularSeason, disabled: !toggle, onChange: function (e) { return setAmountWeeksRegularSeason((parseInt(e.target.value, 10))); } }),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Label, { for: "amountWeeksPlayoffs" }, "Playoff weeks"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", name: "amountWeeksPlayoffs", min: 1, max: 6, type: "number", step: "1", value: amountWeeksPlayoffs, disabled: !toggle, onChange: function (e) { return setAmountWeeksPlayoffs((parseInt(e.target.value, 10))); } }),
                React.createElement(reactstrap_1.Label, { for: "amountWeeksRegularSeason" }, "Note: 1 finals game will be added to the end of the playoff weeks."),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Label, { for: "weekOneDateStart" }, "Week 1 Start Date"),
                React.createElement(react_datetime_picker_1.default, { id: 'weekOneDateStart', name: 'signupDates', disabled: !toggle, onChange: setWeekOneDateStart, value: weekOneDateStart }),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !toggle, onClick: createWeeks }, "Create Weeks"),
                React.createElement("hr", null))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(StepButtons_1.default, __assign({ step: 4 }, props, { disabled: !(amountRegSeasonWeeksCreated > 0) }))))));
};
exports.default = CreateWeeks;
//# sourceMappingURL=CreateWeeks.js.map