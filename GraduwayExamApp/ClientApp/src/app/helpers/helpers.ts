
import { Injectable } from '@angular/core';

import { Observable, Subject } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';


@Injectable()

export class Helpers {

  private authenticationChanged = new Subject<boolean>();

  constructor(private cookieService: CookieService) {}

  public isAuthenticationChanged(): any {

    return this.authenticationChanged.asObservable();
  }

  public toggleAuthentication(data: boolean) {

    this.authenticationChanged.next(data);
  }

  public getToken(): any {

    if (this.isTokenNullOrEmpty()) {

      return '';
    }

    let obj = JSON.parse(this.cookieService.get('token'));
    return obj.access_token;
  }

  public isTokenNullOrEmpty(): boolean {
    let token = this.cookieService.get('token');
    return (token === undefined || token === null ||
      token === 'null' || token === 'undefined' || token === '');
  }

  public setToken(data: any): void {

    this.cookieService.set('token', JSON.stringify(data));
    this.toggleAuthentication(!this.isTokenNullOrEmpty());
  }

  public failToken(): void {

    this.setToken(undefined);
  }

  public logout(): void {

    this.setToken(undefined);
  }
}
