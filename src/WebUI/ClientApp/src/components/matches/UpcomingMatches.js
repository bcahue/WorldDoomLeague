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
var WorldDoomLeague_1 = require("../../WorldDoomLeague");
var state_1 = require("../../state");
;
var UpcomingMatches = function () {
    var _a = react_1.useState(false), loading = _a[0], setLoading = _a[1];
    var _b = react_1.useState([]), upcomingMatches = _b[0], setUpcomingMatches = _b[1];
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
                        client = new WorldDoomLeague_1.MatchesClient();
                        return [4 /*yield*/, client.getUpcomingGames()
                                .then(function (response) { return response.toJSON(); })];
                    case 2:
                        response = _a.sent();
                        data = response.upcomingMatches;
                        localizeMatches(data);
                        return [3 /*break*/, 4];
                    case 3:
                        e_1 = _a.sent();
                        console.log(e_1);
                        console.log(e_1.response);
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
    var localizeMatches = function (matchData) {
        var localizedMatches = [];
        for (var i = 0; i < matchData.length; i++) {
            if (localizedMatches.length <= 0) {
                var newDate = {
                    weekDay: matchData[i].scheduledTime,
                    matchInfo: [matchData[i]]
                };
                var newMatches = localizedMatches.concat(newDate);
                localizedMatches = newMatches;
            }
            else {
                if (localizedMatches.find(function (f) { return new Date(f.weekDay).toLocaleDateString() == new Date(matchData[i].scheduledTime).toLocaleDateString(); })) {
                    var oldMatchInfo = localizedMatches
                        .find(function (f) { return new Date(f.weekDay).toLocaleDateString() == new Date(matchData[i].scheduledTime).toLocaleDateString(); })
                        .matchInfo;
                    var matchInfo = oldMatchInfo
                        .concat(matchData[i]);
                    localizedMatches
                        .find(function (f) { return new Date(f.weekDay).toLocaleDateString() == new Date(matchData[i].scheduledTime).toLocaleDateString(); })
                        .matchInfo = matchInfo;
                    console.log('Game Placed under existing date.');
                }
                else {
                    var matchInfo = matchData[i];
                    var matches = localizedMatches.concat({
                        weekDay: matchInfo.scheduledTime,
                        matchInfo: [matchInfo]
                    });
                    console.log('New Date Created');
                    localizedMatches = matches;
                }
            }
        }
        setUpcomingMatches(localizedMatches);
    };
    var displayUpcomingMatches = function () {
        var matchGroups = [];
        upcomingMatches.map(function (s, idx) {
            var matches = [];
            s.matchInfo.map(function (t, _idx) {
                matches = matches.concat(React.createElement("div", { className: 'upcomingMatch' },
                    React.createElement("a", { className: 'match a-reset' },
                        React.createElement("div", { className: 'matchInfo' },
                            React.createElement("div", { className: 'matchTime' }, new Intl.DateTimeFormat('default', { hour: 'numeric', minute: 'numeric', timeZoneName: 'short' }).format(new Date(t.scheduledTime)))),
                        React.createElement("div", { className: 'matchTeams' },
                            React.createElement("div", { className: 'matchTeamName' }, t.redTeamName),
                            React.createElement("div", { className: 'matchTeamName' }, t.blueTeamName)),
                        React.createElement("div", { className: 'matchRecords' },
                            React.createElement("div", { className: 'matchRecord' },
                                React.createElement("div", { className: 'text-danger' }, t.redTeamRecord)),
                            React.createElement("div", { className: 'matchRecord' },
                                React.createElement("div", { className: 'text-primary' }, t.blueTeamRecord))),
                        React.createElement("div", { className: 'matchSeason' },
                            React.createElement("div", { className: 'seasonName' }, t.seasonName),
                            React.createElement("div", { className: 'gameType' }, t.gameType)),
                        t.maps && t.maps.length > 0 && t.maps.length <= 1 && (React.createElement("div", { className: 'matchMaps' },
                            React.createElement("div", { className: 'mapName' }, t.maps[0].mapName),
                            React.createElement("div", { className: 'mapPack' }, t.maps[0].mapPack),
                            React.createElement("div", { className: 'mapNumber' }, t.maps[0].mapNumber))),
                        t.maps && t.maps.length > 1 && (React.createElement("div", { className: 'matchMaps' },
                            React.createElement("div", { className: 'mapName' }, "Multiple Maps"))))));
            });
            matchGroups = matchGroups.concat(React.createElement("div", { className: 'upcomingMatchesSection' },
                React.createElement("div", { className: 'matchDayHeadline' }, new Intl.DateTimeFormat('default', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' }).format(new Date(s.weekDay))),
                matches));
        });
        return (matchGroups);
    };
    return (React.createElement("div", null,
        React.createElement("h1", null, "Upcoming Matches"),
        upcomingMatches.length > 0 && displayUpcomingMatches()));
};
exports.default = UpcomingMatches;
//# sourceMappingURL=UpcomingMatches.js.map