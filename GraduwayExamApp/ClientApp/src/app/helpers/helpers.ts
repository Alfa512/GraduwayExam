
import { Injectable } from '@angular/core';

import { Observable, Subject } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';


@Injectable()

export class Helpers {

  private authenticationChanged = new Subject<boolean>();

  constructor(private cookieService: CookieService) {



  }

  public isAuthenticated(): boolean {

    /*return (!(window.localStorage['token'] === undefined ||

      window.localStorage['token'] === null ||

      window.localStorage['token'] === 'null' ||

      window.localStorage['token'] === 'undefined' ||

      window.localStorage['token'] === ''));*/
    return (!this.isTokenNullOrEmpty());

  }

  public isAuthenticationChanged(): any {

    return this.authenticationChanged.asObservable();

  }

  public getToken(): any {

    /*if (window.localStorage['token'] === undefined ||

      window.localStorage['token'] === null ||

      window.localStorage['token'] === 'null' ||

      window.localStorage['token'] === 'undefined' ||

      window.localStorage['token'] === '') {

      return '';

    }*/
    if (this.isTokenNullOrEmpty()) {

      return '';
    }

    let obj = JSON.parse(this.cookieService.get('token'));
    //let obj = JSON.parse(window.localStorage['token']);

    return obj.access_token;

  }

  private isTokenNullOrEmpty(): boolean {
    let token = this.cookieService.get('token');
    return (token === undefined || token === null ||
      token === 'null' || token === 'undefined' || token === '');
  }

  public setToken(data: any): void {

    this.cookieService.set('token', JSON.stringify(data));
    //this.setStorageToken(JSON.stringify(data));
    this.authenticationChanged.next(this.isAuthenticated());
  }

  public failToken(): void {

    this.setStorageToken(undefined);

  }

  public logout(): void {

    this.setStorageToken(undefined);

  }

  private setStorageToken(value: any): void {

    //window.localStorage['token'] = value;
    this.cookieService.set('token', value);

    this.authenticationChanged.next(this.isAuthenticated());

  }

}
