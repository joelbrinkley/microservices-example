import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
//ng prime
import { InputTextModule, ButtonModule, ListboxModule } from 'primeng/primeng';

//routes
import { routing } from './app.routing';

//application modules
import { SettingsProvider } from './configuration/settings.provider';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, FormsModule, InputTextModule, ButtonModule, ListboxModule, routing
  ],
  providers: [
    SettingsProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function init(settingsProvider: SettingsProvider) {
  return () => settingsProvider.loadConfig();
}
