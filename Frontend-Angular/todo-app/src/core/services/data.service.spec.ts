import { mockPendingTodos } from './../helpers.spec';
import { DataService } from './data.service';
import { environment } from 'src/environments/environment';

describe('DataService', () => {
  let service: DataService;
  const httpSpy = jasmine.createSpyObj('', ['get']);
  let baseUrl = environment.apiUrl;
  let baseTodoUrl = baseUrl + 'todo';

  beforeEach(() => {
    service = new DataService(httpSpy);
  });

  describe('getPending', () => {

    it('should return list of pendings Todos', (done) => {
      const mockResponse = {
        toPromise: () => {
          return Promise.resolve({ json: () => mockPendingTodos });
        }
      };

      httpSpy.get.and.returnValue(mockResponse);

      service.getPendingTodos()
      .then(data => {
        expect(httpSpy.get).toHaveBeenCalledWith(`${baseTodoUrl}/pending`);
        expect(data).toEqual(mockPendingTodos);
        done();
      });
  });
});

});
