import { Component, OnInit } from '@angular/core';
import { Category } from '../models/category';
import { CategoryService } from '../category.service';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-category-view',
  templateUrl: './category-view.component.html',
  styleUrls: ['./category-view.component.css']
})
export class CategoryViewComponent implements OnInit {

  category: Category = { name: '', id: 0 };

  constructor(private categoryService: CategoryService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.category.id = params['id'];
    });
    if (this.category.id && this.category.id != undefined && this.category.id > 0) {
      this.categoryService.getCategory(this.category.id)
        .subscribe(res => this.category = res
        );
    } else {
      this.router.navigate(['/categories']);
    }

  }

  edit() {
    this.router.navigate(['/category/' + this.category.id + '/edit']);
  }

  delete() {
    if (confirm('Are you sure?')) {
      this.categoryService.deleteCategory(this.category.id).subscribe(res =>
        this.router.navigate(['/categories'])
      );
    }
  }
}
