import { TestBed, inject } from '@angular/core/testing';

import { CategoryserviceService } from './categoryservice.service';

describe('CategoryserviceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CategoryserviceService]
    });
  });

  it('should be created', inject([CategoryserviceService], (service: CategoryserviceService) => {
    expect(service).toBeTruthy();
  }));
});
