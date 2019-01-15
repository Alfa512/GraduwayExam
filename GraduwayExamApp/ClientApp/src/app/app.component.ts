import { Component } from '@angular/core';
import { AfterViewInit } from '@angular/core';
import { Helpers } from "@app/helpers/helpers";
import { Subscription } from "rxjs";
import { startWith, delay } from 'rxjs/operators';
import { User } from "@app/models/user";
import { Task } from "@app/models/task";
import { UserService } from "@app/services/user.service";
import { TaskService } from "@app/services/task.service";

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html',
  providers: [UserService, TaskService]

})
export class AppComponent implements AfterViewInit {
  subscription: Subscription;

  authentication: boolean;

  user: User = new User();
  users: User[] = [];
  tasks: Task[] = [];
  tableMode: boolean = true;

  constructor(private helpers: Helpers, private userService: UserService, private taskService: TaskService) {

  }

  ngAfterViewInit() {

    this.subscription = this.helpers.isAuthenticationChanged().pipe(
      startWith(this.helpers.isAuthenticated()),
      delay(0)).subscribe((value) =>
        this.authentication = value
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

  loadTasks() {

    this.taskService.getTasks()
      .subscribe((data: Task[]) => this.tasks = data);
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
