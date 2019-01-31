
import { Injectable, Input } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Subject } from 'rxjs';

import { catchError, map, tap } from 'rxjs/operators';

import { BaseService } from './base.service';

import { Task } from '../models/task';

import { AppConfig } from '../config/config';

import { Helpers } from '../helpers/helpers';

@Injectable()
export class TaskService extends BaseService {

  private pathAPI = this.config.setting['PathAPI'];
  public taskListChanged = new Subject<boolean>();
  private _taskListChanged: boolean = false;


  constructor(private http: HttpClient, private config: AppConfig, helper: Helpers) { super(helper); }

  isTaskListChanged$ = this.taskListChanged.asObservable();
  

  toggleTaskListChanged() {
    this._taskListChanged = !this._taskListChanged;
    this.taskListChanged.next(this._taskListChanged);
  }

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

    return this.http.post(this.pathAPI + 'task/create', task, super.header());
  }

  updateTask(task: Task) {

    return this.http.post(this.pathAPI + 'task/update', task, super.header());
  }

  deleteTask(task: Task) {

    return this.http.post(this.pathAPI + 'task/delete', task, super.header());
    //return this.http.post(this.pathAPI + 'task/delete', { 'taskId': id }, super.header());

  }
}
