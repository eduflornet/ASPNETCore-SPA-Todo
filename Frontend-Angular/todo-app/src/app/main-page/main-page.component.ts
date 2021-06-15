import { DataService } from './../../core/services/data.service';
import { Component, OnInit } from '@angular/core';
import { ITodo } from 'src/core/interfaces';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  pendingTodos: ITodo[] = [];
  completedTodos: ITodo[] = [];
  
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };

  todoForm = this.fb.group({
    description: ['', Validators.required]
  });

  todo: ITodo = {
    description: '',
    isDone: false,
    id: 0
  };

  constructor(private dataService: DataService,
    private fb: FormBuilder) {

    this.validationMessages = {
      description: {
        required: 'Description is required.',
        minlength: 'Description must be at least three characters.',
        maxlength: 'Description cannot exceed 50 characters.'
      }
    };
  }

  ngOnInit(): void {

  }



  onAdd(): void {
    if (this.todoForm.valid) {
      this.todo.description = this.todoForm.get('description')?.value;
      this.dataService.insertTodo(this.todo)
        .then((todo: ITodo) => {
          if (todo) {
            this.getPendingTodos();
            this.todoForm.reset();
          }
        },
          (err: any) => console.log(err));
    }

  }

  private getPendingTodos(): void {

    this.dataService.getPendingTodos().then((todos: ITodo[]) => {
      this.pendingTodos = todos;
    },
      (err: any) => console.log(err)
    );

  }

  private getCompletedTodos(): void {

    this.dataService.getCompletedTodos().then((todos: ITodo[]) => {
      this.completedTodos = todos;
    },
      (err: any) => console.log(err)
    );

  }

}
