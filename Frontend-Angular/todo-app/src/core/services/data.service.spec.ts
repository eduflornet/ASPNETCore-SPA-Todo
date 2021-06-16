import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { DataService } from './data.service';
import { mockCompletedTodos, mockPendingTodos, mockTodo } from '../helpers.spec';
import { environment } from 'src/environments/environment';
import { HttpResponse } from '@angular/common/http';

describe('DataService', () => {
  let httpTestingController: HttpTestingController;
  let service: DataService;
  let baseUrl = environment.apiUrl;
  let baseTodoUrl = baseUrl + 'todo';

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DataService],
      imports: [HttpClientTestingModule]
    });

    httpTestingController = TestBed.get(HttpTestingController);
    service = TestBed.inject(DataService);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return list of pendings Todos', async (done: DoneFn) => {
    service.getPendingTodos().then(response => {
      expect(response).toBe(mockPendingTodos);
      done();
    });

    const req = httpTestingController.expectOne(`${baseTodoUrl}/pending`);
    expect(req.request.method).toEqual('GET');

    req.flush(mockPendingTodos);
  });

  it('should return list of completed Todos', async (done: DoneFn) => {
    service.getCompletedTodos().then(response => {
      expect(response).toBe(mockCompletedTodos);
      done();
    });

    const req = httpTestingController.expectOne(`${baseTodoUrl}/completed`);
    expect(req.request.method).toEqual('GET');

    req.flush(mockCompletedTodos);
  });

    it('should insert or add an todo object and return it', () => {

      service.insertTodo(mockTodo).then(
        data => expect(data).toEqual(mockTodo, 'should return the mockTodo'),
        fail
      );

      const req = httpTestingController.expectOne(baseTodoUrl);
      expect(req.request.method).toEqual('POST');
      expect(req.request.body).toEqual(mockTodo);

      const expectedResponse = new HttpResponse({ status: 201, statusText: 'Created', body: mockTodo });
      req.event(expectedResponse);
    });

    it('should update an todo object and return it', () => {

      service.updatedTodo(mockTodo).then(
        data => expect(data).toEqual(mockTodo, 'should return the mockTodo'),
        fail
      );

      const req = httpTestingController.expectOne(baseTodoUrl);
      expect(req.request.method).toEqual('PUT');
      expect(req.request.body).toEqual(mockTodo);

      const expectedResponse = new HttpResponse({ status: 201, statusText: 'Updated', body: mockTodo });
      req.event(expectedResponse);
    });


});
