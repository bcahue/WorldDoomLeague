import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    MatchesClient,
    UndoMatchCommand,
    IUnplayedGamesDto,
    IUnplayedGamesVm,
    SeasonsClient,
    ISeasonsVm,
    ISeasonDto,
    ForfeitMatchCommand
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const ForfeitGame = props => {
    const [loading, setLoading] = useState(true);
    const [blueTeamForfeits, setBlueTeamForfeits] = useState(false);
    const [redTeamForfeits, setRedTeamForfeits] = useState(false);
    const [game, setGame] = useState(0);
    const [forfeitedGame, setForfeitedGame] = useState(0);
    const [season, setSeason] = useState(0);

    const [gamesData, setGamesData] = useState<IUnplayedGamesDto[]>([]);
    const [seasonsData, setSeasonsData] = useState<ISeasonDto[]>([]);

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

    const forfeitGame = async (evt) => {
        try {
            let client = new MatchesClient();
            const command = new ForfeitMatchCommand;
            command.match = game;
            command.blueTeamForfeits = blueTeamForfeits;
            command.redTeamForfeits = redTeamForfeits;
            const response = await client.forfeit(command);
            setForfeitedGame(game);
            setGame(0);
            setRedTeamForfeits(false);
            setBlueTeamForfeits(false);
            setGamesData([]);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create check boxes to assign blame
    const renderForfeitForm = () => {
        let form = null;
        if (gamesData && gamesData.length > 0 && game > 0) {
            form = (
                <div>
                    <p>Please check the team at fault for the forfeit. Both teams may be checked for a double-forfeit.</p>
                    <FormGroup check>
                        <Label check>
                            <Input type="checkbox" onClick={e => setRedTeamForfeits(e.currentTarget.checked)} />
                            <p className="text-danger">{gamesData.find(o => o.id == game).redTeamName}</p>
                        </Label>
                    </FormGroup>
                    <FormGroup check>
                        <Label check>
                            <Input type="checkbox" onClick={e => setBlueTeamForfeits(e.currentTarget.checked)} />
                            <p className="text-primary">{gamesData.find(o => o.id == game).blueTeamName}</p>
                        </Label>
                    </FormGroup>
                </div>);
        }

        return (form);
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
            select = (
                <Select
                    options={gamesData}
                    onChange={e => setGame(e.id)}
                    isSearchable={true}
                    value={gamesData.find(o => o.id == game) || null}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.redTeamName} vs ${label.blueTeamName} - Week #${label.weekNumber}`}
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
                    <h3 className='text-center'>Forfeit Game</h3>
                    <p>Please select a season to forfeit a game.</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={updateGames}>Select Season</Button>
                    <br />
                    <Label for="seasonSelect">Select a game</Label>
                    <br />
                    {renderUnplayedGameList()}
                    <br />
                    {renderForfeitForm()}
                    <Button color="primary" size="lg" block disabled={!(game > 0) || loading || (!redTeamForfeits && !blueTeamForfeits)} onClick={forfeitGame}>Forfeit Game</Button>
                    <hr />
                    {forfeitedGame > 0 && (<h2>Game #{forfeitedGame} has been forfeited!</h2>)}
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default ForfeitGame