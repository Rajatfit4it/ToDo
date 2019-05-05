import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { Category } from '../models/category';

@Injectable()
export class CategoryService {

  constructor(private http: HttpClient) { }

  GetCategories() {
    return this.http.get<Category>('/api/category/');

  }

  addCategory(category) {
    return this.http.post('/api/category/', category);
  }

  getCategory(id) {
    return this.http.get<Category>('/api/category/' + id);
  }

  updateCategory(category: Category) {
    return this.http.put('/api/category/' + category.id, category);
  }

  deleteCategory(id) {
    return this.http.delete('api/category/' + id);
  }

}
