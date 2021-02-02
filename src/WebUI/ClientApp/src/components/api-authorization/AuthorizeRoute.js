import React from 'react'
import { Component } from 'react'
import { Route, Redirect } from 'react-router-dom'
import { ApplicationPaths, QueryParameterNames } from './ApiAuthorizationConstants'
import authService from './AuthorizeService'

export default class AuthorizeRoute extends Component {
    constructor(props) {
        super(props);

        this.state = {
            ready: false,
            authenticated: false,
            roles: []
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.authenticationChanged());
        this.populateAuthenticationState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    render() {
        const { ready, authenticated, roles } = this.state;
        var link = document.createElement("a");
        link.href = this.props.path;
        const returnUrl = `${link.protocol}//${link.host}${link.pathname}${link.search}${link.hash}`;
        const redirectUrl = `${ApplicationPaths.Login}?${QueryParameterNames.ReturnUrl}=${encodeURIComponent(returnUrl)}`
        if (!ready) {
            return <div></div>;
        } else {
            const { component: Component, componmentroles, ...rest } = this.props;
            return <Route {...rest}
                render={(props) => {
                    if (authenticated && this.containsDuplicate(this.componentroles, roles)) {
                        return <Component {...props} />
                    } else {
                        return <Redirect to={redirectUrl} />
                    }
                }} />
        }
    }

    async populateAuthenticationState() {
        const [authenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()]);
        this.setState({ ready: true, authenticated, roles: user && user.role });
    }

    async authenticationChanged() {
        this.setState({ ready: false, authenticated: false, roles: [] });
        await this.populateAuthenticationState();
    }

    containsDuplicate(componentroles, userroles) {
        // only for authed pages that aren't 
        if (componentroles === undefined) {
            return true;
        }

        for (var i = 0; i < componentroles.length; i++) {
            for (var j = 0; j < userroles.length; j++) {
                if (userroles[j] === componentroles[i]) {
                    return true;
                }
            }
        }
        return false;
    }
}
