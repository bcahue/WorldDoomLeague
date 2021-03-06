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
var state_1 = require("../../state");
var WorldDoomLeague_1 = require("../../WorldDoomLeague");
function TeamResults() {
    var _this = this;
    var _a = react_1.useState(false), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState([]), data = _b[0], setData = _b[1];
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(_this, void 0, void 0, function () {
            var client, response, data_1, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        setLoading(true);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 4, , 5]);
                        client = new WorldDoomLeague_1.SeasonsClient();
                        return [4 /*yield*/, client.getCurrentSeasonsStandings()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        return [4 /*yield*/, response.seasons];
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
    }, []);
    // create a new table for each stat.
    function renderGameStatTables() {
        var gamestats = data;
        var tableArray = [];
        if (!loading) {
            if (gamestats.length > 0) {
                var gameobj = gamestats;
                gameobj.forEach(function (value) {
                    var seasonStandings = value.seasonStandings;
                    tableArray.push(React.createElement("div", null,
                        React.createElement("h1", null, value.seasonName),
                        React.createElement("table", { className: 'table table-striped', "aria-labelledby": "tabelLabel" },
                            React.createElement("thead", null,
                                React.createElement("tr", null,
                                    React.createElement("th", null, "Team"),
                                    React.createElement("th", null, "GP"),
                                    React.createElement("th", null, "RP"),
                                    React.createElement("th", null, "PTS"),
                                    React.createElement("th", null, "W"),
                                    React.createElement("th", null, "T"),
                                    React.createElement("th", null, "L"),
                                    React.createElement("th", null, "FF"),
                                    React.createElement("th", null, "FA"),
                                    React.createElement("th", null, "DEF"),
                                    React.createElement("th", null, "FRAGS"),
                                    React.createElement("th", null, "DMG"),
                                    React.createElement("th", null, "TIME"))),
                            React.createElement("tbody", null, seasonStandings.map(function (seasonStandings) {
                                return React.createElement("tr", { key: seasonStandings.teamName },
                                    React.createElement("td", null, seasonStandings.teamName),
                                    React.createElement("td", null, seasonStandings.gamesPlayed),
                                    React.createElement("td", null, seasonStandings.roundsPlayed),
                                    React.createElement("td", null, seasonStandings.points),
                                    React.createElement("td", null, seasonStandings.wins),
                                    React.createElement("td", null, seasonStandings.ties),
                                    React.createElement("td", null, seasonStandings.losses),
                                    React.createElement("td", null, seasonStandings.flagCapturesFor),
                                    React.createElement("td", null, seasonStandings.flagCapturesAgainst),
                                    React.createElement("td", null, seasonStandings.flagDefenses),
                                    React.createElement("td", null, seasonStandings.frags),
                                    React.createElement("td", null, seasonStandings.damage),
                                    React.createElement("td", null, seasonStandings.timePlayed));
                            })))));
                });
            }
            else {
                tableArray.push(React.createElement("span", null, "No seasons are currently active."));
            }
        }
        else {
            tableArray.push(React.createElement("span", null, "Loading..."));
        }
        return (tableArray);
    }
    return (React.createElement(React.Fragment, null, renderGameStatTables()));
}
exports.default = TeamResults;
//# sourceMappingURL=TeamResults.js.map