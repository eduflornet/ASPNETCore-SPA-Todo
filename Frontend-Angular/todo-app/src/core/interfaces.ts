export interface ITodo {
  id: number;
  description: string;
  isDone: boolean;
}

export interface ITodoResponse {
  status: boolean;
  todo: ITodo;
}


