import * as React from 'react';
import AuthorizeRoute from '../api-authorization/AuthorizeRoute';
import Roles from '../api-authorization/Roles'
import NewSeasonWizard from './newseasonwizard/NewSeasonWizard';
import ProcessGameWizard from './processgamewizard/ProcessGameWizard';

export default () => (
    <React.Fragment>
        <AuthorizeRoute exact path='/admin/newseasonwizard' component={NewSeasonWizard} componentroles={[Roles.Admin]} />
        <AuthorizeRoute exact path='/admin/processgamewizard' component={ProcessGameWizard} componentroles={[Roles.Admin, Roles.StatsRunner]} />
    </React.Fragment>
);