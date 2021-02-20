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
var react_router_dom_1 = require("react-router-dom");
var state_1 = require("../../../state");
var CreateGames = function (props) {
    var _a = react_1.useState([{
            weekId: null,
            mapId: null,
            gameList: []
        }]), games = _a[0], setGames = _a[1];
    var _b = react_1.useState([{
            id: null,
            weekNumber: null,
            weekStartDate: null
        }]), weeks = _b[0], setWeeks = _b[1];
    var _c = react_1.useState(true), loading = _c[0], setLoading = _c[1];
    var _d = react_1.useState(1), gamesPerWeek = _d[0], setGamesPerWeek = _d[1];
    var _e = react_1.useState(0), currentWeek = _e[0], setCurrentWeek = _e[1];
    var _f = react_1.useState(false), canSubmitGames = _f[0], setCanSubmitGames = _f[1];
    var _g = react_1.useState(false), completedGames = _g[0], setCompletedGames = _g[1];
    var _h = react_1.useState(true), canCreateGames = _h[0], setCanCreateGames = _h[1];
    var _j = react_1.useState([]), data = _j[0], setData = _j[1];
    var _k = react_1.useState(""), mapPack = _k[0], setMapPack = _k[1];
    var _l = react_1.useState(""), mapName = _l[0], setMapName = _l[1];
    var _m = react_1.useState(0), mapNumber = _m[0], setMapNumber = _m[1];
    var _o = react_1.useState(0), newMapId = _o[0], setNewMapId = _o[1];
    var history = react_router_dom_1.useHistory();
    var redirect = function () {
        history.push('/');
    };
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
            var client, response, data_1, e_1;
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
                        data_1 = response.mapList;
                        setData(data_1);
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
    }, [newMapId]);
    var getWeeks = function () { return __awaiter(void 0, void 0, void 0, function () {
        var fetchData;
        return __generator(this, function (_a) {
            fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
                var client, response, weekData, e_2;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            setLoading(true);
                            _a.label = 1;
                        case 1:
                            _a.trys.push([1, 3, , 4]);
                            client = new WorldDoomLeague_1.WeeksClient();
                            return [4 /*yield*/, client.getRegularSeasonWeeks(props.form.season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            weekData = response.weekList;
                            setLoading(false);
                            return [2 /*return*/, weekData];
                        case 3:
                            e_2 = _a.sent();
                            state_1.setErrorMessage(JSON.parse(e_2.response));
                            return [3 /*break*/, 4];
                        case 4: return [2 /*return*/];
                    }
                });
            }); };
            return [2 /*return*/, fetchData()];
        });
    }); };
    var getTeams = function () { return __awaiter(void 0, void 0, void 0, function () {
        var fetchData;
        return __generator(this, function (_a) {
            fetchData = function () { return __awaiter(void 0, void 0, void 0, function () {
                var client, response, teamData, e_3;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            setLoading(true);
                            _a.label = 1;
                        case 1:
                            _a.trys.push([1, 3, , 4]);
                            client = new WorldDoomLeague_1.TeamsClient();
                            return [4 /*yield*/, client.getTeamsBySeasonId(props.form.season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            teamData = response.teamList;
                            setLoading(false);
                            return [2 /*return*/, teamData];
                        case 3:
                            e_3 = _a.sent();
                            state_1.setErrorMessage(JSON.parse(e_3.response));
                            return [3 /*break*/, 4];
                        case 4: return [2 /*return*/];
                    }
                });
            }); };
            return [2 /*return*/, fetchData()];
        });
    }); };
    var submitGames = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, weekly, idx, addPick, response, e_4;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MatchesClient();
                    command = new WorldDoomLeague_1.CreateMatchesCommand;
                    weekly = [];
                    for (idx = 0; idx < games.length; idx++) {
                        addPick = new WorldDoomLeague_1.WeeklyRequest();
                        addPick.weekId = games[idx].weekId;
                        addPick.mapId = games[idx].mapId;
                        addPick.gameList = [];
                        games[idx].gameList.map(function (s, _sidx) {
                            var addGame = new WorldDoomLeague_1.NewGame();
                            addGame.blueTeam = s.blueTeam;
                            addGame.redTeam = s.redTeam;
                            addGame.gameDateTime = s.gameDateTime;
                            addPick.gameList.push(addGame);
                        });
                        weekly.push(addPick);
                    }
                    command.seasonId = props.form.season;
                    command.weeklyGames = weekly;
                    return [4 /*yield*/, client.createRegularSeason(command)];
                case 1:
                    response = _a.sent();
                    setCanSubmitGames(false);
                    setCompletedGames(true);
                    return [3 /*break*/, 3];
                case 2:
                    e_4 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_4.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var submitMaps = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_5;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MapsClient();
                    command = new WorldDoomLeague_1.CreateMapCommand;
                    command.mapName = mapName;
                    command.mapPack = mapPack;
                    command.mapNumber = mapNumber;
                    return [4 /*yield*/, client.create(command)];
                case 1:
                    response = _a.sent();
                    setNewMapId(response);
                    setMapPack('');
                    setMapName('');
                    setMapNumber(0);
                    return [3 /*break*/, 3];
                case 2:
                    e_5 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_5.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var createGames = function () { return __awaiter(void 0, void 0, void 0, function () {
        var pad_array, teams, gameWeeks, maxGamesPerWeek, newGames, newTeams, i, buildGames, newTeamsArray, i, weekTeams;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    pad_array = function (arr, len, fill) {
                        return arr.concat(Array(len).fill(fill)).slice(0, len);
                    };
                    setCanCreateGames(false);
                    return [4 /*yield*/, getTeams()];
                case 1:
                    teams = _a.sent();
                    return [4 /*yield*/, getWeeks()];
                case 2:
                    gameWeeks = _a.sent();
                    if (teams) {
                        maxGamesPerWeek = (teams.length / 2) * gamesPerWeek;
                        newGames = [];
                        newTeams = [];
                        for (i = 0; i < gameWeeks.length; i++) {
                            buildGames = newGames.concat({
                                weekId: gameWeeks[i].id,
                                mapId: null,
                                gameList: pad_array([], maxGamesPerWeek, {
                                    redTeam: null,
                                    blueTeam: null,
                                    gameDateTime: null
                                })
                            });
                            newGames = buildGames;
                        }
                        ;
                        newTeamsArray = teams.map(function (s, _idx) {
                            return __assign(__assign({}, s), { label: s.teamAbbreviation, value: s.id, gamesLeft: gamesPerWeek, isdisabled: false });
                        });
                        for (i = 0; i < gameWeeks.length; i++) {
                            weekTeams = [];
                            weekTeams.push.apply(weekTeams, newTeamsArray);
                            newTeams.push(weekTeams);
                        }
                        ;
                        setGames(newGames);
                        setWeeks(gameWeeks);
                        props.update("teams", newTeams);
                    }
                    return [2 /*return*/];
            }
        });
    }); };
    var handleMapChange = function (weekIndex, value) {
        var newWeeks = games.map(function (game, _idx) {
            if (_idx !== weekIndex)
                return game;
            return __assign(__assign({}, game), { mapId: value });
        });
        setGames(newWeeks);
        checkIfFormComplete(newWeeks);
    };
    var handleRedTeamSelected = function (weekIndex, gameIndex, value) {
        // subtract the gamesLeft to handle disables
        var newTeamsArray = props.form.teams.map(function (s, _idx) {
            if (_idx !== weekIndex)
                return s;
            return s.map(function (t, _sidx) {
                if (t.value !== value.value)
                    return t;
                var left = t.gamesLeft - 1;
                var disable = left <= 0;
                return __assign(__assign({}, t), { gamesLeft: left, isdisabled: disable });
            });
        });
        // re-enable a captain if they are deselected.
        if (games[weekIndex].gameList[gameIndex].redTeam !== null) {
            var reenabledOldTeam = newTeamsArray.map(function (s, _idx) {
                if (_idx !== weekIndex)
                    return s;
                return s.map(function (t, _sidx) {
                    if (t.value !== games[weekIndex].gameList[gameIndex].redTeam)
                        return t;
                    var left = t.gamesLeft + 1;
                    var disable = left <= 0;
                    return __assign(__assign({}, t), { gamesLeft: left, isdisabled: disable });
                });
            });
            props.update("teams", reenabledOldTeam);
        }
        else {
            var newTeamList = newTeamsArray.map(function (s, _idx) {
                if (_idx !== weekIndex)
                    return s;
                return s.map(function (t, _sidx) {
                    if (t.value !== value.value)
                        return t;
                    if (t.playersLeft > 0)
                        return __assign(__assign({}, t), { isdisabled: false });
                    return __assign(__assign({}, t), { isdisabled: true });
                });
            });
            props.update("teams", newTeamList);
        }
        var newRedTeam = games.map(function (game, _idx) {
            if (_idx !== weekIndex)
                return game;
            var newGameList = game.gameList.map(function (t, _sidx) {
                if (_sidx !== gameIndex)
                    return t;
                return __assign(__assign({}, t), { redTeam: value.value });
            });
            return __assign(__assign({}, game), { gameList: newGameList });
        });
        setGames(newRedTeam);
        checkIfFormComplete(newRedTeam);
    };
    var handleBlueTeamSelected = function (weekIndex, gameIndex, value) {
        // subtract the gamesLeft to handle disables
        var newTeamsArray = props.form.teams.map(function (s, _idx) {
            if (_idx !== weekIndex)
                return s;
            return s.map(function (t, _sidx) {
                if (t.value !== value.value)
                    return t;
                var left = t.gamesLeft - 1;
                var disable = left <= 0;
                return __assign(__assign({}, t), { gamesLeft: left, isdisabled: disable });
            });
        });
        // re-enable a captain if they are deselected.
        if (games[weekIndex].gameList[gameIndex].blueTeam !== null) {
            var reenabledOldTeam = newTeamsArray.map(function (s, _idx) {
                if (_idx !== weekIndex)
                    return s;
                return s.map(function (t, _sidx) {
                    if (t.value !== games[weekIndex].gameList[gameIndex].blueTeam)
                        return t;
                    var left = t.gamesLeft + 1;
                    var disable = left <= 0;
                    return __assign(__assign({}, t), { gamesLeft: left, isdisabled: disable });
                });
            });
            props.update("teams", reenabledOldTeam);
        }
        else {
            var newTeamList = newTeamsArray.map(function (s, _idx) {
                if (_idx !== weekIndex)
                    return s;
                return s.map(function (t, _sidx) {
                    if (t.value !== value.value)
                        return t;
                    if (t.playersLeft > 0)
                        return __assign(__assign({}, t), { isdisabled: false });
                    return __assign(__assign({}, t), { isdisabled: true });
                });
            });
            props.update("teams", newTeamList);
        }
        var newBlueTeam = games.map(function (game, _idx) {
            if (_idx !== weekIndex)
                return game;
            var newGameList = game.gameList.map(function (t, _sidx) {
                if (_sidx !== gameIndex)
                    return t;
                return __assign(__assign({}, t), { blueTeam: value.value });
            });
            return __assign(__assign({}, game), { gameList: newGameList });
        });
        setGames(newBlueTeam);
        checkIfFormComplete(newBlueTeam);
    };
    var checkIfFormComplete = function (weeks) {
        if (weeks.every(function (element) { return element.mapId && element.weekId && element.gameList.every(function (game) { return game.blueTeam && game.redTeam; }); })) {
            setCanSubmitGames(true);
        }
        else {
            setCanSubmitGames(false);
        }
    };
    var update = function (e) {
        props.update(e.target.name, e.target.value);
    };
    // create a list for each engine.
    var renderMapDropdown = function (weekIndex) {
        var select = null;
        if (data.length > 0) {
            var maps = [];
            for (var idx = 0; idx < data.length; idx++) {
                var map = new WorldDoomLeague_1.MapsDto();
                map.id = data[idx].id;
                map.mapName = data[idx].mapName;
                map.mapNumber = data[idx].mapNumber;
                map.mapPack = data[idx].mapPack;
                maps.push(map);
            }
            ;
            select = (React.createElement(react_select_1.default, { options: maps, onChange: function (e) { return handleMapChange(weekIndex, e.id); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: completedGames, isSearchable: true, value: maps.find(function (o) { return o.id == games[weekIndex].mapId; }) || null, getOptionValue: function (value) { return value.id; }, getOptionLabel: function (label) { return label.mapName + " | " + label.mapPack; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No maps left!", value: "Not" }], isDisabled: completedGames }));
        }
        return (select);
    };
    // create a form for entering a new engine.
    var renderNewMapForm = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.FormGroup, null,
                React.createElement(reactstrap_1.Label, { for: "mapName" }, "Map Name"),
                React.createElement(reactstrap_1.Input, { type: "text", name: "mapName", id: "mapName", value: mapName, placeholder: "N's Base of Boppin'", onChange: function (e) { return setMapName(e.target.value); } })),
            React.createElement(reactstrap_1.FormGroup, null,
                React.createElement(reactstrap_1.Label, { for: "mapPack" }, "Pack"),
                React.createElement(reactstrap_1.Input, { type: "text", name: "mapPack", id: "mapPack", value: mapPack, placeholder: "32in24-4", onChange: function (e) { return setMapPack(e.target.value); } })),
            React.createElement(reactstrap_1.FormGroup, null,
                React.createElement(reactstrap_1.Label, { for: "engineUrl" }, "Map Number"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", min: 1, max: 32, type: "number", step: "1", value: mapNumber, onChange: function (e) { return setMapNumber(parseInt(e.target.value, 10)); } })),
            React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !mapName || !mapName || !mapNumber || completedGames, onClick: submitMaps }, "Create New Map")));
    };
    var renderMapSelect = function (weekIndex) {
        return (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, null,
                    React.createElement(reactstrap_1.Form, null,
                        React.createElement(reactstrap_1.FormGroup, null,
                            React.createElement(reactstrap_1.Label, { for: "engine" }, "Map"),
                            renderMapDropdown(weekIndex)))))));
    };
    var renderMapCreate = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, null,
                    React.createElement(reactstrap_1.Form, null, renderNewMapForm())))));
    };
    // create a list for each nominating captain.
    var renderRedTeamSelection = function (weekIndex, gameIndex) {
        var select = null;
        if (props.form.teams) {
            select = (React.createElement(react_select_1.default, { options: props.form.teams[weekIndex], onChange: function (e) { return handleRedTeamSelected(weekIndex, gameIndex, e); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: completedGames, value: props.form.teams[weekIndex].find(function (o) { return o.id == games[weekIndex].gameList[gameIndex].redTeam; }) || null, isSearchable: true }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No teams left!", value: "Not" }], isDisabled: completedGames }));
        }
        return (select);
    };
    // create a list for each nominating captain.
    var renderBlueTeamSelection = function (weekIndex, gameIndex) {
        var select = null;
        if (props.form.teams) {
            select = (React.createElement(react_select_1.default, { options: props.form.teams[weekIndex], onChange: function (e) { return handleBlueTeamSelected(weekIndex, gameIndex, e); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: completedGames, value: props.form.teams[weekIndex].find(function (o) { return o.id == games[weekIndex].gameList[gameIndex].blueTeam; }) || null, isSearchable: true }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No teams left!", value: "Not" }], isDisabled: completedGames }));
        }
        return (select);
    };
    var renderPagination = function (weekIndex) {
        var select = null;
        select = (React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "3", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Pagination, { size: "lg", "aria-label": "Page navigation example" }, weeks && weeks.map(function (week, idx) {
                    return React.createElement(reactstrap_1.PaginationItem, { active: week.weekNumber - 1 == weekIndex },
                        React.createElement(reactstrap_1.PaginationLink, { key: week.id, onClick: function (e) { return setCurrentWeek(week.weekNumber - 1); } }, week.weekNumber));
                })))));
        return (select);
    };
    // render the game list
    var renderGamesList = function (weekIndex) {
        var gameList = (React.createElement(React.Fragment, null, games[weekIndex] && canCreateGames == false && (games[weekIndex].gameList.map(function (game, gameIndex) { return (React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { xs: "6" },
                React.createElement("div", null,
                    "Red Team: ",
                    renderRedTeamSelection(weekIndex, gameIndex)),
                React.createElement("br", null)),
            React.createElement(reactstrap_1.Col, { xs: "6" },
                React.createElement("div", null,
                    "Blue Team: ",
                    renderBlueTeamSelection(weekIndex, gameIndex)),
                React.createElement("br", null)))); }))));
        return (gameList);
    };
    // render the game list container.
    var renderGamesListContainer = function (weekIndex) {
        var games = (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                    React.createElement("h4", { className: "text-center" },
                        "Week #",
                        weeks[weekIndex] && weeks[weekIndex].weekNumber))),
            renderGamesList(weekIndex),
            React.createElement("br", null),
            renderMapSelect(weekIndex),
            React.createElement("br", null),
            renderPagination(weekIndex),
            React.createElement("br", null),
            renderMapCreate(),
            React.createElement("br", null)));
        return (games);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Create Regular Season Games"),
                React.createElement("p", null, "Please input the games that will be scheduled for each week in the regular season."),
                React.createElement(reactstrap_1.Label, { for: "amountTeams" }, "Amount of games per team"),
                React.createElement(reactstrap_1.Input, { placeholder: "Amount", name: "gamesPerTeam", min: 1, max: 4, type: "number", step: "1", value: gamesPerWeek, disabled: true }),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !canCreateGames || loading, onClick: createGames }, "Create Games"),
                React.createElement("hr", null))),
        games && canCreateGames == false && (renderGamesListContainer(currentWeek)),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !canSubmitGames, onClick: submitGames }, "Finalize Games"))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Button, { color: "secondary", size: "lg", block: true, disabled: !completedGames, onClick: redirect }, "Finish")))));
};
exports.default = CreateGames;
//# sourceMappingURL=CreateGames.js.map