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
var ProcessGameWizard = function (props) {
    var _a = react_1.useState(true), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState(0), season = _b[0], setSeason = _b[1];
    var _c = react_1.useState(0), game = _c[0], setGame = _c[1];
    var _d = react_1.useState(0), processedGame = _d[0], setProcessedGame = _d[1];
    var _e = react_1.useState(1), numRounds = _e[0], setNumRounds = _e[1];
    var _f = react_1.useState(false), flipTeams = _f[0], setFlipTeams = _f[1];
    var _g = react_1.useState(false), canProcessGame = _g[0], setCanProcessGame = _g[1];
    var _h = react_1.useState(false), completedGame = _h[0], setCompletedGame = _h[1];
    var _j = react_1.useState(true), canSelectSeason = _j[0], setCanSelectSeason = _j[1];
    var _k = react_1.useState(false), canSelectGame = _k[0], setCanSelectGame = _k[1];
    var _l = react_1.useState(false), canCreateRounds = _l[0], setCanCreateRounds = _l[1];
    var _m = react_1.useState(), seasonData = _m[0], setSeasonData = _m[1];
    var _o = react_1.useState(), rounds = _o[0], setRounds = _o[1];
    var _p = react_1.useState(), roundData = _p[0], setRoundData = _p[1];
    var _q = react_1.useState(), maps = _q[0], setMaps = _q[1];
    var _r = react_1.useState(), unplayedGames = _r[0], setUnplayedGames = _r[1];
    var _s = react_1.useState(), playerLineup = _s[0], setPlayerLineup = _s[1];
    var _t = react_1.useState([{
            fileName: "",
            isdisabled: false
        }]), roundFiles = _t[0], setRoundFiles = _t[1];
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
                        return [4 /*yield*/, client.get()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        data = response.seasonList;
                        setSeasonData(data);
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
    var pad_array = function (arr, len, fill) {
        return arr.concat(Array(len).fill(fill)).slice(0, len);
    };
    var getRound = function (roundFile) { return __awaiter(void 0, void 0, void 0, function () {
        var fetchData;
        return __generator(this, function (_a) {
            fetchData = function (roundFile) { return __awaiter(void 0, void 0, void 0, function () {
                var client, response, roundData_1, e_2;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            setLoading(true);
                            _a.label = 1;
                        case 1:
                            _a.trys.push([1, 3, , 4]);
                            client = new WorldDoomLeague_1.FilesClient();
                            return [4 /*yield*/, client.getRoundObject(roundFile)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            roundData_1 = response;
                            setLoading(false);
                            return [2 /*return*/, roundData_1];
                        case 3:
                            e_2 = _a.sent();
                            state_1.setErrorMessage(JSON.parse(e_2.response));
                            return [3 /*break*/, 4];
                        case 4: return [2 /*return*/];
                    }
                });
            }); };
            return [2 /*return*/, fetchData(roundFile)];
        });
    }); };
    var processGame = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, gameRounds, idx, round, response, e_3;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MatchesClient();
                    command = new WorldDoomLeague_1.ProcessMatchCommand;
                    gameRounds = [];
                    for (idx = 0; idx < rounds.length; idx++) {
                        round = new WorldDoomLeague_1.RoundObject();
                        round.roundFileName = rounds[idx].roundFileName;
                        round.map = rounds[idx].map;
                        round.redTeamPlayerIds = [];
                        round.blueTeamPlayerIds = [];
                        rounds[idx].redTeamPlayerIds.map(function (s, _sidx) {
                            round.redTeamPlayerIds.push(s);
                        });
                        rounds[idx].blueTeamPlayerIds.map(function (s, _sidx) {
                            round.blueTeamPlayerIds.push(s);
                        });
                        gameRounds.push(round);
                    }
                    command.flipTeams = flipTeams;
                    command.matchId = game;
                    command.gameRounds = gameRounds;
                    return [4 /*yield*/, client.process(command)];
                case 1:
                    response = _a.sent();
                    setCanProcessGame(false);
                    setCanSelectGame(false);
                    setCanSelectSeason(true);
                    setCanCreateRounds(false);
                    setRounds(null);
                    setRoundData(null);
                    setRoundFiles([{ fileName: "", isdisabled: false }]);
                    setMaps(null);
                    setFlipTeams(false);
                    setGame(0);
                    setProcessedGame(response);
                    setSeason(0);
                    return [3 /*break*/, 3];
                case 2:
                    e_3 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_3.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var selectSeason = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, response, e_4;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MatchesClient();
                    return [4 /*yield*/, client.getUnplayedGames(season)
                            .then(function (response) { return response.toJSON(); })];
                case 1:
                    response = _a.sent();
                    setUnplayedGames(response.unplayedGameList);
                    setCanSelectSeason(false);
                    setCanSelectGame(true);
                    return [3 /*break*/, 3];
                case 2:
                    e_4 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_4.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    var selectGame = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var matchClient, fileClient, matchResponse, fileResponse, mapResponse, fileNames, newRound, e_5;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 4, , 5]);
                    matchClient = new WorldDoomLeague_1.MatchesClient();
                    fileClient = new WorldDoomLeague_1.FilesClient();
                    return [4 /*yield*/, matchClient.getPlayerLineup(game)
                            .then(function (response) { return response.toJSON(); })];
                case 1:
                    matchResponse = _a.sent();
                    return [4 /*yield*/, fileClient.getRoundJsonFiles()];
                case 2:
                    fileResponse = _a.sent();
                    return [4 /*yield*/, matchClient.getGameMaps(game)
                            .then(function (response) { return response.toJSON(); })];
                case 3:
                    mapResponse = _a.sent();
                    fileNames = fileResponse.map(function (s, _idx) {
                        return { fileName: s, isdisabled: false };
                    });
                    newRound = pad_array([], 1, {
                        redTeamPlayerIds: [],
                        blueTeamPlayerIds: [],
                        map: null,
                        roundFileName: ""
                    });
                    setRoundFiles(fileNames);
                    setPlayerLineup(matchResponse.gamePlayerLineup);
                    setMaps(mapResponse.gameMaps);
                    setRounds(newRound);
                    setCanSelectGame(false);
                    setCanCreateRounds(true);
                    return [3 /*break*/, 5];
                case 4:
                    e_5 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_5.response));
                    return [3 /*break*/, 5];
                case 5: return [2 /*return*/];
            }
        });
    }); };
    var checkIfFormComplete = function (rounds) {
        if (rounds.every(function (element) { return element.map && element.roundFileName && element.blueTeamPlayerIds.every(function (number) { return number != null; }) && element.blueTeamPlayerIds.every(function (number) { return number != null; }); })) {
            setCanProcessGame(true);
        }
        else {
            setCanProcessGame(false);
        }
    };
    var handleMapChange = function (roundIndex, value) {
        var newRounds = rounds.map(function (round, _idx) {
            if (_idx !== roundIndex)
                return round;
            return __assign(__assign({}, round), { map: value });
        });
        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };
    var handleRoundFileSelected = function (roundIndex, roundFile) { return __awaiter(void 0, void 0, void 0, function () {
        var roundFileNames, reenabledOldRound, disabledRound, round, newRoundData, newData, newRounds;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    roundFileNames = roundFiles;
                    if (rounds[roundIndex].roundFileName !== "") {
                        reenabledOldRound = roundFileNames.map(function (s, _idx) {
                            if (s.fileName !== rounds[roundIndex].roundFileName)
                                return s;
                            return __assign(__assign({}, s), { isdisabled: false });
                        });
                        roundFileNames = reenabledOldRound;
                    }
                    disabledRound = roundFileNames.map(function (s, _idx) {
                        if (s.fileName !== roundFile)
                            return s;
                        return __assign(__assign({}, s), { isdisabled: true });
                    });
                    setRoundFiles(disabledRound);
                    return [4 /*yield*/, getRound(roundFile)];
                case 1:
                    round = _a.sent();
                    newRoundData = null;
                    if (roundData) {
                        if (roundData[roundIndex]) {
                            newRoundData = roundData.map(function (gameRound, idx) {
                                if (idx !== roundIndex)
                                    return gameRound;
                                return round;
                            });
                        }
                        else {
                            newRoundData = roundData.concat(round);
                        }
                    }
                    else {
                        newData = [];
                        newRoundData = newData.concat(round);
                    }
                    setRoundData(newRoundData);
                    newRounds = rounds.map(function (round, _idx) {
                        if (_idx !== roundIndex)
                            return round;
                        return __assign(__assign({}, round), { roundFileName: roundFile, redTeamPlayerIds: pad_array([], flipTeams ? newRoundData[_idx].blueTeamStats.teamPlayers.length : newRoundData[_idx].redTeamStats.teamPlayers.length, null), blueTeamPlayerIds: pad_array([], flipTeams ? newRoundData[_idx].redTeamStats.teamPlayers.length : newRoundData[_idx].blueTeamStats.teamPlayers.length, null) });
                    });
                    setRounds(newRounds);
                    checkIfFormComplete(newRounds);
                    return [2 /*return*/];
            }
        });
    }); };
    var handleFlipTeamsChecked = function (value) {
        var newRounds = rounds.map(function (round, _idx) {
            if (roundData && roundData[_idx]) {
                return __assign(__assign({}, round), { redTeamPlayerIds: pad_array([], value ? roundData[_idx].blueTeamStats.teamPlayers.length : roundData[_idx].redTeamStats.teamPlayers.length, null), blueTeamPlayerIds: pad_array([], value ? roundData[_idx].redTeamStats.teamPlayers.length : roundData[_idx].blueTeamStats.teamPlayers.length, null) });
            }
            else {
                return round;
            }
        });
        setRounds(newRounds);
        setFlipTeams(value);
        checkIfFormComplete(newRounds);
    };
    var handleRedPlayerSelected = function (roundIndex, playerIndex, value) {
        var newRounds = rounds.map(function (round, idx) {
            if (idx !== roundIndex) {
                return round;
            }
            var redIds = round.redTeamPlayerIds.map(function (id, _idx) {
                if (playerIndex !== _idx) {
                    return id;
                }
                return value;
            });
            return __assign(__assign({}, round), { redTeamPlayerIds: redIds });
        });
        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };
    var handleBluePlayerSelected = function (roundIndex, playerIndex, value) {
        var newRounds = rounds.map(function (round, idx) {
            if (idx !== roundIndex) {
                return round;
            }
            var redIds = round.blueTeamPlayerIds.map(function (id, _idx) {
                if (playerIndex !== _idx) {
                    return id;
                }
                return value;
            });
            return __assign(__assign({}, round), { blueTeamPlayerIds: redIds });
        });
        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };
    var handleRemoveRound = function (roundIndex) {
        if (rounds[roundIndex].roundFileName !== "") {
            var reenabledOldRound = roundFiles.map(function (s, _idx) {
                if (s.fileName !== rounds[roundIndex].roundFileName)
                    return s;
                return __assign(__assign({}, s), { isdisabled: false });
            });
            setRoundFiles(reenabledOldRound);
        }
        var newRounds = rounds.filter(function (round, idx) { return idx !== roundIndex; });
        if (roundData && roundData[roundIndex]) {
            var newRoundData = roundData.filter(function (round, idx) { return idx !== roundIndex; });
            setRoundData(newRoundData);
        }
        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };
    var handleAddRound = function () {
        var newRounds = rounds.concat({
            roundFileName: "",
            map: null,
            blueTeamPlayerIds: [],
            redTeamPlayerIds: []
        });
        setRounds(newRounds);
        checkIfFormComplete(newRounds);
    };
    // create a list for each game map.
    var renderFlipTeamsCheckbox = function () {
        return (React.createElement(reactstrap_1.FormGroup, { check: true },
            React.createElement(reactstrap_1.Label, { check: true },
                React.createElement(reactstrap_1.Input, { type: "checkbox", onChange: function (e) { return handleFlipTeamsChecked(e.target.checked); } }),
                ' ',
                "Flip Teams")));
    };
    // create a list for each game map.
    var renderMapSelect = function (roundIndex) {
        var select = null;
        if (maps.length > 0) {
            select = (React.createElement(react_select_1.default, { options: maps, onChange: function (e) { return handleMapChange(roundIndex, e.id); }, isDisabled: completedGame, isSearchable: true, value: maps.find(function (o) { return o.id == rounds[roundIndex].map; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.mapName + " | " + label.mapPack; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No maps left!", value: "Not" }], isDisabled: completedGame }));
        }
        return (select);
    };
    // create a list for the round files.
    var renderRedPlayerSelection = function (roundIndex) {
        var select = [];
        var players = [];
        if (roundData && roundData[roundIndex]) {
            if (flipTeams) {
                players = roundData[roundIndex].blueTeamStats.teamPlayers;
            }
            else {
                players = roundData[roundIndex].redTeamStats.teamPlayers;
            }
            if (playerLineup.redTeamPlayers) {
                players.map(function (player, idx) {
                    select = select.concat(React.createElement("div", null,
                        React.createElement("h4", null, player),
                        React.createElement(react_select_1.default, { options: playerLineup.redTeamPlayers, onChange: function (e) { return handleRedPlayerSelected(roundIndex, idx, e.id); }, isDisabled: completedGame, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.playerName; }, value: playerLineup.redTeamPlayers.find(function (o) { return o.id == rounds[roundIndex].redTeamPlayerIds[idx]; }) || null, isSearchable: true, isLoading: loading })));
                });
            }
            else {
                select.concat(React.createElement(react_select_1.default, { options: [{ label: "No players left!", value: "Not" }], isDisabled: completedGame }));
            }
        }
        return (select);
    };
    // create a list for each nominating captain.
    var renderBluePlayerSelection = function (roundIndex) {
        var select = [];
        var players = [];
        if (roundData && roundData[roundIndex]) {
            if (flipTeams) {
                players = roundData[roundIndex].redTeamStats.teamPlayers;
            }
            else {
                players = roundData[roundIndex].blueTeamStats.teamPlayers;
            }
            if (playerLineup.blueTeamPlayers) {
                players.map(function (player, idx) {
                    select = select.concat(React.createElement("div", null,
                        React.createElement("h4", null, player),
                        React.createElement(react_select_1.default, { options: playerLineup.blueTeamPlayers, onChange: function (e) { return handleBluePlayerSelected(roundIndex, idx, e.id); }, isDisabled: completedGame, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.playerName; }, value: playerLineup.blueTeamPlayers.find(function (o) { return o.id == rounds[roundIndex].blueTeamPlayerIds[idx]; }) || null, isSearchable: true, isLoading: loading })));
                });
            }
            else {
                select.concat(React.createElement(react_select_1.default, { options: [{ label: "No players left!", value: "Not" }], isDisabled: completedGame }));
            }
        }
        return (select);
    };
    // create a list for each nominating captain.
    var renderRoundFileSelect = function (roundIndex) {
        var select = null;
        if (roundFiles) {
            select = (React.createElement(react_select_1.default, { options: roundFiles, onChange: function (e) { return handleRoundFileSelected(roundIndex, e.fileName); }, isOptionDisabled: function (option) { return option.isdisabled; }, isDisabled: completedGame, value: roundFiles.find(function (o) { return o.fileName == rounds[roundIndex].roundFileName; }) || null, getOptionValue: function (value) { return value.fileName; }, getOptionLabel: function (label) { return label.fileName; }, isSearchable: true, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No teams left!", value: "Not" }], isDisabled: completedGame }));
        }
        return (select);
    };
    // create a list for each season
    var renderSeasonList = function () {
        var select = null;
        if (seasonData && seasonData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: seasonData, onChange: function (e) { return setSeason(e.id); }, isDisabled: completedGame || !canSelectSeason, isSearchable: true, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.seasonName; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No seasons!", value: "Not" }], isDisabled: completedGame }));
        }
        return (select);
    };
    var renderRemoveRoundButton = function (roundIndex) {
        return (React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(rounds.length > 1) || loading, onClick: function (e) { return handleRemoveRound(roundIndex); } },
            "Remove Round #",
            roundIndex + 1));
    };
    var renderAddRoundButton = function () {
        return (React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(rounds.every(function (element) { return element.roundFileName !== ""; })) || loading, onClick: function (e) { return handleAddRound(); } }, "Add Round"));
    };
    // create a list for each season
    var renderGameList = function () {
        var select = null;
        if (unplayedGames) {
            select = (React.createElement(react_select_1.default, { options: unplayedGames, onChange: function (e) { return setGame(e.id); }, isDisabled: completedGame || !canSelectGame, isSearchable: true, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.redTeamName + " vs " + label.blueTeamName + " - Week " + label.weekNumber; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No games!", value: "Not" }], isDisabled: completedGame }));
        }
        return (select);
    };
    // render the game list
    var renderRoundList = function () {
        var gameList = (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                    renderFlipTeamsCheckbox(),
                    React.createElement("br", null))),
            rounds.map(function (round, roundIndex) { return (React.createElement("div", null,
                React.createElement(reactstrap_1.Row, null,
                    React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                        React.createElement("h2", { className: "text-center" },
                            "Round #",
                            roundIndex + 1),
                        React.createElement("br", null),
                        React.createElement("h4", { className: "text-center" }, "Round File"),
                        renderRoundFileSelect(roundIndex),
                        React.createElement("br", null))),
                React.createElement(reactstrap_1.Row, null,
                    React.createElement(reactstrap_1.Col, { xs: "6" },
                        React.createElement("div", null,
                            React.createElement("h2", { className: flipTeams ? "text-primary text-center" : "text-danger text-center" }, playerLineup.redTeamName),
                            renderRedPlayerSelection(roundIndex),
                            React.createElement("br", null)),
                        React.createElement("br", null)),
                    React.createElement(reactstrap_1.Col, { xs: "6" },
                        React.createElement("div", null,
                            React.createElement("h2", { className: flipTeams ? "text-danger text-center" : "text-primary text-center" }, playerLineup.blueTeamName),
                            renderBluePlayerSelection(roundIndex),
                            React.createElement("br", null)),
                        React.createElement("br", null))),
                React.createElement(reactstrap_1.Row, null,
                    React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                        React.createElement("h4", { className: "text-center" }, "Map"),
                        renderMapSelect(roundIndex),
                        React.createElement("br", null))),
                React.createElement(reactstrap_1.Row, null,
                    React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                        renderRemoveRoundButton(roundIndex),
                        React.createElement("hr", null))))); }),
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                    renderAddRoundButton(),
                    React.createElement("br", null)))));
        return (gameList);
    };
    // render the game list container.
    var renderRoundListContainer = function () {
        var games = (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.Row, null,
                React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                    React.createElement("h4", { className: "text-center" },
                        playerLineup.redTeamName,
                        " vs. ",
                        playerLineup.blueTeamName))),
            renderRoundList(),
            React.createElement("br", null)));
        return (games);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Process Game"),
                React.createElement("p", null, "Please select the ongoing season where a game needs to be played."),
                React.createElement(reactstrap_1.Label, { for: "seasonSelect" }, "Select a season"),
                renderSeasonList(),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(season > 0) || loading || !canSelectSeason, onClick: selectSeason }, "Select Season"),
                React.createElement("hr", null))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Label, { for: "gameSelect" }, "Select a Game"),
                renderGameList(),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(game > 0) || loading || !canSelectGame, onClick: selectGame }, "Select Game"),
                React.createElement("hr", null))),
        canCreateRounds && (renderRoundListContainer()),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !canProcessGame, onClick: processGame }, "Process Game"),
                React.createElement("br", null))),
        processedGame > 0 && (React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' },
                    "Game #",
                    processedGame,
                    " has been processed!"))))));
};
exports.default = ProcessGameWizard;
//# sourceMappingURL=ProcessGameWizard.js.map