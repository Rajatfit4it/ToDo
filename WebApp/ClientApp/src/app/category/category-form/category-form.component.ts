import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { Router, ActivatedRoute, Params } from '@angular/router';


@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {

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
    } 
  }

  submit() {
    if (this.category.id > 0) {
      this.categoryService.updateCategory(this.category)
        .subscribe(res => {
          console.log(res);
          this.router.navigate(['/category/' + this.category.id]);
        });
    } else {
      this.categoryService.addCategory(this.category)
        .subscribe(res => {
          console.log(res);
          this.router.navigate(['/categories']);
        });
    }
  }

}
