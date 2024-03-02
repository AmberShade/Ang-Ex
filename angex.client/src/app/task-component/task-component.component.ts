import { Input, Component, ElementRef, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task } from '../task.model'

@Component({
  selector: 'task-comp',
  templateUrl: './task-component.component.html',
  styleUrls: ['./task-component.component.css']
})
export class TaskComponentComponent {
  @Input({ required: true }) public task: Task;
  @Output() editTask = new EventEmitter<Task>();
  public temptask: Task;
  public editing = false;
  constructor(private http: HttpClient, private host: ElementRef<HTMLElement>) {
    this.task = new Task();
    this.temptask = this.task.clone();
}
  //add task as a parameter for this item
  public delClick() {
    this.http.delete("/tasks/" + this.task.id).subscribe(() => this.host.nativeElement.remove());
  }
  public beginEdit() {
    this.temptask = this.task.clone();
    this.editing = true;
  }
  public cancelEdit() {
    this.editing = false;
  }
  public saveChanges() {
    this.editTask.emit(this.temptask);
  }
}
