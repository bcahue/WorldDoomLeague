"use strict";
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
var React = require("react");
var react_1 = require("react");
var reactstrap_1 = require("reactstrap");
var react_datetime_picker_1 = require("react-datetime-picker");
var state_1 = require("../../../state");
require("bootstrap/dist/css/bootstrap.css");
var WorldDoomLeague_1 = require("../../../WorldDoomLeague");
var SeasonList = function (props) {
    var _a = react_1.useState(false), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState([]), data = _b[0], setData = _b[1];
    var _c = react_1.useState(""), seasonName = _c[0], setSeasonName = _c[1];
    var _d = react_1.useState(0), newSeasonId = _d[0], setNewSeasonId = _d[1];
    var _e = react_1.useState(null), signupDate = _e[0], setSignupDate = _e[1];
    var _f = react_1.useState(false), isOpen = _f[0], setIsOpen = _f[1];
    var toggle = function () { return setIsOpen(!isOpen); };
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
            var client, response, data_1, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        setLoading(true);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 4, , 5]);
                        client = new WorldDoomLeague_1.SeasonsClient();
                        return [4 /*yield*/, client.get()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        return [4 /*yield*/, response.seasonList];
                    case 3:
                        data_1 = _a.sent();
                        setData(data_1);
                        return [3 /*break*/, 5];
                    case 4:
                        e_1 = _a.sent();
                        state_1.setErrorMessage(JSON.parse(e_1.response));
                        return [3 /*break*/, 5];
                    case 5:
                        setLoading(false);
                        return [2 /*return*/];
                }
            });
        }); };
        fetchData();
    }, [newSeasonId]);
    var handleSeasonChange = function (name, value) {
        setNewSeasonId(value);
        props.update(name, value);
    };
    var handleSubmit = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_2;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.SeasonsClient();
                    command = new WorldDoomLeague_1.CreateSeasonCommand;
                    command.seasonName = seasonName;
                    command.wadId = props.form.wad;
                    command.enginePlayed = props.form.engine;
                    command.seasonDateStart = new Date(signupDate);
                    return [4 /*yield*/, client.create(command)];
                case 1:
                    response = _a.sent();
                    setNewSeasonId(response);
                    setSeasonName('');
                    handleSeasonChange("season", response);
                    return [3 /*break*/, 3];
                case 2:
                    e_2 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_2.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var renderNewSeasonForm = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.Label, { for: 'seasonname' }, "Season Name"),
            React.createElement(reactstrap_1.Input, { type: 'text', className: 'form-control', id: 'seasonname', name: 'seasonname', placeholder: 'Season Name', onChange: function (e) { return setSeasonName(e.target.value); } }),
            React.createElement(reactstrap_1.Label, { for: 'signupDates' }, "Signups Begin"),
            React.createElement(react_datetime_picker_1.default, { id: 'signupDates', name: 'signupDates', onChange: setSignupDate, value: signupDate }),
            React.createElement(reactstrap_1.FormText, { color: "muted" }, "Here is a list of current and former seasons for convenience."),
            React.createElement(reactstrap_1.Button, { color: "primary", onClick: toggle, style: { marginBottom: '1rem' } }, "Toggle"),
            React.createElement(reactstrap_1.Collapse, { isOpen: isOpen },
                React.createElement(reactstrap_1.ListGroup, null, renderSeasonList())),
            React.createElement("br", null),
            React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !seasonName || !props.form.wad || !props.form.engine || !signupDate, onClick: handleSubmit }, "Create New Season")));
    };
    // create a list for each season.
    var renderSeasonList = function () {
        var listArray = [];
        if (!loading) {
            var seasonObject = data;
            if (seasonObject.length > 0) {
                seasonObject.forEach(function (value) {
                    listArray.push(React.createElement(reactstrap_1.ListGroupItem, { key: value.id },
                        value.seasonName,
                        React.createElement("br", null),
                        "Signups Begin: ",
                        new Intl.DateTimeFormat('default', { dateStyle: 'full', timeStyle: 'long' }).format(new Date(value.dateStart))));
                });
            }
            else {
                listArray.push(React.createElement(reactstrap_1.ListGroupItem, { key: "none" }, "There are currently no seasons recorded in the system."));
            }
        }
        else {
            listArray.push(React.createElement(reactstrap_1.ListGroupItem, { key: "spinner" },
                React.createElement(reactstrap_1.Spinner, { size: "sm", color: "primary" })));
        }
        return (listArray);
    };
    return (React.createElement(React.Fragment, null, renderNewSeasonForm()));
};
exports.default = SeasonList;
//# sourceMappingURL=SeasonList.js.map