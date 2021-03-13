import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    SeasonsClient,
    IUnfinishedSeasonsVm,
    IUnfinishedSeasonDto,
    PlayerTransactionsClient,
    ReverseLastTradeCommand,
} from '../../../WorldDoomLeague';
import { setErrorMessage } from '../../../state';

const ReverseLastTrade = props => {
    const [loading, setLoading] = useState(true);
    const [tradeSuccessful, setTradeSuccessful] = useState(false);

    const [season, setSeason] = useState(0);

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

    const reverseLastTrade = async (evt) => {
        try {
            let client = new PlayerTransactionsClient();
            const command = new ReverseLastTradeCommand;
            command.season = season;
            const response = await client.reverseLastTrade(command);

            setSeason(0);

            setTradeSuccessful(response);
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

    return (
        <React.Fragment>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <h3 className='text-center'>Promote Player to Captain</h3>
                    <p>Please select a season to promote a player.</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <h4 className="text-center text-danger">Warning: This will reverse any trade, but be advised you will not be able to reverse a trade if a game has been played since the trade was made.</h4>
                    <h4 className="text-center text-danger">If you receive an error, undo the previous games before you undo this trade.</h4>
                </Col>
            </Row>
            <Row>
                <Col sm="12" md={{ size: 6, offset: 3 }}>
                    <br />
                    {tradeSuccessful && (<h2 className="text-center">Trade completed!</h2>)}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={reverseLastTrade}>Commit Reversal</Button>
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default ReverseLastTrade