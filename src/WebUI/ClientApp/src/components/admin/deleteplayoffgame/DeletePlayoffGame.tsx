import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    SeasonsClient,
    IUnfinishedSeasonsVm,
    IUnfinishedSeasonDto,
    MatchesClient,
    IUnplayedPlayoffGamesVm,
    IUnplayedPlayoffGamesDto,
    DeletePlayoffMatchCommand,
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const DeletePlayoffGame = props => {
    const [loading, setLoading] = useState(true);
    const [deleteSuccessful, setDeleteSuccessful] = useState(false);

    const [game, setGame] = useState(0);
    const [season, setSeason] = useState(0);

    const [gamesData, setGamesData] = useState<IUnplayedPlayoffGamesDto[]>([]);
    const [seasonsData, setSeasonsData] = useState<IUnfinishedSeasonDto[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.getUnfinishedSeasons()
                    .then(response => response.toJSON() as Promise<IUnfinishedSeasonsVm>);
                const data = response.seasonList;
                setSeasonsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    const updateUnplayedPlayoffGames = async () => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MatchesClient();
                const response = await client.getUnplayedPlayoffGames(season)
                    .then(response => response.toJSON() as Promise<IUnplayedPlayoffGamesVm>);
                const data = response.unplayedPlayoffGameList;
                setGamesData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const deletePlayoffGame = async (evt) => {
        try {
            let client = new MatchesClient();
            const command = new DeletePlayoffMatchCommand;
            command.match = game;
            const response = await client.deletePlayoffMatch(command);

            setGame(0);

            setDeleteSuccessful(response);

            setGamesData([]);
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

    // create a list item for each game
    const renderGamesList = () => {
        let select = null;
        if (gamesData && gamesData.length > 0) {
            select = (
                <Select
                    options={gamesData}
                    onChange={e => setGame(e.id)}
                    isSearchable={true}
                    value={gamesData.find(o => o.id == game) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.redTeamName} vs. ${label.blueTeamName} - Week #${label.weekNumber} - ${label.scheduledDate}`}
                    isLoading={loading}
                />);
        } else {
            select = (<Select options={[{ label: "No games!", value: "0" }]} />);
        }

        return (select);
    };

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Delete Playoff Game</h3>
                    <p>Please select a season still in progress to delete a playoff game.</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={updateUnplayedPlayoffGames}>Select Season</Button>
                    <br />
                    <Label for="week">Select a game</Label>
                    <br />
                    {renderGamesList()}
                    <br />
                    <h4 className="text-center text-danger">You can only delete unplayed playoff games. Please undo the game first if it has already been played.</h4>
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <br />
                    {deleteSuccessful && (<h2 className="text-center">Game deletion completed!</h2>)}
                    <br />
                    <Button color="danger" size="lg" block disabled={!(season > 0) || !(game > 0) || loading} onClick={deletePlayoffGame}>Delete Playoff Game</Button>
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default DeletePlayoffGame