import { createGlobalState } from 'react-hooks-global-state';
import { LeaderboardStatsMode, IPlayerLeaderboardStatsDto } from './WorldDoomLeague';
import { IErrorResponse } from './ErrorResponse'

const { setGlobalState, useGlobalState } = createGlobalState({
    errorMessage: {} as IErrorResponse,
    allTimeLeaderboardData: [],
    allTimeLeaderboardMode: LeaderboardStatsMode.Total,
});

export const setErrorMessage = (s: IErrorResponse) => {
    setGlobalState('errorMessage', s);
};

export const setAllTimeLeaderboardData = (s: IPlayerLeaderboardStatsDto[]) => {
    setGlobalState('allTimeLeaderboardData', s);
};

export const setAllTimeLeaderboardMode = (s: LeaderboardStatsMode) => {
    setGlobalState('allTimeLeaderboardMode', s);
};

export { useGlobalState };