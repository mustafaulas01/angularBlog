import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { response } from 'express';
import { Observable, Subscription } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css'
})
export class BlogDetailsComponent implements OnInit {

  url: string | null = null;
  blogSubsription?:Subscription;

  blogPost$?:Observable<BlogPost>;

  constructor(private route: ActivatedRoute,private blogService:BlogPostService) { }



  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next: (response) => {
        this.url = response.get('url');
      }
    })

     if(this.url)
     {
     this.blogPost$= this.blogService.getBlogPostByUrl(this.url);

     }
 
    //fetch blog details by url

  }
}
