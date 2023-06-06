import { TestBed } from '@angular/core/testing';

import { FetchThingsService } from './fetch-things.service';

describe('FetchThingsService', () => {
  let service: FetchThingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FetchThingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
