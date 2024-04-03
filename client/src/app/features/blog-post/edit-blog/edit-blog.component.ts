import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-blog',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './edit-blog.component.html',
  styleUrl: './edit-blog.component.css'
})
export class EditBlogComponent implements OnInit, OnDestroy {

  id:string |null=null;
  routeSubsciption?:Subscription;
  model?:BlogPost

  constructor(private route:ActivatedRoute,private blogPostService:BlogPostService) {}

  ngOnDestroy(): void {
  this.routeSubsciption?.unsubscribe();
  }

  ngOnInit(): void {
    this.routeSubsciption = this.route.paramMap.subscribe({
      next: (response) => {
        this.id = response.get('id');

        if (this.id)
          this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
            this.model=response;
            }
          });
      }
    })
  }

  onFormSubmit():void {
    
  }

}
