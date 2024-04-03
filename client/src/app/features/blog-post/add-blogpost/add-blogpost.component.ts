import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { Category } from '../../category/models/category';


@Component({
  selector: 'app-add-blogpost',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule],
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent implements OnInit {
model:AddBlogPost;
categories$?:Observable<Category []>;

  constructor(private blogPostService:BlogPostService,private router:Router,private categoryService:CategoryService) {
    this.model = {
      title: '',
      shortDescription: '',
      urlHandle: '',
      content: '',
      featureImageUrl: '',
      author: '',
      isVisible: true,
      publishedDate: new Date(),
      categories:[]

    }
  }
  ngOnInit(): void {
  this.categories$=this.categoryService.getAllCategory();
  }

  onFormSubmit():void {
   this.blogPostService.createBlogPost(this.model).subscribe({

    next:(response)=> {
      //console.log (response);
      this.router.navigateByUrl("/admin/blogposts");
    }
   });

  }
}
