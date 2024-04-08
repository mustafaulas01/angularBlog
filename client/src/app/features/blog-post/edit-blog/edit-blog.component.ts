import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category';
import { UpdateBlogPost } from '../models/update-blog-post.model';
import { ImageSelectorComponent } from '../../../shared/components/image-selector/image-selector.component';

@Component({
  selector: 'app-edit-blog',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule,ImageSelectorComponent],
  templateUrl: './edit-blog.component.html',
  styleUrl: './edit-blog.component.css'
})
export class EditBlogComponent implements OnInit, OnDestroy {

  id:string |null=null;
  routeSubsciption?:Subscription;
  blogUpdateSubscription?:Subscription;
  getBlogSubsription?:Subscription;
  model?:BlogPost;
  categories$? : Observable<Category []>;

  isImageSelectorVisible:boolean=false;
  selectedCategories?:string []

   constructor(private route:ActivatedRoute,private blogPostService:BlogPostService,
    private categoriService:CategoryService,private router:Router) {}

  ngOnDestroy(): void {
  this.routeSubsciption?.unsubscribe();
  this.blogUpdateSubscription?.unsubscribe();
  this.getBlogSubsription?.unsubscribe();
  }

  ngOnInit(): void {
    this.categories$= this.categoriService.getAllCategory()

    this.routeSubsciption = this.route.paramMap.subscribe({
      next: (response) => {
        this.id = response.get('id');

        if (this.id)
        this.getBlogSubsription=  this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
            this.model=response;
            this.selectedCategories=response.categories.map(x=>x.id);
            }
          });
      }
    })
  }

  onFormSubmit(): void {
    //convert this model to request object
    if (this.model && this.id) {
      var updateBlogPost: UpdateBlogPost =
      {
        title: this.model.title,
        shortDescription: this.model.shortDescription,
        content: this.model.content,
        author: this.model.author,
        urlHandle:this.model.urlHandle,
        featureImageUrl: this.model.featureImageUrl,
        publishedDate: this.model.publishedDate,
        isVisible: this.model.isVisible,
        categories: this.selectedCategories ?? []

      };
    
     this.blogUpdateSubscription= this.blogPostService.updateBlogPost(this.id,updateBlogPost).subscribe({
        next:(response)=>{
        this.router.navigateByUrl("/admin/blogposts");
        }
      });

    }
  }

  onDelete()
  {
    if(this.id)
    this.blogPostService.deleteBlogPost(this.id).subscribe({
    next:(response)=> { this.router.navigateByUrl("/admin/blogposts")}
    })
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
