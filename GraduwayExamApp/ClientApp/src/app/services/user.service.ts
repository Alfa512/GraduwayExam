
import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { of } from 'rxjs';

import { catchError, map, tap } from 'rxjs/operators';

import { BaseService } from './base.service';

import { User } from '../models/user';

import { AppConfig } from '../config/config';

import { Helpers } from '../helpers/helpers';

@Injectable()
export class UserService extends BaseService {

  private pathAPI = this.config.setting['PathAPI'];

  constructor(private http: HttpClient, private config: AppConfig, helper: Helpers) { super(helper); }

  /** GET users from the server */

  getUsers(): Observable<User[]> {

    return this.http.get<User[]>(this.pathAPI + 'user/list', super.header()).pipe(
      catchError(super.handleError));
  }

  createUser(user: User) {

    return this.http.post(this.pathAPI + 'create', user);

  }

  updateUser(user: User) {

    return this.http.post(this.pathAPI + 'update', user);

  }

  deleteUser(userId: string) {

    return this.http.get<User[]>(this.pathAPI + 'create', super.header()).pipe(catchError(super.handleError));

  }
}
