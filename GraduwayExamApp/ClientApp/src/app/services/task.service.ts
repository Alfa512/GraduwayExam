
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

  getTasks(): Observable<Task[]> {

    return this.http.get<Task[]>(this.pathAPI + 'task/list', super.header()).pipe(
      catchError(super.handleError));

  }

  createTask(task: Task) {

    return this.http.post(this.pathAPI + 'create', task);

  }

  updateTask(task: Task) {

    return this.http.post(this.pathAPI + 'update', task);

  }

  deleteTask(taskId: string) {

    return this.http.post(this.pathAPI + 'delete', taskId);

  }
}
