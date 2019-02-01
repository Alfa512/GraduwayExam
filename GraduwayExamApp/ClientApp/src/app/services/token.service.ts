
import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';
import { Token } from '../models/token';
import { Helpers } from '../helpers/helpers';
import { User } from '../models/user';

@Injectable()

export class TokenService extends BaseService {


  public errorMessage: string;

  constructor(http: HttpClient, helper: Helpers) { super(http, helper); }

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
