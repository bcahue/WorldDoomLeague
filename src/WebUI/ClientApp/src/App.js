"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_router_1 = require("react-router");
var history_1 = require("history");
var Layout_1 = require("./components/Layout");
var Home_1 = require("./components/home/Home");
var LeaderboardAllTimeStats_1 = require("./components/LeaderboardAllTimeStats");
require("./custom.css");
// Create browser history to use in the Redux store
var baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
var history = history_1.createBrowserHistory({ basename: baseUrl });
exports.default = (function () { return (React.createElement(react_router_1.Router, { history: history },
    React.createElement(Layout_1.default, null,
        React.createElement(react_router_1.Route, { exact: true, path: '/', component: Home_1.default }),
        React.createElement(react_router_1.Route, { path: '/leaderboard-all-time', component: LeaderboardAllTimeStats_1.default })))); });
//# sourceMappingURL=App.js.map