
import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpClientModule } from '@angular/common/http';

import { Observable } from 'rxjs';

import { of } from 'rxjs';

import { catchError, map, tap } from 'rxjs/operators';

import { AppConfig } from '../config/config';

import { BaseService } from './base.service';

import { Token } from '../models/token';

import { Helpers } from '../helpers/helpers';

import { User } from '../models/user';

@Injectable()

export class TokenService extends BaseService {

  private pathAPI = this.config.setting['PathAPI'];

  public errorMessage: string;

  constructor(private http: HttpClient, private config: AppConfig, helper: Helpers) { super(helper); }

  auth(data: User): any {

    return this.getToken(data);
  }

  logout(): any {

    super.logout();

  }

  private getToken(body: User): Observable<any> {

    return this.http.post<any>(this.pathAPI + 'token/token', body).pipe(

      catchError(super.handleError)
    );

  }

}
