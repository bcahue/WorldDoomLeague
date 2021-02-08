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
            teamName: null,
            teamAbbreviation: null,
            teamCaptain: null
        }]), teamList = _a[0], setTeamList = _a[1];
    var _b = react_1.useState(6), amountTeams = _b[0], setAmountTeams = _b[1];
    var _c = react_1.useState(0), createdTeams = _c[0], setCreatedTeams = _c[1];
    var _d = react_1.useState(false), toggle = _d[0], setToggle = _d[1];
    var _e = react_1.useState(false), canSubmitTeams = _e[0], setCanSubmitTeams = _e[1];
    react_1.useEffect(function () {
        var pad_array = function (arr, len, fill) {
            return arr.concat(Array(len).fill(fill)).slice(0, len);
        };
        var newTeamList = pad_array(teamList, amountTeams, {
            teamName: null,
            teamAbbreviation: null,
            teamCaptain: null
        });
        setTeamList(newTeamList);
    }, [amountTeams]);
    var enableTeamList = function () {
        setToggle(true);
    };
    var handleTeamNameChange = function (idx, value) {
        var newTeamName = teamList.map(function (team, sidx) {
            if (idx !== sidx)
                return team;
            return __assign(__assign({}, team), { teamName: value });
        });
        setTeamList(newTeamName);
        checkIfFormComplete(newTeamName);
    };
    var handleTeamAbvChange = function (idx, value) {
        var newTeamName = teamList.map(function (team, sidx) {
            if (idx !== sidx)
                return team;
            return __assign(__assign({}, team), { teamAbbreviation: value });
        });
        setTeamList(newTeamName);
        checkIfFormComplete(newTeamName);
    };
    var handlePlayerSelected = function (idx, value) {
        var newPlayerArray = props.form.players.map(function (s, _idx) {
            if (s.value !== value.value)
                return s;
            return __assign(__assign({}, s), { isdisabled: true });
        });
        if (teamList[idx].teamCaptain !== null) {
            var reenabledOldPlayer = newPlayerArray.map(function (s, _idx) {
                if (s.value !== teamList[idx].teamCaptain)
                    return s;
                return __assign(__assign({}, s), { isdisabled: false });
            });
            props.update("players", reenabledOldPlayer);
        }
        else {
            props.update("players", newPlayerArray);
        }
        var newCaptain = teamList.map(function (team, sidx) {
            if (idx !== sidx)
                return team;
            return __assign(__assign({}, team), { teamCaptain: value.value });
        });
        setTeamList(newCaptain);
        checkIfFormComplete(newCaptain);
    };
    var createTeams = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, teams, idx, addTeam, response, captains, players, e_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.TeamsClient();
                    command = new WorldDoomLeague_1.CreateTeamsCommand;
                    teams = [];
                    for (idx = 0; idx < teamList.length; idx++) {
                        addTeam = new WorldDoomLeague_1.TeamsRequest();
                        addTeam.teamName = teamList[idx].teamName;
                        addTeam.teamAbbreviation = teamList[idx].teamAbbreviation;
                        addTeam.teamCaptain = teamList[idx].teamCaptain;
                        teams.push(addTeam);
                    }
                    command.season = props.form.season;
                    command.teamsRequestList = teams;
                    return [4 /*yield*/, client.createTeams(command)];
                case 1:
                    response = _a.sent();
                    setCreatedTeams(response);
                    captains = createCaptainsList();
                    setCanSubmitTeams(false);
                    setAmountTeams(response);
                    props.update("captains", captains);
                    players = createPlayersList();
                    props.update("players", players);
                    return [3 /*break*/, 3];
                case 2:
                    e_1 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_1.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var createCaptainsList = function () {
        return teamList.map(function (s, _idx) {
            var captain = props.form.players.find(function (i) { return i.value === s.teamCaptain; });
            if (s.teamCaptain !== captain.value)
                return s;
            // this should add the captain names to the objects.
            return __assign(__assign({}, s), { label: captain.label, value: captain.value });
        });
    };
    var createPlayersList = function () {
        return props.form.players.filter(function (s, _idx) { return s.isdisabled !== true; });
    };
    var checkIfFormComplete = function (teams) {
        if (teams.every(function (element) { return element.teamAbbreviation && element.teamCaptain && element.teamName &&
            element.teamAbbreviation.length > 0 && element.teamName.length > 0; })) {
            setCanSubmitTeams(true);
        }
        else {
            setCanSubmitTeams(false);
        }
    };
    var update = function (e) {
        props.update(e.target.name, e.target.value);
    };
    // create a list for each engine.
    var renderPlayerDropdown = function (idx) {
        var select = null;
        if (props.form.players) {
            select = (React.createElement(react_select_1.default, { options: props.form.players, onChange: function (e) { return handlePlayerSelected(idx, e); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: (createdTeams === amountTeams), isSearchable: true }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No players left!", value: "Not" }], isDisabled: (createdTeams === amountTeams) }));
        }
        return (select);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Add Teams"),
                React.createElement("p", null, "This step will add the teams that will play during the season."),
                React.createElement(reactstrap_1.Label, { for: "amountTeams" }, "Amount of teams"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", name: "amountTeams", min: 4, max: 512, type: "number", step: "2", value: amountTeams, disabled: toggle, onChange: function (e) { return setAmountTeams(parseInt(e.target.value, 10)); } }),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: toggle, onClick: enableTeamList }, "Generate Team Cards"),
                React.createElement("hr", null))),
        React.createElement(reactstrap_1.Row, null, teamList && toggle && (teamList.map(function (team, index) { return (React.createElement(reactstrap_1.Col, { xs: "6", sm: "4" },
            React.createElement(reactstrap_1.Card, null,
                React.createElement(reactstrap_1.CardBody, null,
                    React.createElement(reactstrap_1.CardTitle, { tag: "h5" }, "Team"),
                    React.createElement(reactstrap_1.Label, { for: "teamName" }, "Team Name"),
                    React.createElement(reactstrap_1.Input, { type: "text", value: team.teamName, placeholder: "Super Chargers", disabled: (createdTeams === amountTeams), onChange: function (e) { return handleTeamNameChange(index, e.target.value); } }),
                    React.createElement(reactstrap_1.Label, { for: "teamName" }, "Team Abbreviation"),
                    React.createElement(reactstrap_1.Input, { type: "text", value: team.teamAbbreviation, placeholder: "SUC", disabled: (createdTeams === amountTeams), onChange: function (e) { return handleTeamAbvChange(index, e.target.value); } }),
                    React.createElement(reactstrap_1.Label, { for: "teamName" }, "Team Captain"),
                    renderPlayerDropdown(index))),
            React.createElement("br", null))); }))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !canSubmitTeams, onClick: createTeams }, "Create Teams"))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(StepButtons_1.default, __assign({ step: 3 }, props, { disabled: !(createdTeams === amountTeams) }))))));
};
exports.default = RegisterDraft;
//# sourceMappingURL=RegisterDraft.js.map