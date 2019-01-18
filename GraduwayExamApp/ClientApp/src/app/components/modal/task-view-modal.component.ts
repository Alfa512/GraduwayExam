import { Component, Input } from '@angular/core';
import { Task } from "@app/models/task";
import { User } from "@app/models/user";
import { TaskService } from "@app/services/task.service";
import { UserService } from "@app/services/user.service";


@Component({
  selector: 'app-task-view-modal',
  templateUrl: './task-view-modal.component.html',
  providers: [TaskService, UserService]

})
export class TaskViewModalComponent {
  _task: Task;
  editDesc: boolean = false;
  newdesc: string;
  taskPriority: number;
  taskState: number;
  assignee: User = new User();
  creator: User = new User();

  constructor(private taskService: TaskService, private userService: UserService) {

  }

  get task(): Task {
    return this._task;
  }

  @Input()
  set task(task: Task) {
    this._task = task;
    if (this.task) {
      this.newdesc = this.task.description;
      this.taskState = this.task.state;
      this.taskPriority = this.task.priority;
      this.userService.getById(this.task.creatorId).subscribe((data: User) => this.creator = data);
      this.userService.getById(this.task.userId).subscribe((data: User) => this.assignee = data);
    }
  }

  saveTaskDescription() {
    this.task.description = this.newdesc;
    this.editDesc = false;
    this.taskService.updateTask(this.task).subscribe((data: Task) => this.task = data);
  }

  saveTaskChanges() {
    this.task.state = this.taskState;
    this.task.priority = this.taskPriority;
    this.task.description = this.newdesc;
    this.editDesc = false;
    this.taskService.updateTask(this.task).subscribe((data: Task) => this.task = data);
  }
}
