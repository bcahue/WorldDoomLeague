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
var react_datetime_picker_1 = require("react-datetime-picker");
var state_1 = require("../../../state");
var CreatePlayoffGame = function (props) {
    var _a = react_1.useState(true), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState(false), useHomefields = _b[0], setUseHomefields = _b[1];
    var _c = react_1.useState(false), allowHomefields = _c[0], setAllowHomefields = _c[1];
    var _d = react_1.useState(0), season = _d[0], setSeason = _d[1];
    var _e = react_1.useState(0), week = _e[0], setWeek = _e[1];
    var _f = react_1.useState(0), blueTeam = _f[0], setBlueTeam = _f[1];
    var _g = react_1.useState(0), redTeam = _g[0], setRedTeam = _g[1];
    var _h = react_1.useState(0), newGame = _h[0], setNewGame = _h[1];
    var _j = react_1.useState(null), date = _j[0], setDate = _j[1];
    var _k = react_1.useState([]), mapIds = _k[0], setMapIds = _k[1];
    var _l = react_1.useState(0), mapId = _l[0], setMapId = _l[1];
    var _m = react_1.useState(0), gameMapId = _m[0], setGameMapId = _m[1];
    var _o = react_1.useState([]), seasonsData = _o[0], setSeasonsData = _o[1];
    var _p = react_1.useState([]), weeksData = _p[0], setWeeksData = _p[1];
    var _q = react_1.useState([]), mapsData = _q[0], setMapsData = _q[1];
    var _r = react_1.useState([]), gameMapsData = _r[0], setGameMapsData = _r[1];
    var _s = react_1.useState([]), teamsData = _s[0], setTeamsData = _s[1];
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
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
            var client, response, data, e_2;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        setLoading(true);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        client = new WorldDoomLeague_1.MapsClient();
                        return [4 /*yield*/, client.get()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        data = response.mapList;
                        setMapsData(data);
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
    }, [newGame]);
    var createPlayoffWeeks = function () { return __awaiter(void 0, void 0, void 0, function () {
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
                            return [4 /*yield*/, client.getPlayoffWeeks(season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.weekList;
                            setWeeksData(data);
                            return [4 /*yield*/, getTeams()];
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
    var getTeams = function () { return __awaiter(void 0, void 0, void 0, function () {
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
                            return [4 /*yield*/, client.getTeamsBySeasonId(season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.teamList;
                            setTeamsData(data);
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
    var createGame = function () { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_5;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MatchesClient();
                    command = new WorldDoomLeague_1.CreateMatchCommand;
                    command.season = season;
                    command.week = week;
                    command.gameDateTime = date;
                    command.blueTeam = blueTeam;
                    command.redTeam = redTeam;
                    command.gameMapIds = mapIds;
                    return [4 /*yield*/, client.create(command)];
                case 1:
                    response = _a.sent();
                    setSeason(0);
                    setWeek(0);
                    setRedTeam(0);
                    setBlueTeam(0);
                    setMapId(0);
                    setGameMapId(0);
                    setMapIds([]);
                    setGameMapsData([]);
                    setUseHomefields(false);
                    setAllowHomefields(false);
                    setNewGame(response);
                    setWeeksData(null);
                    setDate(null);
                    return [3 /*break*/, 3];
                case 2:
                    e_5 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_5.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var addMap = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var gameMaps, newGameMapsData, newMapsData, disabledMap, newGameMap, newGameMapIds;
        return __generator(this, function (_a) {
            gameMaps = mapIds;
            newGameMapsData = gameMapsData;
            newMapsData = mapsData;
            disabledMap = newMapsData.map(function (s, _idx) {
                if (s.id !== mapId)
                    return s;
                return __assign(__assign({}, s), { isdisabled: true });
            });
            newMapsData = disabledMap;
            newGameMap = newGameMapsData.concat({
                id: mapsData.find(function (o) { return o.id == mapId; }).id,
                mapName: mapsData.find(function (o) { return o.id == mapId; }).mapName,
                mapPack: mapsData.find(function (o) { return o.id == mapId; }).mapPack,
                mapNumber: mapsData.find(function (o) { return o.id == mapId; }).mapNumber
            });
            newGameMapsData = newGameMap;
            newGameMapIds = gameMaps.concat(mapId);
            gameMaps = newGameMapIds;
            setMapIds(gameMaps);
            setGameMapsData(newGameMapsData);
            setMapsData(newMapsData);
            setMapId(0);
            setGameMapId(0);
            return [2 /*return*/];
        });
    }); };
    var removeMap = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var gameMaps, newGameMapsData, newMapsData, disabledMap, newGameMap, newGameMapIds;
        return __generator(this, function (_a) {
            gameMaps = mapIds;
            newGameMapsData = gameMapsData;
            newMapsData = mapsData;
            disabledMap = newMapsData.map(function (s, _idx) {
                if (s.id !== gameMapId)
                    return s;
                return __assign(__assign({}, s), { isdisabled: false });
            });
            newMapsData = disabledMap;
            newGameMap = newGameMapsData.filter(function (gameMap, idx) { return gameMap.id !== gameMapId; });
            newGameMapsData = newGameMap;
            newGameMapIds = gameMaps.filter(function (num, idx) { return num !== gameMapId; });
            gameMaps = newGameMapIds;
            setMapIds(gameMaps);
            setGameMapsData(newGameMapsData);
            setMapsData(newMapsData);
            setMapId(0);
            setGameMapId(0);
            return [2 /*return*/];
        });
    }); };
    var handleRedTeamSelected = function (teamId) {
        var newTeams = teamsData;
        // re-enable a team if it is deselected.
        if (redTeam !== 0) {
            var reenabledOldTeam = teamsData.map(function (s, _idx) {
                if (s.id !== redTeam)
                    return s;
                return __assign(__assign({}, s), { isdisabled: false });
            });
            newTeams = reenabledOldTeam;
        }
        // disable the team that was selected.
        var disabledTeam = newTeams.map(function (s, _idx) {
            if (s.id !== teamId)
                return s;
            return __assign(__assign({}, s), { isdisabled: true });
        });
        if (teamsData.find(function (o) { return o.id === teamId; }).homeFieldMapId !== null) {
            if (teamsData.find(function (o) { return o.id === blueTeam; }).homeFieldMapId !== null) {
                setAllowHomefields(true);
            }
            else {
                setAllowHomefields(false);
            }
        }
        else {
            setAllowHomefields(false);
        }
        newTeams = disabledTeam;
        setTeamsData(newTeams);
        setRedTeam(teamId);
    };
    var handleBlueTeamSelected = function (teamId) {
        var newTeams = teamsData;
        // re-enable a team if it is deselected.
        if (blueTeam !== 0) {
            var reenabledOldTeam = teamsData.map(function (s, _idx) {
                if (s.id !== blueTeam)
                    return s;
                return __assign(__assign({}, s), { isdisabled: false });
            });
            newTeams = reenabledOldTeam;
        }
        // disable the team that was selected.
        var disabledTeam = newTeams.map(function (s, _idx) {
            if (s.id !== teamId)
                return s;
            return __assign(__assign({}, s), { isdisabled: true });
        });
        if (teamsData.find(function (o) { return o.id === teamId; }).homeFieldMapId !== null) {
            if (teamsData.find(function (o) { return o.id === redTeam; }).homeFieldMapId !== null) {
                setAllowHomefields(true);
            }
            else {
                setAllowHomefields(false);
            }
        }
        else {
            setAllowHomefields(false);
        }
        newTeams = disabledTeam;
        setTeamsData(newTeams);
        setBlueTeam(teamId);
    };
    var handleUseHomefieldsChecked = function (value) {
        var gameMaps = [];
        if (value === true) {
            gameMaps.push(teamsData.find(function (o) { return o.id == redTeam; }).homeFieldMapId);
            gameMaps.push(teamsData.find(function (o) { return o.id == blueTeam; }).homeFieldMapId);
            setUseHomefields(true);
        }
        else {
            setUseHomefields(false);
            setGameMapsData([]);
        }
        setMapIds(gameMaps);
    };
    // create a list item for each playoff week
    var renderWeeksList = function () {
        var select = null;
        if (weeksData && weeksData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: weeksData, onChange: function (e) { return setWeek(e.id); }, isSearchable: true, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return "Week #" + label.weekNumber + " - Week Type: " + label.weekType; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No weeks!", value: "0" }] }));
        }
        return (select);
    };
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
    // create a list for each team, and the date select.
    var renderTeamsList = function () {
        var select = null;
        select = (React.createElement("div", null,
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, { xs: "6" },
                    React.createElement("h2", { className: "text-center text-danger" }, "Red Team"),
                    React.createElement("br", null),
                    React.createElement(react_select_1.default, { options: teamsData, onChange: function (e) { return handleRedTeamSelected(e.id); }, isSearchable: true, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.teamName + " - Homefield: " + label.homeFieldMapName; }, isOptionDisabled: function (d) { return d.isdisabled; }, isLoading: loading })),
                React.createElement(reactstrap_1.Col, { xs: "6" },
                    React.createElement("h2", { className: "text-center text-primary" }, "Blue Team"),
                    React.createElement("br", null),
                    React.createElement(react_select_1.default, { options: teamsData, onChange: function (e) { return handleBlueTeamSelected(e.id); }, isSearchable: true, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.teamName + " - Homefield: " + label.homeFieldMapName; }, isOptionDisabled: function (d) { return d.isdisabled; }, isLoading: loading }))),
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                    React.createElement("br", null),
                    React.createElement(react_datetime_picker_1.default, { id: 'gameDate', name: 'signupDates', onChange: function (e) { return setDate(e); }, value: typeof date === "string" ? new Date(date) : date }),
                    React.createElement("br", null),
                    React.createElement("br", null))),
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                    React.createElement("p", null, "If the Use Homefield Maps checkmark is disabled, its because the teams selected do not have homefield associated with them."),
                    React.createElement("p", null, "Make homefield selections for these teams and come back to this, or manually set the maps that will be played during the game."),
                    React.createElement(reactstrap_1.FormGroup, { check: true },
                        React.createElement(reactstrap_1.Label, { check: true },
                            React.createElement(reactstrap_1.Input, { type: "checkbox", onChange: function (e) { return handleUseHomefieldsChecked(e.target.checked); }, disabled: !allowHomefields }),
                            "Use Homefield Maps")),
                    useHomefields == false && (React.createElement("div", null,
                        React.createElement(reactstrap_1.Label, { for: "mapSelect" }, "Add at least 1 game map to this game."),
                        React.createElement(react_select_1.default, { options: mapsData, onChange: function (e) { return setMapId(e.id); }, isSearchable: true, value: mapsData.find(function (o) { return o.id == mapId; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.mapName; }, isOptionDisabled: function (d) { return d.isdisabled; }, isLoading: loading }),
                        React.createElement("br", null),
                        React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(mapId > 0), onClick: addMap }, "Add Map to Game"),
                        React.createElement("br", null),
                        React.createElement(reactstrap_1.Label, { for: "mapSelect" }, "Game Maps"),
                        React.createElement(react_select_1.default, { options: gameMapsData, onChange: function (e) { return setGameMapId(e.id); }, isSearchable: true, value: gameMapsData.find(function (o) { return o.id == gameMapId; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.mapName; }, isLoading: loading }),
                        React.createElement("br", null),
                        React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(gameMapId > 0), onClick: removeMap }, "Remove Map from Game"),
                        React.createElement("br", null)))))));
        return (select);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Create Playoff Game"),
                React.createElement("p", null, "Please select a season to create a playoff game."),
                React.createElement(reactstrap_1.Label, { for: "seasonSelect" }, "Select a season"),
                renderSeasonList(),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(season > 0) || loading, onClick: createPlayoffWeeks }, "Select Season"),
                React.createElement("br", null),
                newGame > 0 && (React.createElement("h2", null,
                    "Game #",
                    newGame,
                    " has been created!")),
                weeksData && weeksData.length > 0 && (renderWeeksList()),
                React.createElement("br", null))),
        week > 0 && (renderTeamsList()),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(blueTeam > 0) || !(redTeam > 0) || !(mapIds.length > 0) || loading, onClick: createGame }, "Create Playoff Game")))));
};
exports.default = CreatePlayoffGame;
//# sourceMappingURL=CreatePlayoffGame.js.map