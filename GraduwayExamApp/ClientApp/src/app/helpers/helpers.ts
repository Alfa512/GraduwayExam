
import { Injectable } from '@angular/core';

import { Observable, Subject } from 'rxjs';


@Injectable()

export class Helpers {

  private authenticationChanged = new Subject<boolean>();

  constructor() {



  }

  public isAuthenticated(): boolean {

    return (!(window.sessionStorage['token'] === undefined ||

      window.sessionStorage['token'] === null ||

      window.sessionStorage['token'] === 'null' ||

      window.sessionStorage['token'] === 'undefined' ||

      window.sessionStorage['token'] === ''));

  }

  public isAuthenticationChanged(): any {

    return this.authenticationChanged.asObservable();

  }

  public getToken(): any {

    if (window.sessionStorage['token'] === undefined ||

      window.sessionStorage['token'] === null ||

      window.sessionStorage['token'] === 'null' ||

      window.sessionStorage['token'] === 'undefined' ||

      window.sessionStorage['token'] === '') {

      return '';

    }

    let obj = JSON.parse(window.sessionStorage['token']);

    return obj.token;

  }

  public setToken(data: any): void {

    this.setStorageToken(JSON.stringify(data));

  }

  public failToken(): void {

    this.setStorageToken(undefined);

  }

  public logout(): void {

    this.setStorageToken(undefined);

  }

  private setStorageToken(value: any): void {

    window.sessionStorage['token'] = value;

    this.authenticationChanged.next(this.isAuthenticated());

  }

}
