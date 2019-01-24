
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

  getUsers(sort: number = null): Observable<User[]> {
    let orderBy = sort === null ? "" : "?orderby=" + sort;
    return this.http.get<User[]>(this.pathAPI + 'user/list' + orderBy, super.header()).pipe(
      catchError(super.handleError));
  }

  getById(id: string): Observable<User> {
    return this.http.get<User>(this.pathAPI + 'user/getbyid?id=' + id, super.header()).pipe(
      catchError(super.handleError));
  }

  createUser(user: User): Observable<User> {

    return this.http.post<User>(this.pathAPI + 'user/create', user).pipe(
      catchError(super.handleError));

  }

  updateUser(user: User) {

    return this.http.post(this.pathAPI + 'user/update', user);

  }

  deleteUser(userId: string) {

    return this.http.get<User[]>(this.pathAPI + 'user/delete', super.header()).pipe(catchError(super.handleError));

  }
}
