import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';

@Component({
  selector: 'app-add-blogpost',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule, MarkdownModule],
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent {
model:AddBlogPost;

  constructor(private blogPostService:BlogPostService,private router:Router) {
    this.model = {
      title: '',
      shortDescription: '',
      urlHandle: '',
      content: '',
      featureImageUrl: '',
      author: '',
      isVisible: true,
      publishedDate: new Date()

    }
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
