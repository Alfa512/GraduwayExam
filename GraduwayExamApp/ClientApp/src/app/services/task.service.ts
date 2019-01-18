
import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { of } from 'rxjs';

import { catchError, map, tap } from 'rxjs/operators';

import { BaseService } from './base.service';

import { Task } from '../models/task';

import { AppConfig } from '../config/config';

import { Helpers } from '../helpers/helpers';

@Injectable()
export class TaskService extends BaseService {

  private pathAPI = this.config.setting['PathAPI'];

  constructor(private http: HttpClient, private config: AppConfig, helper: Helpers) { super(helper); }

  /** GET tasks from the server */

  getTasks(sort: number = null): Observable<Task[]> {
    let orderBy = sort === null ? "" : "?orderby=" + sort;
    return this.http.get<Task[]>(this.pathAPI + 'task/list' + orderBy, super.header()).pipe(
      catchError(super.handleError));

  }

  getTasksByUserId(userId: string): Observable<Task[]> {

    return this.http.get<Task[]>(this.pathAPI + 'task/byuserid?userId=' + userId, super.header()).pipe(
      catchError(super.handleError));

  }

  createTask(task: Task) {

    return this.http.post(this.pathAPI + 'task/create', task);

  }

  updateTask(task: Task) {
    const data = {
      //data: {
        Id: task.id,

        Title: task.title,
        Description: task.description,
        State: task.state,
        Priority: task.priority,
        UserId: task.userId,
        CreatorId: task.creatorId
      //}
    };
    //return this.http.get<Task>(this.pathAPI + 'task/update?task=' + '25', super.header()).pipe(catchError(super.handleError));
    return this.http.post(this.pathAPI + 'task/update', task, super.header()); //{ headers: new HttpHeaders({ 'Content-Type': 'multipart/form-data' }) }

  }

  deleteTask(taskId: string) {

    return this.http.post(this.pathAPI + 'task/delete', taskId);

  }
}
