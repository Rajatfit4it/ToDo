import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class CategoryService {

  constructor(private http: Http) { }

  GetCategories() {
    return this.http.get('/api/category')
      .map(res => res.json());

  }

}
