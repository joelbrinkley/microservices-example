import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
//ng prime
import { InputTextModule, ButtonModule, ListboxModule } from 'primeng/primeng';
import { AuthModule, OidcSecurityService, OpenIDImplicitFlowConfiguration, OidcConfigService, AuthWellKnownEndpoints } from 'angular-auth-oidc-client';
//routes
import { routing } from './app.routing';

//application modules
import { ManageAccountsViewComponent, ViewAllAccountsComponent, AccountService } from './accounts/index';

import { SettingsProvider } from './configuration/settings.provider';

import {AuthService} from './services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    ManageAccountsViewComponent,
    ViewAllAccountsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    AuthModule.forRoot(),
    InputTextModule,
    ButtonModule,
    ListboxModule,
    routing
  ],
  providers: [
    OidcSecurityService,
    OidcConfigService,
    AuthService,
    SettingsProvider,
    {
      provide: APP_INITIALIZER,
      useFactory: init,
      deps: [SettingsProvider, OidcConfigService],
      multi: true
    },
    AccountService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}

export function init(settingsProvider: SettingsProvider) {
  return () => settingsProvider.loadConfig();
}
