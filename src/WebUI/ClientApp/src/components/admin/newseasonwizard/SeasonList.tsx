import * as React from 'react';
import { useState, useEffect } from 'react';
import { Spinner, ListGroup, ListGroupItem } from 'reactstrap';
import { setErrorMessage } from '../../../state';
import {
    ISeasonsVm,
    ISeasonDto,
    SeasonsClient
} from '../../../WorldDoomLeague';

const SeasonList = () => {
    const [loading, setLoading] = useState<boolean>(false);
    const [data, setData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new SeasonsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<ISeasonsVm>);
                const data = await response.seasonList;
                setData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    // create a list for each season.
    const renderSeasonList = () => {
        const seasonList = data;
        var listArray = [];
        if (!loading) {
            const seasonObject = seasonList;
            if (seasonList.length > 0) {
                seasonObject.forEach(function (value) {
                    listArray.push(<ListGroupItem>{value.seasonName}</ListGroupItem>);
                });
            } else {
                listArray.push(<ListGroupItem>There are currently no seasons recorded in the system.</ListGroupItem>);
            }
        } else {
            listArray.push(<ListGroupItem><Spinner size="sm" color="primary" /></ListGroupItem>);
        }
        return (listArray);
    };

    return (
        <React.Fragment>
            <ListGroup>
                {renderSeasonList()}
            </ListGroup>
        </React.Fragment>
    );
};

export default SeasonList