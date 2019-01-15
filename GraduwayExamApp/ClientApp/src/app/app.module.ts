import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';

//import { environment } from '../environments/environment';


// Components

import { LoginComponent, LogoutComponent, DashboardComponent } from '@app/components';
import { HeaderComponent, FooterComponent } from '@app/components';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Helpers } from './helpers/helpers';
import { TokenService } from '@app/services/token.service';
import { HttpClientModule, HttpClient, HttpHandler } from '@angular/common/http';
import { AppConfig } from '@app/config/config';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    LogoutComponent,
    DashboardComponent,
   //Helpers
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
    //Helpers
  ],
  providers: [{ provide: APP_BASE_HREF, useValue: '' },
    Helpers,
    TokenService,
    HttpClientModule,
    HttpClient,
    AppConfig
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
