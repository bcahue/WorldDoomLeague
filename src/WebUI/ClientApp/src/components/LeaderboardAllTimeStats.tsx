import { useEffect, useState, useRef } from 'react';
import * as React from 'react';
import { setErrorMessage, setAllTimeLeaderboardData, setAllTimeLeaderboardMode, useGlobalState } from '../state';
import {
    LeaderboardStatsMode,
    ILeaderboardStatsDto,
    IPlayerLeaderboardAllTimeStatsVm,
    LeaderboardStatsClient
} from '../WorldDoomLeague';


function FetchLeaderboardAllTimeStats() {
    const [loading, setLoading] = useState<boolean>(false);
    const [mode] = useGlobalState('allTimeLeaderboardMode');
    const [data] = useGlobalState('allTimeLeaderboardData');
    const cache = useRef({});

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new LeaderboardStatsClient();
                console.log(typeof (cache.current[mode]));
                if (cache.current[mode] !== undefined) {
                    const data = cache.current[mode];
                    setAllTimeLeaderboardData(data);
                } else {
                    const response = await client.getAllTime(mode)
                        .then(response => response.toJSON() as Promise<IPlayerLeaderboardAllTimeStatsVm>);
                    const data = await response.playerLeaderboardStats;
                    cache.current[mode] = data; // set response in cache;
                    setAllTimeLeaderboardData(data);
                }
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [mode]);

    // create a new table for each stat.
    function renderStatsTables() {
        const leaderboards = data;
        var tableArray = [];
        if (!loading) {
            const leadersObject = leaderboards;
            leadersObject.forEach(function (value) {
                const leaderboardStats = value.leaderboardStats;
                tableArray.push(<div>
                    <h1>{value.statName}</h1>
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th>Rank</th>
                                <th>Name</th>
                                <th>Stat</th>
                            </tr>
                        </thead>
                        <tbody>
                            {leaderboardStats.map((leaderboardStats: ILeaderboardStatsDto) =>
                                <tr key={leaderboardStats.id}>
                                    <td>{leaderboardStats.id}</td>
                                    <td>{leaderboardStats.playerName}</td>
                                    <td>{leaderboardStats.stat}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>);
            });
        } else {
            tableArray.push(<span>Loading...</span>);
        }
        return (tableArray);
    }

    function handleModeChange(e) {
        setAllTimeLeaderboardMode(e.target.value);
    }

    function renderModeSelect() {
        return (
            <div className="d-flex justify-content-between">
                <div className="SetMode">
                    <select value={mode} onChange={(e) => handleModeChange(e)}>
                        {Object.keys(LeaderboardStatsMode).map(key => (
                            <option key={key} value={key}>
                                {LeaderboardStatsMode[key]}
                            </option>
                        ))}
                    </select>
                </div>
            </div>
        );
    }

    return (
        <React.Fragment>
            <h1 id="tabelLabel">All Time Player Stats</h1>
            <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
            {renderModeSelect()}
            {renderStatsTables()}
        </React.Fragment>
    );
}

export default FetchLeaderboardAllTimeStats