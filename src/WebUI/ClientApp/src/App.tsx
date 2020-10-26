import * as React from 'react';
import { Route, Router } from 'react-router';
import { createBrowserHistory } from 'history';
import Layout from './components/Layout';
import Home from './components/home/Home';
import FetchLeaderboardAllTimeStats from './components/LeaderboardAllTimeStats';
import './custom.css'

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
const history = createBrowserHistory({ basename: baseUrl });


export default () => (
    <Router history={history}>
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/leaderboard-all-time' component={FetchLeaderboardAllTimeStats} />
        </Layout>
    </Router>
);