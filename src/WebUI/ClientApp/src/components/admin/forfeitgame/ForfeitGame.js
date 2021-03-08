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
var react_select_1 = require("react-select");
var WorldDoomLeague_1 = require("../../../WorldDoomLeague");
var state_1 = require("../../../state");
var ForfeitGame = function (props) {
    var _a = react_1.useState(true), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState(false), blueTeamForfeits = _b[0], setBlueTeamForfeits = _b[1];
    var _c = react_1.useState(false), redTeamForfeits = _c[0], setRedTeamForfeits = _c[1];
    var _d = react_1.useState(0), game = _d[0], setGame = _d[1];
    var _e = react_1.useState(0), forfeitedGame = _e[0], setForfeitedGame = _e[1];
    var _f = react_1.useState(0), season = _f[0], setSeason = _f[1];
    var _g = react_1.useState([]), gamesData = _g[0], setGamesData = _g[1];
    var _h = react_1.useState([]), seasonsData = _h[0], setSeasonsData = _h[1];
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
    var updateGames = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
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
                            client = new WorldDoomLeague_1.MatchesClient();
                            return [4 /*yield*/, client.getUnplayedGames(season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.unplayedGameList;
                            setGamesData(data);
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
    var forfeitGame = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_3;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MatchesClient();
                    command = new WorldDoomLeague_1.ForfeitMatchCommand;
                    command.match = game;
                    command.blueTeamForfeits = blueTeamForfeits;
                    command.redTeamForfeits = redTeamForfeits;
                    return [4 /*yield*/, client.forfeit(command)];
                case 1:
                    response = _a.sent();
                    setForfeitedGame(game);
                    setGame(0);
                    setRedTeamForfeits(false);
                    setBlueTeamForfeits(false);
                    setGamesData([]);
                    return [3 /*break*/, 3];
                case 2:
                    e_3 = _a.sent();
                    state_1.setErrorMessage(JSON.parse(e_3.response));
                    return [3 /*break*/, 3];
                case 3: return [2 /*return*/];
            }
        });
    }); };
    // create check boxes to assign blame
    var renderForfeitForm = function () {
        var form = null;
        if (gamesData && gamesData.length > 0 && game > 0) {
            form = (React.createElement("div", null,
                React.createElement("p", null, "Please check the team at fault for the forfeit. Both teams may be checked for a double-forfeit."),
                React.createElement(reactstrap_1.FormGroup, { check: true },
                    React.createElement(reactstrap_1.Label, { check: true },
                        React.createElement(reactstrap_1.Input, { type: "checkbox", onClick: function (e) { return setRedTeamForfeits(e.currentTarget.checked); } }),
                        React.createElement("p", { className: "text-danger" }, gamesData.find(function (o) { return o.id == game; }).redTeamName))),
                React.createElement(reactstrap_1.FormGroup, { check: true },
                    React.createElement(reactstrap_1.Label, { check: true },
                        React.createElement(reactstrap_1.Input, { type: "checkbox", onClick: function (e) { return setBlueTeamForfeits(e.currentTarget.checked); } }),
                        React.createElement("p", { className: "text-primary" }, gamesData.find(function (o) { return o.id == game; }).blueTeamName)))));
        }
        return (form);
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
    // create a list item for each unplayed game
    var renderUnplayedGameList = function () {
        var select = null;
        if (gamesData && gamesData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: gamesData, onChange: function (e) { return setGame(e.id); }, isSearchable: true, value: gamesData.find(function (o) { return o.id == game; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.redTeamName + " vs " + label.blueTeamName + " - Week #" + label.weekNumber; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No games!", value: "0" }] }));
        }
        return (select);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Forfeit Game"),
                React.createElement("p", null, "Please select a season to forfeit a game."),
                React.createElement(reactstrap_1.Label, { for: "seasonSelect" }, "Select a season"),
                renderSeasonList(),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(season > 0) || loading, onClick: updateGames }, "Select Season"),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Label, { for: "seasonSelect" }, "Select a game"),
                React.createElement("br", null),
                renderUnplayedGameList(),
                React.createElement("br", null),
                renderForfeitForm(),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(game > 0) || loading || (!redTeamForfeits && !blueTeamForfeits), onClick: forfeitGame }, "Forfeit Game"),
                React.createElement("hr", null),
                forfeitedGame > 0 && (React.createElement("h2", null,
                    "Game #",
                    forfeitedGame,
                    " has been forfeited!"))))));
};
exports.default = ForfeitGame;
//# sourceMappingURL=ForfeitGame.js.map