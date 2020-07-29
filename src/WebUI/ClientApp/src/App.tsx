import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import FetchLeaderboardAllTimeStats from './components/FetchLeaderboardAllTimeStats';
import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/leaderboard-all-time' component={FetchLeaderboardAllTimeStats} />
    </Layout>
);
