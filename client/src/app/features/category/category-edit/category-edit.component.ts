import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UpdateCategoryModel } from '../models/update-category-model';

@Component({
  selector: 'app-category-edit',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,FormsModule],
  templateUrl: './category-edit.component.html',
  styleUrl: './category-edit.component.css'
})
export class CategoryEditComponent implements OnInit,OnDestroy {

id:string | null=null;
paramsSubscription?:Subscription;
category?:Category;

  constructor(private route:ActivatedRoute,private categoryService:CategoryService) {

  }
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }
  ngOnInit(): void {
  
   this.paramsSubscription= this.route.paramMap.subscribe({
    next:(params)=>{

     this.id= params.get('id');
     if(this.id)
     {
      //get the data from the API for this cateogoryID
      this.categoryService.getCategoryById(this.id).subscribe({
        next:(response)=>{
          this.category=response;
        }
      })
     }
    }
    });

  }

    onFormSubmit():void
    {
     const updateCategoryRequest:UpdateCategoryModel= {
      name:this.category?.name??'',
      urlHandle:this.category?.urlHandle??''
     };

    }
}
