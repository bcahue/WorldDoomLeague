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
var react_1 = require("react");
var React = require("react");
var state_1 = require("../state");
var WorldDoomLeague_1 = require("../WorldDoomLeague");
function FetchLeaderboardAllTimeStats() {
    var _this = this;
    var _a = react_1.useState(false), loading = _a[0], setLoading = _a[1];
    var mode = state_1.useGlobalState('allTimeLeaderboardMode')[0];
    var data = state_1.useGlobalState('allTimeLeaderboardData')[0];
    var cache = react_1.useRef({});
    react_1.useEffect(function () {
        var fetchData = function () { return __awaiter(_this, void 0, void 0, function () {
            var client, data_1, response, data_2, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        setLoading(true);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 6, , 7]);
                        client = new WorldDoomLeague_1.LeaderboardStatsClient();
                        console.log(typeof (cache.current[mode]));
                        if (!(cache.current[mode] !== undefined)) return [3 /*break*/, 2];
                        data_1 = cache.current[mode];
                        state_1.setAllTimeLeaderboardData(data_1);
                        return [3 /*break*/, 5];
                    case 2: return [4 /*yield*/, client.getAllTime(mode)
                            .then(function (response) { return response.toJSON(); })];
                    case 3:
                        response = _a.sent();
                        return [4 /*yield*/, response.playerLeaderboardStats];
                    case 4:
                        data_2 = _a.sent();
                        cache.current[mode] = data_2; // set response in cache;
                        state_1.setAllTimeLeaderboardData(data_2);
                        _a.label = 5;
                    case 5: return [3 /*break*/, 7];
                    case 6:
                        e_1 = _a.sent();
                        state_1.setErrorMessage(JSON.parse(e_1.response));
                        return [3 /*break*/, 7];
                    case 7:
                        setLoading(false);
                        return [2 /*return*/];
                }
            });
        }); };
        fetchData();
    }, [mode]);
    // create a new table for each stat.
    function renderStatsTables() {
        var leaderboards = data;
        var tableArray = [];
        if (!loading) {
            var leadersObject = leaderboards;
            leadersObject.forEach(function (value) {
                var leaderboardStats = value.leaderboardStats;
                tableArray.push(React.createElement("div", null,
                    React.createElement("h1", null, value.statName),
                    React.createElement("table", { className: 'table table-striped', "aria-labelledby": "tabelLabel" },
                        React.createElement("thead", null,
                            React.createElement("tr", null,
                                React.createElement("th", null, "Rank"),
                                React.createElement("th", null, "Name"),
                                React.createElement("th", null, "Stat"))),
                        React.createElement("tbody", null, leaderboardStats.map(function (leaderboardStats) {
                            return React.createElement("tr", { key: leaderboardStats.id },
                                React.createElement("td", null, leaderboardStats.id),
                                React.createElement("td", null, leaderboardStats.playerName),
                                React.createElement("td", null, leaderboardStats.stat));
                        })))));
            });
        }
        else {
            tableArray.push(React.createElement("span", null, "Loading..."));
        }
        return (tableArray);
    }
    function handleModeChange(e) {
        state_1.setAllTimeLeaderboardMode(e.target.value);
    }
    function renderModeSelect() {
        return (React.createElement("div", { className: "d-flex justify-content-between" },
            React.createElement("div", { className: "SetMode" },
                React.createElement("select", { value: mode, onChange: function (e) { return handleModeChange(e); } }, Object.keys(WorldDoomLeague_1.LeaderboardStatsMode).map(function (key) { return (React.createElement("option", { key: key, value: key }, WorldDoomLeague_1.LeaderboardStatsMode[key])); })))));
    }
    return (React.createElement(React.Fragment, null,
        React.createElement("h1", { id: "tabelLabel" }, "All Time Player Stats"),
        React.createElement("p", null, "This component demonstrates fetching data from the server and working with URL parameters."),
        renderModeSelect(),
        renderStatsTables()));
}
exports.default = FetchLeaderboardAllTimeStats;
//# sourceMappingURL=LeaderboardAllTimeStats.js.map