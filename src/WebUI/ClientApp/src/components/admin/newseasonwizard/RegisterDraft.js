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
var react_select_1 = require("react-select");
var WorldDoomLeague_1 = require("../../../WorldDoomLeague");
var state_1 = require("../../../state");
var RegisterDraft = function (props) {
    var _a = react_1.useState([{
            nominatedPlayer: null,
            nominatingPlayer: null,
            playerSoldTo: null,
            sellPrice: 0
        }]), draftList = _a[0], setDraftList = _a[1];
    var _b = react_1.useState(3), playersPerTeam = _b[0], setPlayersPerTeam = _b[1];
    var _c = react_1.useState(false), canSubmitDraft = _c[0], setCanSubmitDraft = _c[1];
    var _d = react_1.useState(false), completedDraft = _d[0], setCompletedDraft = _d[1];
    var _e = react_1.useState(true), canCreateDraft = _e[0], setCanCreateDraft = _e[1];
    var createDraftPicks = function () {
        setCanCreateDraft(false);
        var pad_array = function (arr, len, fill) {
            return arr.concat(Array(len).fill(fill)).slice(0, len);
        };
        if (props.form.captains) {
            var maxPlayersDrafted = props.form.captains.length * playersPerTeam;
            var newDraftList = pad_array(draftList, maxPlayersDrafted, {
                nominatedPlayer: null,
                nominatingPlayer: null,
                playerSoldTo: null,
                sellPrice: 0
            });
            var playersPerTeamMinusCaptain_1 = playersPerTeam;
            var newCaptainArray = props.form.captains.map(function (s, _idx) {
                return __assign(__assign({}, s), { playersLeft: playersPerTeamMinusCaptain_1 });
            });
            props.update("captains", newCaptainArray);
            setDraftList(newDraftList);
        }
    };
    var handleNominatingSelected = function (idx, value) {
        var newNominatingPlayer = draftList.map(function (draft, sidx) {
            if (idx !== sidx)
                return draft;
            return __assign(__assign({}, draft), { nominatingPlayer: value.value });
        });
        setDraftList(newNominatingPlayer);
        checkIfFormComplete(newNominatingPlayer);
    };
    // Handle the sell price
    var handleSoldForInput = function (idx, value) {
        var newSellPrice = draftList.map(function (draft, sidx) {
            if (idx !== sidx)
                return draft;
            return __assign(__assign({}, draft), { sellPrice: value });
        });
        setDraftList(newSellPrice);
        checkIfFormComplete(newSellPrice);
    };
    var handleSoldToSelected = function (idx, value) {
        // subtract the playersleft to handle disables
        var newCaptainsArray = props.form.captains.map(function (s, _idx) {
            if (s.value !== value.value)
                return s;
            var left = s.playersLeft - 1;
            return __assign(__assign({}, s), { playersLeft: left });
        });
        // re-enable a captain if they are deselected.
        if (draftList[idx].playerSoldTo !== null) {
            var reenabledOldCaptain = newCaptainsArray.map(function (s, _idx) {
                if (s.value !== draftList[idx].playerSoldTo)
                    return s;
                var left = s.playersLeft + 1;
                return __assign(__assign({}, s), { playersLeft: left });
            });
            var newCaptainList = reenabledOldCaptain.map(function (s, _idx) {
                if (s.playersLeft > 0)
                    return __assign(__assign({}, s), { isdisabled: false });
                return __assign(__assign({}, s), { isdisabled: true });
            });
            props.update("captains", newCaptainList);
        }
        else {
            var newCaptainList = newCaptainsArray.map(function (s, _idx) {
                if (s.playersLeft > 0)
                    return __assign(__assign({}, s), { isdisabled: false });
                return __assign(__assign({}, s), { isdisabled: true });
            });
            props.update("captains", newCaptainList);
        }
        var newCaptainSoldTo = draftList.map(function (draft, sidx) {
            if (idx !== sidx)
                return draft;
            return __assign(__assign({}, draft), { playerSoldTo: value.value });
        });
        setDraftList(newCaptainSoldTo);
        checkIfFormComplete(newCaptainSoldTo);
    };
    var handleNominatedSelected = function (idx, value) {
        var newPlayerArray = props.form.players.map(function (s, _idx) {
            if (s.value !== value.value)
                return s;
            return __assign(__assign({}, s), { isdisabled: true });
        });
        if (draftList[idx].nominatedPlayer !== null) {
            var reenabledOldPlayer = newPlayerArray.map(function (s, _idx) {
                if (s.value !== draftList[idx].nominatedPlayer)
                    return s;
                return __assign(__assign({}, s), { isdisabled: false });
            });
            props.update("players", reenabledOldPlayer);
        }
        else {
            props.update("players", newPlayerArray);
        }
        var newCaptain = draftList.map(function (draft, sidx) {
            if (idx !== sidx)
                return draft;
            return __assign(__assign({}, draft), { nominatedPlayer: value.value });
        });
        setDraftList(newCaptain);
        checkIfFormComplete(newCaptain);
    };
    var createDraft = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, draft, idx, addPick, response, e_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.DraftClient();
                    command = new WorldDoomLeague_1.CreateDraftCommand;
                    draft = [];
                    for (idx = 0; idx < draftList.length; idx++) {
                        addPick = new WorldDoomLeague_1.DraftRequest();
                        addPick.nominatedPlayer = draftList[idx].nominatedPlayer;
                        addPick.nominatingPlayer = draftList[idx].nominatingPlayer;
                        addPick.playerSoldTo = draftList[idx].playerSoldTo;
                        addPick.sellPrice = draftList[idx].sellPrice;
                        draft.push(addPick);
                    }
                    command.season = props.form.season;
                    command.draftRequestList = draft;
                    return [4 /*yield*/, client.create(command)];
                case 1:
                    response = _a.sent();
                    setCanSubmitDraft(false);
                    setCompletedDraft(true);
                    return [3 /*break*/, 3];
                case 2:
                    e_1 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_1.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var checkIfFormComplete = function (draft) {
        if (draft.every(function (element) { return element.nominatedPlayer && element.nominatingPlayer && element.playerSoldTo &&
            element.sellPrice.length > 0; })) {
            setCanSubmitDraft(true);
        }
        else {
            setCanSubmitDraft(false);
        }
    };
    var update = function (e) {
        props.update(e.target.name, e.target.value);
    };
    // create a list for each captain who bought a player.
    var renderSoldForInput = function (idx) {
        var select = null;
        select = (React.createElement(reactstrap_1.InputGroup, null,
            React.createElement(reactstrap_1.InputGroupAddon, { addonType: "prepend" },
                React.createElement(reactstrap_1.InputGroupText, null, "$")),
            React.createElement(reactstrap_1.Input, { placeholder: "Amount", min: 1, max: 28, type: "number", step: "1", disabled: completedDraft, onChange: function (e) { return handleSoldForInput(idx, e.target.value); } })));
        return (select);
    };
    // create a list for each captain who bought a player.
    var renderSoldToDropdown = function (idx) {
        var select = null;
        if (props.form.captains) {
            select = (React.createElement(react_select_1.default, { options: props.form.captains, onChange: function (e) { return handleSoldToSelected(idx, e); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: completedDraft, isSearchable: true }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No captains left!", value: "Not" }], isDisabled: completedDraft }));
        }
        return (select);
    };
    // create a list for each nominating captain.
    var renderNominatingCaptainDropdown = function (idx) {
        var select = null;
        if (props.form.captains) {
            select = (React.createElement(react_select_1.default, { options: props.form.captains, onChange: function (e) { return handleNominatingSelected(idx, e); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: completedDraft, isSearchable: true }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No captains left!", value: "Not" }], isDisabled: completedDraft }));
        }
        return (select);
    };
    // create a list for each nominated player.
    var renderNominatedPlayerDropdown = function (idx) {
        var select = null;
        if (props.form.players) {
            select = (React.createElement(react_select_1.default, { options: props.form.players, onChange: function (e) { return handleNominatedSelected(idx, e); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: completedDraft, isSearchable: true }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No players left!", value: "Not" }], isDisabled: completedDraft }));
        }
        return (select);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Register Draft"),
                React.createElement("p", null, "Please input the draft information. When completed, the team rosters will be finalized."),
                React.createElement(reactstrap_1.Label, { for: "amountTeams" }, "Amount of players per team"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", name: "amountPlayers", min: 4, max: 4, type: "number", step: "1", value: 4, disabled: true }),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !canCreateDraft, onClick: createDraftPicks }, "Create Draft Picks"),
                React.createElement("hr", null))),
        React.createElement(reactstrap_1.Row, null, draftList && canCreateDraft == false && (draftList.map(function (draft, index) { return (React.createElement(reactstrap_1.Col, { xs: "6", sm: "4" },
            React.createElement(reactstrap_1.Card, null,
                React.createElement(reactstrap_1.CardBody, null,
                    React.createElement(reactstrap_1.CardTitle, { tag: "h5" },
                        "Draft Pick #",
                        index + 1),
                    "Captain",
                    renderNominatingCaptainDropdown(index),
                    "nominates player",
                    renderNominatedPlayerDropdown(index),
                    "who is bought by",
                    renderSoldToDropdown(index),
                    "for",
                    renderSoldForInput(index))),
            React.createElement("br", null))); }))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !canSubmitDraft, onClick: createDraft }, "Finalize Draft"))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(StepButtons_1.default, __assign({ step: 3 }, props, { disabled: !completedDraft }))))));
};
exports.default = RegisterDraft;
//# sourceMappingURL=RegisterDraft.js.map