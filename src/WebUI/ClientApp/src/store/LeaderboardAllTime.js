"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.reducer = exports.actionCreators = void 0;
var WorldDoomLeague_1 = require("../WorldDoomLeague");
// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).
exports.actionCreators = {
    requestLeaderboardsAllTime: function (mode) { return function (dispatch, getState) {
        // Only load data if it's something we don't already have (and are not already loading)
        var appState = getState();
        if (appState && appState.leaderboardAllTime && mode !== appState.leaderboardAllTime.mode) {
            var client = new WorldDoomLeague_1.LeaderboardStatsClient();
            client.getAllTime(mode)
                .then(function (response) { return response.toJSON(); })
                .then(function (data) {
                dispatch({ type: 'RECEIVE_LEADERBOARD_ALLTIME', mode: mode, leaderboards: data });
            });
            dispatch({ type: 'REQUEST_LEADERBOARD_ALLTIME', mode: mode });
        }
    }; }
};
// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
var unloadedState = { leaderboards: {}, mode: undefined, isLoading: false };
exports.reducer = function (state, incomingAction) {
    if (state === undefined) {
        return unloadedState;
    }
    var action = incomingAction;
    switch (action.type) {
        case 'REQUEST_LEADERBOARD_ALLTIME':
            return {
                mode: action.mode,
                leaderboards: state.leaderboards,
                isLoading: true
            };
        case 'RECEIVE_LEADERBOARD_ALLTIME':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.mode === state.mode) {
                return {
                    mode: action.mode,
                    leaderboards: action.leaderboards,
                    isLoading: false
                };
            }
            break;
    }
    return state;
};
//# sourceMappingURL=LeaderboardAllTime.js.map