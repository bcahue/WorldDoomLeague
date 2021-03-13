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
var React = require("react");
var react_1 = require("react");
var reactstrap_1 = require("reactstrap");
var react_select_1 = require("react-select");
var WorldDoomLeague_1 = require("../../../WorldDoomLeague");
var state_1 = require("../../../state");
var TradePlayerToTeam = function (props) {
    var _a = react_1.useState(true), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState(false), tradeSuccessful = _b[0], setTradeSuccessful = _b[1];
    var _c = react_1.useState(0), week = _c[0], setWeek = _c[1];
    var _d = react_1.useState(0), season = _d[0], setSeason = _d[1];
    var _e = react_1.useState(0), redPlayer = _e[0], setRedPlayer = _e[1];
    var _f = react_1.useState(0), bluePlayer = _f[0], setBluePlayer = _f[1];
    var _g = react_1.useState(0), redTeam = _g[0], setRedTeam = _g[1];
    var _h = react_1.useState(0), blueTeam = _h[0], setBlueTeam = _h[1];
    var _j = react_1.useState([]), weeksData = _j[0], setWeeksData = _j[1];
    var _k = react_1.useState([]), seasonsData = _k[0], setSeasonsData = _k[1];
    var _l = react_1.useState([]), teamsData = _l[0], setTeamsData = _l[1];
    var _m = react_1.useState(), redTeamPlayersData = _m[0], setRedTeamPlayersData = _m[1];
    var _o = react_1.useState(), blueTeamPlayersData = _o[0], setBlueTeamPlayersData = _o[1];
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
            var client, response, data, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        setLoading(true);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        client = new WorldDoomLeague_1.SeasonsClient();
                        return [4 /*yield*/, client.getUnfinishedSeasons()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        data = response.seasonList;
                        setSeasonsData(data);
                        return [3 /*break*/, 4];
                    case 3:
                        e_1 = _a.sent();
                        state_1.setErrorMessage(JSON.parse(e_1.response));
                        return [3 /*break*/, 4];
                    case 4:
                        setLoading(false);
                        return [2 /*return*/];
                }
            });
        }); };
        fetchData();
    }, []);
    var updateTeams = function () { return __awaiter(void 0, void 0, void 0, function () {
        var fetchData;
        return __generator(this, function (_a) {
            fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
                var client, response, data, e_2;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            setLoading(true);
                            _a.label = 1;
                        case 1:
                            _a.trys.push([1, 3, , 4]);
                            client = new WorldDoomLeague_1.TeamsClient();
                            return [4 /*yield*/, client.getTeamsBySeasonId(season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.teamList;
                            setTeamsData(data);
                            return [3 /*break*/, 4];
                        case 3:
                            e_2 = _a.sent();
                            state_1.setErrorMessage(JSON.parse(e_2.response));
                            return [3 /*break*/, 4];
                        case 4:
                            setLoading(false);
                            return [2 /*return*/];
                    }
                });
            }); };
            fetchData();
            return [2 /*return*/];
        });
    }); };
    var updateWeeks = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var fetchData;
        return __generator(this, function (_a) {
            fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
                var client, response, data, e_3;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            setLoading(true);
                            _a.label = 1;
                        case 1:
                            _a.trys.push([1, 4, , 5]);
                            client = new WorldDoomLeague_1.WeeksClient();
                            return [4 /*yield*/, client.getRegularSeasonWeeks(season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.weekList;
                            setWeeksData(data);
                            return [4 /*yield*/, updateTeams()];
                        case 3:
                            _a.sent();
                            return [3 /*break*/, 5];
                        case 4:
                            e_3 = _a.sent();
                            state_1.setErrorMessage(JSON.parse(e_3.response));
                            return [3 /*break*/, 5];
                        case 5:
                            setLoading(false);
                            return [2 /*return*/];
                    }
                });
            }); };
            fetchData();
            return [2 /*return*/];
        });
    }); };
    var updatePlayersFromTeam = function (teamId) { return __awaiter(void 0, void 0, void 0, function () {
        var fetchData;
        return __generator(this, function (_a) {
            fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
                var client, response, data, e_4;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            setLoading(true);
                            _a.label = 1;
                        case 1:
                            _a.trys.push([1, 3, , 4]);
                            client = new WorldDoomLeague_1.TeamsClient();
                            return [4 /*yield*/, client.getTeamPlayers(teamId)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.teamPlayers;
                            setRedTeamPlayersData(data);
                            return [3 /*break*/, 4];
                        case 3:
                            e_4 = _a.sent();
                            state_1.setErrorMessage(JSON.parse(e_4.response));
                            return [3 /*break*/, 4];
                        case 4:
                            setLoading(false);
                            return [2 /*return*/];
                    }
                });
            }); };
            fetchData();
            return [2 /*return*/];
        });
    }); };
    var updatePlayersToTeam = function (teamId) { return __awaiter(void 0, void 0, void 0, function () {
        var fetchData;
        return __generator(this, function (_a) {
            fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
                var client, response, data, e_5;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            setLoading(true);
                            _a.label = 1;
                        case 1:
                            _a.trys.push([1, 3, , 4]);
                            client = new WorldDoomLeague_1.TeamsClient();
                            return [4 /*yield*/, client.getTeamPlayers(teamId)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.teamPlayers;
                            setBlueTeamPlayersData(data);
                            return [3 /*break*/, 4];
                        case 3:
                            e_5 = _a.sent();
                            state_1.setErrorMessage(JSON.parse(e_5.response));
                            return [3 /*break*/, 4];
                        case 4:
                            setLoading(false);
                            return [2 /*return*/];
                    }
                });
            }); };
            fetchData();
            return [2 /*return*/];
        });
    }); };
    var tradePlayerToTeam = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_6;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.PlayerTransactionsClient();
                    command = new WorldDoomLeague_1.TradePlayerToTeamCommand;
                    command.season = season;
                    command.week = week;
                    command.teamTradedFrom = redTeam;
                    command.teamTradedTo = blueTeam;
                    command.tradedPlayer = redPlayer;
                    command.tradedPlayerFor = bluePlayer;
                    return [4 /*yield*/, client.tradePlayerToTeam(command)];
                case 1:
                    response = _a.sent();
                    setWeek(0);
                    setBluePlayer(0);
                    setRedPlayer(0);
                    setBlueTeam(0);
                    setRedTeam(0);
                    setTradeSuccessful(response);
                    setWeeksData([]);
                    setTeamsData([]);
                    setRedTeamPlayersData(null);
                    setBlueTeamPlayersData(null);
                    return [3 /*break*/, 3];
                case 2:
                    e_6 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_6.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var handleTeamTradedFrom = function (teamId) { return __awaiter(void 0, void 0, void 0, function () {
        var newTeams, reenabledOldTeam, disabledTeam;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    newTeams = teamsData;
                    // re-enable a team if it is deselected.
                    if (redTeam !== 0) {
                        reenabledOldTeam = teamsData.map(function (s, _idx) {
                            if (s.id !== redTeam)
                                return s;
                            return __assign(__assign({}, s), { isdisabled: false });
                        });
                        newTeams = reenabledOldTeam;
                    }
                    disabledTeam = newTeams.map(function (s, _idx) {
                        if (s.id !== teamId)
                            return s;
                        return __assign(__assign({}, s), { isdisabled: true });
                    });
                    newTeams = disabledTeam;
                    setTeamsData(newTeams);
                    setRedTeam(teamId);
                    return [4 /*yield*/, updatePlayersFromTeam(teamId)];
                case 1:
                    _a.sent();
                    return [2 /*return*/];
            }
        });
    }); };
    var handleTeamTradedTo = function (teamId) { return __awaiter(void 0, void 0, void 0, function () {
        var newTeams, reenabledOldTeam, disabledTeam;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    newTeams = teamsData;
                    // re-enable a team if it is deselected.
                    if (blueTeam !== 0) {
                        reenabledOldTeam = teamsData.map(function (s, _idx) {
                            if (s.id !== blueTeam)
                                return s;
                            return __assign(__assign({}, s), { isdisabled: false });
                        });
                        newTeams = reenabledOldTeam;
                    }
                    disabledTeam = newTeams.map(function (s, _idx) {
                        if (s.id !== teamId)
                            return s;
                        return __assign(__assign({}, s), { isdisabled: true });
                    });
                    newTeams = disabledTeam;
                    setTeamsData(newTeams);
                    setBlueTeam(teamId);
                    return [4 /*yield*/, updatePlayersToTeam(teamId)];
                case 1:
                    _a.sent();
                    return [2 /*return*/];
            }
        });
    }); };
    // create a list item for each season
    var renderSeasonList = function () {
        var select = null;
        if (seasonsData && seasonsData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: seasonsData, onChange: function (e) { return setSeason(e.id); }, isSearchable: true, value: seasonsData.find(function (o) { return o.id == season; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.seasonName; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No seasons!", value: "0" }] }));
        }
        return (select);
    };
    // create a list item for each season
    var renderWeeksList = function () {
        var select = null;
        if (weeksData && weeksData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: weeksData, onChange: function (e) { return setWeek(e.id); }, isSearchable: true, value: weeksData.find(function (o) { return o.id == week; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return "Week #" + label.weekNumber + " - " + label.weekStartDate; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No weeks!", value: "0" }] }));
        }
        return (select);
    };
    // create a list item for each team
    var renderTradedFromList = function () {
        var select = null;
        if (teamsData && teamsData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: teamsData, onChange: function (e) { return handleTeamTradedFrom(e.id); }, isSearchable: true, value: teamsData.find(function (o) { return o.id == redTeam; }) || null, isOptionDisabled: function (value) { return value.isdisabled; }, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.teamName + " (" + label.teamAbbreviation + ")"; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No teams!", value: "0" }] }));
        }
        return (select);
    };
    // create a list item for each team
    var renderTradedToList = function () {
        var select = null;
        if (teamsData && teamsData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: teamsData, onChange: function (e) { return handleTeamTradedTo(e.id); }, isSearchable: true, value: teamsData.find(function (o) { return o.id == blueTeam; }) || null, isOptionDisabled: function (value) { return value.isdisabled; }, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.teamName + " (" + label.teamAbbreviation + ")"; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No teams!", value: "0" }] }));
        }
        return (select);
    };
    // create a list item for each player traded from
    var renderPlayerTradedFrom = function () {
        var select = null;
        if (redTeamPlayersData && redTeamPlayersData.teamPlayers && redTeamPlayersData.teamPlayers.length > 0) {
            select = (React.createElement(react_select_1.default, { options: redTeamPlayersData.teamPlayers, onChange: function (e) { return setRedPlayer(e.id); }, isSearchable: true, value: redTeamPlayersData.teamPlayers.find(function (o) { return o.id == redPlayer; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return "" + label.playerName; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No players!", value: "0" }] }));
        }
        return (select);
    };
    // create a list item for each player traded to
    var renderPlayerTradedTo = function () {
        var select = null;
        if (blueTeamPlayersData && blueTeamPlayersData.teamPlayers && blueTeamPlayersData.teamPlayers.length > 0) {
            select = (React.createElement(react_select_1.default, { options: blueTeamPlayersData.teamPlayers, onChange: function (e) { return setBluePlayer(e.id); }, isSearchable: true, value: blueTeamPlayersData.teamPlayers.find(function (o) { return o.id == bluePlayer; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return "" + label.playerName; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No players!", value: "0" }] }));
        }
        return (select);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Trade Player To Team"),
                React.createElement("p", null, "Please select a season to make a trade.."),
                React.createElement(reactstrap_1.Label, { for: "seasonSelect" }, "Select a season"),
                renderSeasonList(),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(season > 0) || loading, onClick: updateWeeks }, "Select Season"),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Label, { for: "week" }, "Select a week"),
                React.createElement("br", null),
                renderWeeksList(),
                React.createElement("br", null),
                React.createElement("h4", { className: "text-center text-danger" }, "Warning: This will allow you to create a trade during any regular season week, but be advised the the roster change happens immediately."),
                React.createElement("h4", { className: "text-center text-danger" }, "In other words, don't make this trade if there's an outstanding game to process with this player on this team."))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { xs: "6" },
                React.createElement("h4", { className: "text-center" }, "Player traded from"),
                renderTradedFromList(),
                React.createElement("br", null),
                renderPlayerTradedFrom()),
            React.createElement(reactstrap_1.Col, { xs: "6" },
                React.createElement("h4", { className: "text-center" }, "Player traded to"),
                renderTradedToList(),
                React.createElement("br", null),
                renderPlayerTradedTo())),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("br", null),
                tradeSuccessful && (React.createElement("h2", { className: "text-center" }, "Trade completed!")),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(season > 0) || !(week > 0) || !(redTeam > 0) || !(blueTeam > 0) || !(redPlayer > 0) || !(bluePlayer > 0) || loading, onClick: tradePlayerToTeam }, "Finalize Trade")))));
};
exports.default = TradePlayerToTeam;
//# sourceMappingURL=TradePlayerToTeam.js.map