import { Component, Input } from '@angular/core';
import { Task } from "@app/models/task";
import { User } from "@app/models/user";
import { TaskService } from "@app/services/task.service";
import { UserService } from "@app/services/user.service";

@Component({
  selector: 'app-add-task-modal',
  templateUrl: './add-task-modal.component.html',
  providers: [UserService]

})
export class AddTaskModalComponent {
  _task: Task = new Task();
  title: string;
  description: string;
  taskPriority: number;
  taskState: number;
  assigneeId: string;
  assignee: User = new User();
  creator: User = new User();
  usersList: User[] = [];

  constructor(private taskService: TaskService, private userService: UserService) {
    this.getUsers();
  }

  get task(): Task {
    return this._task;
  }

  getUsers() {
    this.userService.getUsers().subscribe((data: User[]) => this.usersList = data);
  }

  @Input()
  set task(task: Task) {
    this._task = task;
  }
  
  newTask() {
    if (this.task == null)
      this.task = new Task();
    this.task.title = this.title;
    this.task.title = this.title;
    this.task.state = this.taskState;
    this.task.priority = this.taskPriority;
    this.task.description = this.description;
    this.task.userId = this.assigneeId;
    this.taskService.createTask(this.task).subscribe((data: Task) => {
      this.taskService.toggleTaskListChanged();
      this.clearForm();
      this.close();
    });
  }

  clearForm() {
    this.task = new Task();
  }

  close() {
    let modalCloseButton: HTMLElement = document.getElementById('add-task-modal-close') as HTMLElement;
    modalCloseButton.click();
  }
}
