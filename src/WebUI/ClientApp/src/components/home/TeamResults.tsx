import * as React from 'react';
import { useEffect, useState, useRef } from 'react';
import { setErrorMessage, setAllTimeLeaderboardData, setAllTimeLeaderboardMode, useGlobalState } from '../../state';
import { SeasonsClient, ISeasonListVm, ISeasonStandingsDto } from '../../WorldDoomLeague';


function TeamResults() {
    const [loading, setLoading] = useState<boolean>(false);
    const [data, setData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.getCurrentSeasonsStandings()
                    .then(response => response.toJSON() as Promise<ISeasonListVm>);
                const data = await response.seasons;
                setData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    // create a new table for each stat.
    function renderGameStatTables() {
        const gamestats = data;
        var tableArray = [];
        if (!loading) {
            if (gamestats.length > 0) {
                const gameobj = gamestats;
                gameobj.forEach(function (value) {
                    const seasonStandings = value.seasonStandings;
                    tableArray.push(<div>
                        <h1>{value.seasonName}</h1>
                        <table className='table table-striped' aria-labelledby="tabelLabel">
                            <thead>
                                <tr>
                                    <th>Team</th>
                                    <th>GP</th>
                                    <th>RP</th>
                                    <th>PTS</th>
                                    <th>W</th>
                                    <th>T</th>
                                    <th>L</th>
                                    <th>FF</th>
                                    <th>FA</th>
                                    <th>DEF</th>
                                    <th>FRAGS</th>
                                    <th>DMG</th>
                                    <th>TIME</th>
                                </tr>
                            </thead>
                            <tbody>
                                {seasonStandings.map((seasonStandings: ISeasonStandingsDto) =>
                                    <tr key={seasonStandings.teamName}>
                                        <td>{seasonStandings.teamName}</td>
                                        <td>{seasonStandings.gamesPlayed}</td>
                                        <td>{seasonStandings.roundsPlayed}</td>
                                        <td>{seasonStandings.points}</td>
                                        <td>{seasonStandings.wins}</td>
                                        <td>{seasonStandings.ties}</td>
                                        <td>{seasonStandings.losses}</td>
                                        <td>{seasonStandings.flagCapturesFor}</td>
                                        <td>{seasonStandings.flagCapturesAgainst}</td>
                                        <td>{seasonStandings.flagDefenses}</td>
                                        <td>{seasonStandings.frags}</td>
                                        <td>{seasonStandings.damage}</td>
                                        <td>{seasonStandings.timePlayed}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>);
                });
            } else {
                tableArray.push(<span>No seasons are currently active.</span>);
            }
        } else {
            tableArray.push(<span>Loading...</span>);
        }
        return (tableArray);
    }

    return (
        <React.Fragment>
            {renderGameStatTables()}
        </React.Fragment>
    );
}

export default TeamResults