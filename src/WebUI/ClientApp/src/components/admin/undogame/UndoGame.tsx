import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    MatchesClient,
    IPlayedGamesVm,
    IPlayedGamesDto,
    UndoMatchCommand
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const UndoGame = props => {
    const [loading, setLoading] = useState(true);
    const [game, setGame] = useState(0);
    const [undidGame, setUndidGame] = useState(0);

    const [gamesData, setGamesData] = useState<IPlayedGamesDto[]>();

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MatchesClient();
                const response = await client.getPlayedGames()
                    .then(response => response.toJSON() as Promise<IPlayedGamesVm>);
                const data = response.playedGameList;
                setGamesData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, [undidGame]);

    const undoGame = async (evt) => {
        try {
            let client = new MatchesClient();
            const command = new UndoMatchCommand;
            command.match = game;
            const response = await client.undo(command);
            setUndidGame(game);
            setGame(0);
        } catch (e) {
            setErrorMessage(JSON.parse(e.response));
        }
    };

    // create a list item for each played game
    const renderPlayedGameList = () => {
        let select = null;
        if (gamesData && gamesData.length > 0) {
            select = (
                <Select
                    options={gamesData}
                    onChange={e => setGame(e.id)}
                    isSearchable={true}
                    getOptionValue={value => value.id.toString()}
                    getOptionLabel={label => `${label.redTeamName} vs ${label.blueTeamName} - ${label.season} Week #${label.weekNumber} - Winner: ${label.winningTeam} | Forfeit: ${label.forfeit}`}
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
                    <h3 className='text-center'>Undo Game</h3>
                    <p>Please select the game to be undo'd. Any game within an ongoing season can be picked thats been played or forfeited, or a finals game from a completed season.</p>
                    <p>Note: If you undo a finals game, the season associated with it will be reset to "ongoing" and the winner will be removed.</p>
                    <Label for="seasonSelect">Select a game</Label>
                    {renderPlayedGameList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(game > 0) || loading} onClick={undoGame}>Undo Game</Button>
                    <hr />
                    {undidGame > 0 && (<h2>Game #{undidGame} has been undid!</h2>)}
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default UndoGame