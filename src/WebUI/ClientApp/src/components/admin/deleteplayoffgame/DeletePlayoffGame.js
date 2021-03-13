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
var DeletePlayoffGame = function (props) {
    var _a = react_1.useState(true), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState(false), deleteSuccessful = _b[0], setDeleteSuccessful = _b[1];
    var _c = react_1.useState(0), game = _c[0], setGame = _c[1];
    var _d = react_1.useState(0), season = _d[0], setSeason = _d[1];
    var _e = react_1.useState([]), gamesData = _e[0], setGamesData = _e[1];
    var _f = react_1.useState([]), seasonsData = _f[0], setSeasonsData = _f[1];
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
    var updateUnplayedPlayoffGames = function () { return __awaiter(void 0, void 0, void 0, function () {
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
                            return [4 /*yield*/, client.getUnplayedPlayoffGames(season)
                                    .then(function (response) { return response.toJSON(); })];
                        case 2:
                            response = _a.sent();
                            data = response.unplayedPlayoffGameList;
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
    var deletePlayoffGame = function (evt) { return __awaiter(void 0, void 0, void 0, function () {
        var client, command, response, e_3;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 2, , 3]);
                    client = new WorldDoomLeague_1.MatchesClient();
                    command = new WorldDoomLeague_1.DeletePlayoffMatchCommand;
                    command.match = game;
                    return [4 /*yield*/, client.deletePlayoffMatch(command)];
                case 1:
                    response = _a.sent();
                    setGame(0);
                    setDeleteSuccessful(response);
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
    // create a list item for each game
    var renderGamesList = function () {
        var select = null;
        if (gamesData && gamesData.length > 0) {
            select = (React.createElement(react_select_1.default, { options: gamesData, onChange: function (e) { return setGame(e.id); }, isSearchable: true, value: gamesData.find(function (o) { return o.id == game; }) || null, getOptionValue: function (value) { return value.id.toString(); }, getOptionLabel: function (label) { return label.redTeamName + " vs. " + label.blueTeamName + " - Week #" + label.weekNumber + " - " + label.scheduledDate; }, isLoading: loading }));
        }
        else {
            select = (React.createElement(react_select_1.default, { options: [{ label: "No games!", value: "0" }] }));
        }
        return (select);
    };
    return (React.createElement(React.Fragment, null,
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("h3", { className: 'text-center' }, "Delete Playoff Game"),
                React.createElement("p", null, "Please select a season still in progress to delete a playoff game."),
                React.createElement(reactstrap_1.Label, { for: "seasonSelect" }, "Select a season"),
                renderSeasonList(),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "primary", size: "lg", block: true, disabled: !(season > 0) || loading, onClick: updateUnplayedPlayoffGames }, "Select Season"),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Label, { for: "week" }, "Select a game"),
                React.createElement("br", null),
                renderGamesList(),
                React.createElement("br", null),
                React.createElement("h4", { className: "text-center text-danger" }, "You can only delete unplayed playoff games. Please undo the game first if it has already been played."))),
        React.createElement(reactstrap_1.Row, null,
            React.createElement(reactstrap_1.Col, { sm: "12", md: { size: 6, offset: 3 } },
                React.createElement("br", null),
                deleteSuccessful && (React.createElement("h2", { className: "text-center" }, "Game deletion completed!")),
                React.createElement("br", null),
                React.createElement(reactstrap_1.Button, { color: "danger", size: "lg", block: true, disabled: !(season > 0) || !(game > 0) || loading, onClick: deletePlayoffGame }, "Delete Playoff Game")))));
};
exports.default = DeletePlayoffGame;
//# sourceMappingURL=DeletePlayoffGame.js.map