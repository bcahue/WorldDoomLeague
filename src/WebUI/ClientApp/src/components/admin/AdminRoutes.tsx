import * as React from 'react';
import AuthorizeRoute from '../api-authorization/AuthorizeRoute';
import Roles from '../api-authorization/Roles'
import NewSeasonWizard from './newseasonwizard/NewSeasonWizard';

export default () => (
    <AuthorizeRoute exact path='/admin/newseasonwizard' component={NewSeasonWizard} componmentroles={[Roles.Admin]} />
    );