import { Component, ViewEncapsulation } from '@angular/core';
import { AfterViewInit } from '@angular/core';
import { Helpers } from "@app/helpers/helpers";
import { Subscription, Subject } from "rxjs";
import { startWith, delay } from 'rxjs/operators';
import { User } from "@app/models/user";
import { Task } from "@app/models/task";
import { UserService } from "@app/services/user.service";
import { TaskService } from "@app/services/task.service";
import { TokenService } from "@app/services/token.service";



@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html',
  encapsulation: ViewEncapsulation.None,
  providers: [UserService, TaskService, TokenService]

})
export class AppComponent implements AfterViewInit {
  subscription: Subscription;

  authentication: boolean;

  user: User = new User();
  users: User[] = [];
  tasks: Task[] = [];
  selectedTask: Task;
  isTaskSelected: boolean = false;
  tableMode: boolean = true;
  format: string = "dd/MM/yyyy h:mma";

  constructor(private helpers: Helpers, private userService: UserService, private taskService: TaskService, private tokenService: TokenService) {

  }

  ngAfterViewInit() {

    this.subscription = this.helpers.isAuthenticationChanged().pipe(
      startWith(this.tokenService.isAuthenticated()),
      delay(0)).subscribe((value) =>
        this.authentication = value
    );
    this.taskService.isTaskListChanged$.subscribe((data) => {
        this.loadTasks();
      }
    );
    this.loadUsers();
    this.loadTasks();

  }

  title = 'Task board';

  ngOnDestroy() {

    this.subscription.unsubscribe();

  }

  loadUsers() {

    this.userService.getUsers()
      .subscribe((data: User[]) => this.users = data);
  }

  updateUsers(sort: number = null) {
    this.userService.getUsers(sort)
      .subscribe((data: User[]) => this.users = data);
  }


  loadTasks() {

    this.taskService.getTasks()
      .subscribe((data: Task[]) => {
        this.tasks = data;
        this.selectedTask = data[0];
        this.isTaskSelected = true;
      });
  }

  updateTasks(sort: number = null) {

    this.taskService.getTasks(sort)
      .subscribe((data: Task[]) => {
        this.tasks = data;
        this.selectedTask = data[0];
        this.isTaskSelected = true;
      });
  }

  selectUser(userId: string) {
    this.userService.getById(userId)
      .subscribe((data: User) => this.user = data);

    this.taskService.getTasksByUserId(userId).subscribe((data: Task[]) => {
      this.tasks = data;
      this.selectedTask = data[0];
      this.isTaskSelected = true;
    });
  }

  selectTask(id: string) {
    this.selectedTask = this.tasks.find(t => t.id === id);
    this.isTaskSelected = true;
  }

  deleteTask(id: string) {
    let task = this.tasks.find(t => t.id === id);
    this.taskService.deleteTask(task).subscribe((res: boolean) => {
      if (res) {
        this.loadTasks();
      }
    });
  }

  save() {
    if (this.user.id == null) {
      this.userService.createUser(this.user)
        .subscribe((data: User) => this.users.push(data));
    } else {
      this.userService.updateUser(this.user)
        .subscribe(data => this.loadUsers());
    }
    this.cancel();
  }
  editProduct(u: User) {
    this.user = u;
  }
  cancel() {
    this.user = new User();
    this.tableMode = true;
  }
  delete(u: User) {
    this.userService.deleteUser(u.id)
      .subscribe(data => this.loadUsers());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }
}
