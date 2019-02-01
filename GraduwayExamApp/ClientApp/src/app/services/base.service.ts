import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { of } from 'rxjs';

import { catchError, map, tap } from 'rxjs/operators';

import { Helpers } from "@app/helpers/helpers";

import { AppConfig } from '../config/config';

import {User} from "@app/models/user";

@Injectable()

export class BaseService {

  public config: AppConfig = new AppConfig();

  public  pathAPI = this.config.setting['PathAPI'];

  constructor(protected http: HttpClient, private helper: Helpers) { }


  public extractData(res: Response) {

    let body = res.json();

    return body || {};

  }



  public handleError(error: Response | any) {

    let errMsg: string;

    if (error instanceof Response) {

      const body = error.json() || '';

      const err = body || JSON.stringify(body);

      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;

    } else {

      errMsg = error.message ? error.message : error.toString();
    }

    console.error(errMsg);

    return Observable.throw(errMsg);
  }


  public header() {
    let header = new HttpHeaders({ 'Content-Type': 'application/json', 'dataType': "json" });

    header = header.append('Authorization', "Bearer " + this.helper.getToken());
    return { headers: header };
  }

  public isAuthenticated(): boolean {
    this.checkAuthentication();
    return (!this.helper.isTokenNullOrEmpty());
  }

  private checkAuthentication() {

    this.updateToken().subscribe((data: any) => {
      this.helper.setToken(data);
    });
  }

  public setToken(data: any) {

    this.helper.setToken(data);
  }

  private updateToken(body: User = null): Observable<any> {
    return this.http.post<any>(this.pathAPI + 'token/validate', body, this.header()).pipe(
      catchError(this.handleError)
    );
  }

  public logout() {

    this.helper.logout();
  }

  public failToken(error: Response | any) {

    this.helper.failToken();
    return this.handleError(Response);
  }

}
