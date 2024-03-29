import { Component, OnDestroy } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { HttpClientModule } from '@angular/common/http';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,HttpClientModule],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent implements OnDestroy {

  model:AddCategoryRequest;

  private addCategorySubscribtion?:Subscription

  constructor(private categoryService:CategoryService,private router:Router) {
    this.model= {name:'',urlHandle:''}
  }

  ngOnDestroy(): void {

  this.addCategorySubscribtion?.unsubscribe();
  }

  onFormSubmit() {
  
  this.addCategorySubscribtion=  this.categoryService.addCategory(this.model).subscribe({
      next:(response)=>{this.router.navigateByUrl('/admin/categories')},
      error:(error)=>{console.log("category add error:"+error);}
    })
  }
}
