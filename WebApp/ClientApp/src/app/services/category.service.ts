import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { Category } from '../models/category';

@Injectable()
export class CategoryService {

  constructor(private http: Http) { }

  GetCategories() {
    return this.http.get('/api/category/')
      .map(res => res.json());

  }

  addCategory(category) {
    return this.http.post('/api/category/', category);
    //.map(res => res.json());
  }

  getCategory(id) {
    return this.http.get('/api/category/' + id)
      .map(res => res.json());
  }

  updateCategory(category: Category) {
    return this.http.put('/api/category/' + category.id, category);
  }

  deleteCategory(id) {
    return this.http.delete('api/category/' + id);
  }

}
