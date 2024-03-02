import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {Task } from './task.model'

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  public tasks: Task[] = [];
  public newT = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getTasks();
  }

  getTasks() {
    this.http.get<Task[]>('/tasks').subscribe(
      (result) => {
        this.tasks = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
  addNew() {
    this.newT= true;
  }
  addTask(event:Task) {
    this.newT = false;
    this.http.post("/tasks", event).subscribe(() => this.getTasks());
    
  }
  editTask(event: Task) {
    this.http.put("/tasks/"+event.id, event).subscribe(() => this.getTasks());

  }
  title = 'angex.client';
}
