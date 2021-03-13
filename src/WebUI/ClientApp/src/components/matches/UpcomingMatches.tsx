import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, FormGroup, Button, Row, Col } from 'reactstrap';
import Select from 'react-select';

import {
    IUpcomingMatchesDto,
    IUpcomingMatchesVm,
    MatchesClient,
    UpcomingMatchesDto
} from '../../WorldDoomLeague';
import { setErrorMessage } from '../../state';

export interface ILocalizedMatches {
    weekDay: Date;
    matchInfo: IUpcomingMatchesDto[];
};

const UpcomingMatches = () => {
    const [loading, setLoading] = useState<boolean>(false);

    const [upcomingMatches, setUpcomingMatches] = useState<ILocalizedMatches[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MatchesClient();
                const response = await client.getUpcomingGames()
                    .then(response => response.toJSON() as Promise<IUpcomingMatchesVm>);
                const data = response.upcomingMatches;

                localizeMatches(data);
            } catch (e) {
                console.log(e);
                console.log(e.response);
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    const localizeMatches = (matchData: IUpcomingMatchesDto[]) => {
        var localizedMatches = [];

        for (var i = 0; i < matchData.length; i++) {
            if (localizedMatches.length <= 0) {
                const newDate = {
                    weekDay: matchData[i].scheduledTime,
                    matchInfo: [matchData[i]]
                };

                const newMatches = localizedMatches.concat(newDate);

                localizedMatches = newMatches;
            } else {
                if (localizedMatches.find(f => new Date(f.weekDay).toLocaleDateString() == new Date(matchData[i].scheduledTime).toLocaleDateString())) {
                    const oldMatchInfo = localizedMatches
                        .find(f => new Date(f.weekDay).toLocaleDateString() == new Date(matchData[i].scheduledTime).toLocaleDateString())
                        .matchInfo as IUpcomingMatchesDto[];

                    const matchInfo = oldMatchInfo
                        .concat(matchData[i]);

                    localizedMatches
                        .find(f => new Date(f.weekDay).toLocaleDateString() == new Date(matchData[i].scheduledTime).toLocaleDateString())
                        .matchInfo = matchInfo;

                    console.log('Game Placed under existing date.');
                } else {
                    const matchInfo = matchData[i];
                    const matches = localizedMatches.concat({
                        weekDay: matchInfo.scheduledTime,
                        matchInfo: [matchInfo]
                    });

                    console.log('New Date Created');

                    localizedMatches = matches;
                }
            }
        }

        setUpcomingMatches(localizedMatches);
    };

    const displayUpcomingMatches = () => {
        var matchGroups = [];

        upcomingMatches.map((s, idx) => {
            var matches = [];
            s.matchInfo.map((t, _idx) => {
                matches = matches.concat(
                    <div className='upcomingMatch'>
                        <a className='match a-reset'>
                            <div className='matchInfo'>
                                <div className='matchTime'>
                                    {new Intl.DateTimeFormat('default', { hour: 'numeric', minute: 'numeric', timeZoneName: 'short' } as Intl.DateTimeFormatOptions).format(new Date(t.scheduledTime))}
                                </div>
                            </div>
                            <div className='matchTeams'>
                                <div className='matchTeamName'>
                                    {t.redTeamName}
                                </div>
                                <div className='matchTeamName'>
                                    {t.blueTeamName}
                                </div>
                            </div>
                            <div className='matchRecords'>
                                <div className='matchRecord'>
                                    <div className='text-danger'>{t.redTeamRecord}</div>
                                </div>
                                <div className='matchRecord'>
                                    <div className='text-primary'>{t.blueTeamRecord}</div>
                                </div>
                            </div>
                            <div className='matchSeason'>
                                <div className='seasonName'>
                                    {t.seasonName}
                                </div>
                                <div className='gameType'>
                                    {t.gameType}
                                </div>
                            </div>
                            {t.maps && t.maps.length > 0 && t.maps.length <= 1 && (
                                <div className='matchMaps'>
                                    <div className='mapName'>
                                        {t.maps[0].mapName}
                                    </div>
                                    <div className='mapPack'>
                                        {t.maps[0].mapPack}
                                    </div>
                                    <div className='mapNumber'>
                                        {t.maps[0].mapNumber}
                                    </div>
                                </div>
                            )}
                            {t.maps && t.maps.length > 1 && (
                                <div className='matchMaps'>
                                    <div className='mapName'>
                                        Multiple Maps
                                    </div>
                                </div>
                            )}
                        </a>
                    </div>
                );
            });
            matchGroups = matchGroups.concat(
            <div className='upcomingMatchesSection'>
                <div className='matchDayHeadline'>
                    {new Intl.DateTimeFormat('default', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' } as Intl.DateTimeFormatOptions).format(new Date(s.weekDay))}
                </div>
                {matches}
            </div>
            );
        });

        return (matchGroups);
    };

    return (
        <div>
            <h1>Upcoming Matches</h1>
            {upcomingMatches.length > 0 && displayUpcomingMatches()}
        </div>
    );
};

export default UpcomingMatches;