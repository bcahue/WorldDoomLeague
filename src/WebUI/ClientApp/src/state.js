"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.useGlobalState = exports.setAllTimeLeaderboardMode = exports.setAllTimeLeaderboardData = exports.setErrorMessage = void 0;
var react_hooks_global_state_1 = require("react-hooks-global-state");
var WorldDoomLeague_1 = require("./WorldDoomLeague");
var _a = react_hooks_global_state_1.createGlobalState({
    errorMessage: {},
    allTimeLeaderboardData: [],
    allTimeLeaderboardMode: WorldDoomLeague_1.LeaderboardStatsMode.Total,
}), setGlobalState = _a.setGlobalState, useGlobalState = _a.useGlobalState;
exports.useGlobalState = useGlobalState;
var setErrorMessage = function (s) {
    setGlobalState('errorMessage', s);
};
exports.setErrorMessage = setErrorMessage;
var setAllTimeLeaderboardData = function (s) {
    setGlobalState('allTimeLeaderboardData', s);
};
exports.setAllTimeLeaderboardData = setAllTimeLeaderboardData;
var setAllTimeLeaderboardMode = function (s) {
    setGlobalState('allTimeLeaderboardMode', s);
};
exports.setAllTimeLeaderboardMode = setAllTimeLeaderboardMode;
//# sourceMappingURL=state.js.map