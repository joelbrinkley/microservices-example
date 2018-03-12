import { Injectable, Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';

import { OidcSecurityService, OpenIDImplicitFlowConfiguration, AuthWellKnownEndpoints } from 'angular-auth-oidc-client';
import { SettingsProvider } from '../configuration/settings.provider';

@Injectable()
export class AuthService implements OnInit, OnDestroy {
    isAuthorized: boolean = false;
    isAuthorized$ = new Subject();

    constructor(public oidcSecurityService: OidcSecurityService, private http: HttpClient, settingsProvider: SettingsProvider) {
        this.isAuthorized$.subscribe(isAuthorized => {
            console.log('setting is authorized');
            this.isAuthorized = isAuthorized as boolean;
        });
        const openIdImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();
        openIdImplicitFlowConfiguration.stsServer = settingsProvider.configuration.stsServer;
        openIdImplicitFlowConfiguration.redirect_url = settingsProvider.configuration.redirect_url;
        openIdImplicitFlowConfiguration.client_id = settingsProvider.configuration.client_id;
        openIdImplicitFlowConfiguration.response_type = settingsProvider.configuration.response_type;
        openIdImplicitFlowConfiguration.scope = settingsProvider.configuration.scope;
        openIdImplicitFlowConfiguration.post_logout_redirect_uri = settingsProvider.configuration.baseUrl;
        openIdImplicitFlowConfiguration.forbidden_route = '/forbidden';
        openIdImplicitFlowConfiguration.unauthorized_route = '/unauthorized';
        openIdImplicitFlowConfiguration.auto_userinfo = true;
        openIdImplicitFlowConfiguration.log_console_warning_active = true;
        openIdImplicitFlowConfiguration.log_console_debug_active = true;
        openIdImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds = 10;

        const authWellKnownEndpoints = new AuthWellKnownEndpoints();
        authWellKnownEndpoints.setWellKnownEndpoints(settingsProvider.configuration);

        console.log(openIdImplicitFlowConfiguration);
        this.oidcSecurityService.setupModule(openIdImplicitFlowConfiguration, authWellKnownEndpoints);

    }

    ngOnInit() {

    }

    ngOnDestroy(): void {
        this.oidcSecurityService.onModuleSetup.unsubscribe();
        this.isAuthorized$.unsubscribe();
    }

    getIsAuthorized(): Observable<boolean> {
        return this.oidcSecurityService.getIsAuthorized();
    }

    login() {
        console.log('start login');
        this.oidcSecurityService.authorize();
    }

    refreshSession() {
        console.log('start refreshSession');
        this.oidcSecurityService.authorize();
    }

    logout() {
        console.log('start logoff');
        this.oidcSecurityService.logoff();
    }

    authorizedCallback() {
        this.oidcSecurityService.authorizedCallback();
    }
}