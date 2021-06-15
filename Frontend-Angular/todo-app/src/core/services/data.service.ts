import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ITodo, ITodoResponse } from '../interfaces';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl = environment.apiUrl;
  baseTodoUrl = this.baseUrl + 'todo';

  constructor(private http: HttpClient) { }

  public async getPendingTodos(): Promise<ITodo[]> {
    return this.http.get<ITodo[]>(`${this.baseTodoUrl}/pending`)
      .pipe(
        map(pendingTodos => {
          return pendingTodos;
        }),
        catchError(this.handleError)
      ).toPromise();
  }

  public async getCompletedTodos(): Promise<ITodo[]> {
    return this.http.get<ITodo[]>(`${this.baseTodoUrl}/completed`)
      .pipe(
        map(completedTodos => {
          return completedTodos;
        }),
        catchError(this.handleError)
      ).toPromise();
  }

  public async insertTodo(todo: ITodo): Promise<ITodo> {
    return this.http.post<ITodoResponse>(this.baseTodoUrl, todo)
      .pipe(
        map((data) => {
          return data.todo;
        }),
        catchError(this.handleError)
      ).toPromise();
  }

  public async updatedTodo(todo: ITodo): Promise<ITodo> {
    return this.http.put<ITodoResponse>(this.baseTodoUrl, todo)
      .pipe(
        map((data) => {
          console.log('updatedTodo status: ' + data.status);
          return data.todo;
        }),
        catchError(this.handleError)
      ).toPromise();
  }



  private handleError(error: HttpErrorResponse) {
    console.error('server error:', error);
    if (error.error instanceof Error) {
      let errMessage = error.error.message;
      return Observable.throw(errMessage);
    }
    return Observable.throw(error || 'ASP.NET Core server error');
  }


}
