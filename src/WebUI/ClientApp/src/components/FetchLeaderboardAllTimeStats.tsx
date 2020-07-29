import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../store';
import * as LeaderboardAllTimeStatsStore from '../store/LeaderboardAllTime';
import { LeaderboardStatsMode, ILeaderboardStatsDto } from '../WorldDoomLeague';

// At runtime, Redux will merge together...
type LeaderboardAllTimeProps =
    LeaderboardAllTimeStatsStore.LeaderboardAllTimeState // ... state we've requested from the Redux store
    & typeof LeaderboardAllTimeStatsStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ mode: LeaderboardStatsMode }>; // ... plus incoming routing parameters


class FetchLeaderboardAllTimeStats extends React.PureComponent<LeaderboardAllTimeProps> {
  // This method is called when the component is first added to the document
  public componentDidMount() {
    this.ensureDataFetched();
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
    this.ensureDataFetched();
  }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">All Time Player Stats</h1>
        <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
        {this.renderModeSelect()}
        {this.renderStatsTables()}
      </React.Fragment>
    );
  }

    private ensureDataFetched() {
        this.props.requestLeaderboardsAllTime(this.props.mode || LeaderboardStatsMode.Total);
  }

    // create a new table for each stat.
    private renderStatsTables() {
        const leaderboards = this.props.leaderboards;
        var tableArray = [];
        if (leaderboards.playerLeaderboardStats !== undefined) {
            const leadersObject = leaderboards.playerLeaderboardStats;
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
            tableArray.push(this.props.isLoading && <span>Loading...</span>);
        }
        return (tableArray);
    }

    handleModeChange(e) {
        this.props.requestLeaderboardsAllTime(e.target.value);
    }

    private renderModeSelect() {
    return (
      <div className="d-flex justify-content-between">
            <div className="SetMode">
                <select value={this.props.mode} onChange={(e) => this.handleModeChange(e)}>
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
}

export default withRouter(connect(
  (state: ApplicationState) => state.leaderboardAllTime, // Selects which state properties are merged into the component's props
    LeaderboardAllTimeStatsStore.actionCreators // Selects which action creators are merged into the component's props
)(FetchLeaderboardAllTimeStats as any));
