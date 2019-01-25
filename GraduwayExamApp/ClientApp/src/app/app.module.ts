import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Components

import { LoginComponent, LogoutComponent, DashboardComponent } from '@app/components';
import { HeaderComponent, FooterComponent } from '@app/components';
import { TaskViewModalComponent } from '@app/components/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Helpers } from './helpers/helpers';
import { TokenService } from '@app/services/token.service';
import { UserService } from '@app/services/user.service';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppConfig } from '@app/config/config';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    LogoutComponent,
    DashboardComponent,
    TaskViewModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [{ provide: APP_BASE_HREF, useValue: '' },
    Helpers,
    UserService,
    TokenService,
    HttpClientModule,
    HttpClient,
    AppConfig
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
