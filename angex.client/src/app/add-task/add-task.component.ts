import { Component, Output, EventEmitter } from '@angular/core';
import { Task } from '../task.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent {
  @Output() createdElement = new EventEmitter<Task>();
  public newItem = new Task();
  create() {
    this.createdElement.emit(this.newItem);
  }
}
