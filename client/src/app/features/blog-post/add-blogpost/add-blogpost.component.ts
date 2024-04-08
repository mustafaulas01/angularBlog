import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category';
import { ImageSelectorComponent } from '../../../shared/components/image-selector/image-selector.component';
import { ImageService } from '../../../shared/components/image-selector/image.service';


@Component({
  selector: 'app-add-blogpost',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule,ImageSelectorComponent],
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent implements OnInit ,OnDestroy {
model:AddBlogPost;
categories$?:Observable<Category []>;
imageSelectoreSubsription?:Subscription;

isImageSelectorVisible:boolean=false;

  constructor(private blogPostService:BlogPostService,private router:Router,private categoryService:CategoryService,private imageService:ImageService) {
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
  ngOnDestroy(): void {
    this.imageSelectoreSubsription?.unsubscribe();
  }
  ngOnInit(): void {
  this.categories$=this.categoryService.getAllCategory();
  this.imageSelectoreSubsription= this.imageService.onSelectImage().subscribe({
    next:(response)=> {
    
          this.model.featureImageUrl=response.url;
          this.closeImageSelector();
        
    }
  })

  }

  onFormSubmit():void {
   this.blogPostService.createBlogPost(this.model).subscribe({

    next:(response)=> {
      //console.log (response);
      this.router.navigateByUrl("/admin/blogposts");
    }
   });

  }

  openImageSelector():void
  {
  this.isImageSelectorVisible=true;
  }

  closeImageSelector():void
  {
  this.isImageSelectorVisible=false;
  }
}
