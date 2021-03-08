import * as React from 'react';
import { useState, useEffect } from 'react';
import { Label, Input, Row, Col, Button, FormGroup, Form } from 'reactstrap';
import Select from 'react-select';
import {
    SeasonsClient,
    ISeasonsVm,
    ISeasonDto,
    AssignTeamHomefieldCommand,
    IUnplayedGamesDto,
    IUnplayedGamesVm,
    ITeamsDto,
    TeamsClient,
    ITeamsVm,
    IMapsDto,
    MapsClient,
    IMapsVm
} from '../../../WorldDoomLeague';
import DateTimePicker from 'react-datetime-picker';
import { setErrorMessage } from '../../../state';

const SelectHomefieldMaps = props => {
    const [loading, setLoading] = useState(true);
    const [season, setSeason] = useState(0);

    const [seasonsData, setSeasonsData] = useState<ISeasonDto[]>([]);
    const [teamsData, setTeamsData] = useState<ITeamsDto[]>([]);
    const [mapsData, setMapsData] = useState([]);

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

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new MapsClient();
                const response = await client.get()
                    .then(response => response.toJSON() as Promise<IMapsVm>);
                const data = response.mapList;
                setMapsData(data);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    }, []);

    const updateTeams = async (evt) => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let client = new TeamsClient();
                const response = await client.getTeamsBySeasonId(season)
                    .then(response => response.toJSON() as Promise<ITeamsVm>);
                const data = response.teamList;
                setTeamsData(data);

                // loop thru each team and disable their homefield from the map list.
                var newMaps = mapsData;
                data.map((s, idx) => {
                    if (s.homeFieldMapId) {
                        const newMapList = mapsData.map((t, _idx) => {
                            if (t.id !== s.homeFieldMapId) return t;
                            return { ...t, isdisabled: true }
                        });
                        newMaps = newMapList;
                    }
                });

                setMapsData(newMaps);
            } catch (e) {
                setErrorMessage(JSON.parse(e.response));
            }
            setLoading(false);
        };

        fetchData();
    };

    const selectTeamHomefield = async (mapId: number, teamId: number) => {
        try {
            let client = new TeamsClient();
            const command = new AssignTeamHomefieldCommand;
            command.mapId = mapId;
            command.teamId = teamId;
            const response = await client.assignHomeField(command);
            await updateTeams(null);
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
    const renderTeamList = () => {
        let select = null;
        if (teamsData && teamsData.length > 0) {
            select = (teamsData.map((team, idx) =>
                <div>
                    <h4>{team.teamName} ({team.teamAbbreviation})</h4>
                    <br />
                    <Select
                        options={mapsData}
                        onChange={e => selectTeamHomefield(e.id, team.id)}
                        isSearchable={true}
                        value={mapsData.find(o => o.id == team.homeFieldMapId) || null}
                        isOptionDisabled={(option) => option.isdisabled}
                        getOptionValue={value => value.id.toString()}
                        getOptionLabel={label => label.mapName}
                        isLoading={loading}
                    />
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
                    <h3 className='text-center'>Select Homefields</h3>
                    <p>Please select a season to select homefields for teams.</p>
                    <Label for="seasonSelect">Select a season</Label>
                    {renderSeasonList()}
                    <br />
                    <Button color="primary" size="lg" block disabled={!(season > 0) || loading} onClick={updateTeams}>Select Season</Button>
                    <br />
                    {renderTeamList()}
                    <br />
                </Col>
            </Row>
        </React.Fragment>
    );
};

export default SelectHomefieldMaps