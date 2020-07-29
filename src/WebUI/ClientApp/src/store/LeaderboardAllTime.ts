import { Action, Reducer } from 'redux';
import { normalize, schema } from 'normalizr';
import { AppThunkAction } from './';
import {
    LeaderboardStatsMode,
    LeaderboardStatsClient,
    IPlayerLeaderboardAllTimeStatsVm
} from '../WorldDoomLeague'

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface LeaderboardAllTimeState {
    isLoading: boolean;
    mode?: LeaderboardStatsMode;
    leaderboards: IPlayerLeaderboardAllTimeStatsVm;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestLeaderboardAllTimeAction {
    type: 'REQUEST_LEADERBOARD_ALLTIME';
    mode: LeaderboardStatsMode;
}

interface ReceiveLeaderboardAllTimeAction {
    type: 'RECEIVE_LEADERBOARD_ALLTIME';
    mode: LeaderboardStatsMode;
    leaderboards: IPlayerLeaderboardAllTimeStatsVm;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestLeaderboardAllTimeAction | ReceiveLeaderboardAllTimeAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestLeaderboardsAllTime: (mode: LeaderboardStatsMode): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.leaderboardAllTime && mode !== appState.leaderboardAllTime.mode) {
            let client = new LeaderboardStatsClient();
            client.getAllTime(mode)
                .then(response => response.toJSON() as Promise<IPlayerLeaderboardAllTimeStatsVm>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_LEADERBOARD_ALLTIME', mode: mode, leaderboards: data });
                });
            dispatch({ type: 'REQUEST_LEADERBOARD_ALLTIME', mode: mode });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: LeaderboardAllTimeState = {leaderboards: {}, mode: undefined, isLoading: false };

export const reducer: Reducer<LeaderboardAllTimeState> = (state: LeaderboardAllTimeState | undefined, incomingAction: Action): LeaderboardAllTimeState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
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
