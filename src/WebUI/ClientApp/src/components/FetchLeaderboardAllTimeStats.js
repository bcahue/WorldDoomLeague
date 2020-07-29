"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_redux_1 = require("react-redux");
var react_router_1 = require("react-router");
var LeaderboardAllTimeStatsStore = require("../store/LeaderboardAllTime");
var WorldDoomLeague_1 = require("../WorldDoomLeague");
var FetchLeaderboardAllTimeStats = /** @class */ (function (_super) {
    __extends(FetchLeaderboardAllTimeStats, _super);
    function FetchLeaderboardAllTimeStats() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    // This method is called when the component is first added to the document
    FetchLeaderboardAllTimeStats.prototype.componentDidMount = function () {
        this.ensureDataFetched();
    };
    // This method is called when the route parameters change
    FetchLeaderboardAllTimeStats.prototype.componentDidUpdate = function () {
        this.ensureDataFetched();
    };
    FetchLeaderboardAllTimeStats.prototype.render = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement("h1", { id: "tabelLabel" }, "All Time Player Stats"),
            React.createElement("p", null, "This component demonstrates fetching data from the server and working with URL parameters."),
            this.renderModeSelect(),
            this.renderStatsTables()));
    };
    FetchLeaderboardAllTimeStats.prototype.ensureDataFetched = function () {
        this.props.requestLeaderboardsAllTime(this.props.mode || WorldDoomLeague_1.LeaderboardStatsMode.Total);
    };
    // create a new table for each stat.
    FetchLeaderboardAllTimeStats.prototype.renderStatsTables = function () {
        var leaderboards = this.props.leaderboards;
        var tableArray = [];
        if (leaderboards.playerLeaderboardStats !== undefined) {
            var leadersObject = leaderboards.playerLeaderboardStats;
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
            tableArray.push(this.props.isLoading && React.createElement("span", null, "Loading..."));
        }
        return (tableArray);
    };
    FetchLeaderboardAllTimeStats.prototype.handleModeChange = function (e) {
        this.props.requestLeaderboardsAllTime(e.target.value);
    };
    FetchLeaderboardAllTimeStats.prototype.renderModeSelect = function () {
        var _this = this;
        return (React.createElement("div", { className: "d-flex justify-content-between" },
            React.createElement("div", { className: "SetMode" },
                React.createElement("select", { value: this.props.mode, onChange: function (e) { return _this.handleModeChange(e); } }, Object.keys(WorldDoomLeague_1.LeaderboardStatsMode).map(function (key) { return (React.createElement("option", { key: key, value: key }, WorldDoomLeague_1.LeaderboardStatsMode[key])); })))));
    };
    return FetchLeaderboardAllTimeStats;
}(React.PureComponent));
exports.default = react_router_1.withRouter(react_redux_1.connect(function (state) { return state.leaderboardAllTime; }, // Selects which state properties are merged into the component's props
LeaderboardAllTimeStatsStore.actionCreators // Selects which action creators are merged into the component's props
)(FetchLeaderboardAllTimeStats));
//# sourceMappingURL=FetchLeaderboardAllTimeStats.js.map