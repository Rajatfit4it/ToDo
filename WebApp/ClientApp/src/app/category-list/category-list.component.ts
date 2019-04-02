import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../category.service';
import { Category } from '../models/category';


@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  constructor(private categoryService: CategoryService) { }
  categories: Category[];

  ngOnInit() {
    this.categoryService.GetCategories().subscribe(res => {
      this.categories = res;
    });
  }

}
