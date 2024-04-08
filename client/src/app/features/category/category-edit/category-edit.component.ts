import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UpdateCategoryModel } from '../models/update-category-model';


@Component({
  selector: 'app-category-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './category-edit.component.html',
  styleUrl: './category-edit.component.css'
})
export class CategoryEditComponent implements OnInit, OnDestroy {

  id: string | null = null;
  paramsSubscription?: Subscription;
  editCategorySubscription?: Subscription
  category?: Category;

  constructor(private route: ActivatedRoute, private categoryService: CategoryService,
    private router: Router) {

  }
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }
  ngOnInit(): void {

    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {

        this.id = params.get('id');
        if (this.id) {
          //get the data from the API for this cateogoryID
          this.categoryService.getCategoryById(this.id).subscribe({
            next: (response) => {
              this.category = response;
            }
          })
        }
      }
    });

  }

  onFormSubmit(): void {
    const updateCategoryRequest: UpdateCategoryModel = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    if (this.id) {
      this.editCategorySubscription = this.categoryService.updateCategory(this.id, updateCategoryRequest).subscribe(
        {
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          }
        }
      )
    }

  }

  onDelete(): void {

    if (this.id)
  
      this.categoryService.deleteCategory(this.id).subscribe({
        next: (response) => {
 
          this.router.navigateByUrl('/admin/categories');
        }
      });
  }
}
