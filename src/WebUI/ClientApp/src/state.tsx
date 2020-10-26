import { createGlobalState } from 'react-hooks-global-state';
import { LeaderboardStatsMode, IPlayerLeaderboardStatsDto } from './WorldDoomLeague';

const { setGlobalState, useGlobalState } = createGlobalState({
    errorMessage: '',
    allTimeLeaderboardData: [],
    allTimeLeaderboardMode: LeaderboardStatsMode.Total,
});

export const setErrorMessage = (s: string) => {
    setGlobalState('errorMessage', s);
};

export const setAllTimeLeaderboardData = (s: IPlayerLeaderboardStatsDto[]) => {
    setGlobalState('allTimeLeaderboardData', s);
};

export const setAllTimeLeaderboardMode = (s: LeaderboardStatsMode) => {
    setGlobalState('allTimeLeaderboardMode', s);
};

export { useGlobalState };