import * as React from 'react';
import { Route, Router } from 'react-router';
import { createBrowserHistory } from 'history';
import Layout from './components/Layout';
import Home from './components/home/Home';
import FetchLeaderboardAllTimeStats from './components/LeaderboardAllTimeStats';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import AdminRoutes from './components/admin/AdminRoutes';
import Roles from './components/api-authorization/Roles';
import './custom.css';
import UpcomingMatches from './components/matches/UpcomingMatches';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
const history = createBrowserHistory({ basename: baseUrl });


export default () => (
    <Router history={history}>
        <Layout>
            <ApiAuthorizationRoutes />
            <AdminRoutes />
            <Route exact path='/' component={Home} />
            <Route exact path='/matches' component={UpcomingMatches} />
            <Route path='/leaderboard-all-time' component={FetchLeaderboardAllTimeStats} />
        </Layout>
    </Router>
);