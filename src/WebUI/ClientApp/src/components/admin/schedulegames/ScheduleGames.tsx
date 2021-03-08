import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    MatchesClient,
    SeasonsClient,
    ISeasonsVm,
    ISeasonDto,
    ScheduleMatchCommand,
    IUnplayedGamesDto,
    IUnplayedGamesVm
} from '../../../WorldDoomLeague';
import DateTimePicker from 'react-datetime-picker';
import { setErrorMessage } from '../../../state';

const ScheduleGames = props => {
    const [loading, setLoading] = useState(true);
    const [season, setSeason] = useState(0);

    const [seasonsData, setSeasonsData] = useState<ISeasonDto[]>([]);
    const [gamesData, setGamesData] = useState<IUnplayedGamesDto[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<ISeasonsVm>);
                const data = response.seasonList;
                setSeasonsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    const updateGames = async (evt) => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MatchesClient();
                const response = await client.getUnplayedGames(season)
                    .then(response => response.toJSON() as Promise<IUnplayedGamesVm>);
                const data = response.unplayedGameList;
                setGamesData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const scheduleGame = async (game: number, date: Date) => {
        try {
            let client = new MatchesClient();
            const command = new ScheduleMatchCommand;
            command.match = game;
            command.gameDateTime = date;
            const response = await client.schedule(command);
            await updateGames(null);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create a list item for each season
    const renderSeasonList = () => {
        let select = null;
        if (seasonsData && seasonsData.length > 0) {
            select = (
                <Select
                    options={seasonsData}
                    onChange={e => setSeason(e.id)}
                    isSearchable={true}
                    value={seasonsData.find(o => o.id == season) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => label.seasonName}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No seasons!", value: "0" }]} />);
        }

        return (select);
    };

    // create a list item for each unplayed game
    const renderUnplayedGameList = () => {
        let select = null;
        if (gamesData && gamesData.length > 0) {
            select = (gamesData.map((game, idx) =>
                <div>
                    <h4>{game.redTeamName} vs {game.blueTeamName} - Week #{game.weekNumber}</h4>
                    <br />
                    <DateTimePicker
                        id={'gameDate' + idx}
                        name={'signupDates' + idx}
                        onChange={e => scheduleGame(game.id, e)}
                        value={typeof game.scheduledDate === "string" ? new Date(game.scheduledDate) : game.scheduledDate} />
                    <br />
                    <br />
                </div>
            ));
        }

        return (select);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Schedule Games</h3>
                    <p>Please select a season to schedule games.</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={updateGames}>Select Season</Button>
                    <br />
                    {gamesData && gamesData.length > 0 && (
                        <p>Schedule the time in your local time zone and it will be translated to the server in UTC.</p>
                    )}
                    {renderUnplayedGameList()}
                    <br />
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default ScheduleGames