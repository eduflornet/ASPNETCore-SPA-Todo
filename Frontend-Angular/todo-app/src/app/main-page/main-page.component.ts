import { ITodo } from './../../core/models/todo.model';
import { DataService } from './../../core/services/data.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  pendingTodos: ITodo[] = [];
  completedTodos: ITodo[] = [];

  constructor(private dataService: DataService) { }

  ngOnInit(): void {

    this.dataService.getPendingTodos().then((todos: ITodo[]) => {
      this.pendingTodos = todos;
    },
      (err: any) => console.log(err)
    );

    this.dataService.getCompletedTodos().then((todos: ITodo[]) => {
      this.completedTodos = todos;
    },
      (err: any) => console.log(err)
    );

  }

}
