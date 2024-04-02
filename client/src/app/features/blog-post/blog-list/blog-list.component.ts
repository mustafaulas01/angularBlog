import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { BlogPost } from '../models/blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-list',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule],
  templateUrl: './blog-list.component.html',
  styleUrl: './blog-list.component.css'
})
export class BlogListComponent implements OnInit {

  blogPost$?:Observable<BlogPost[]> ;

   constructor(private blogPostService:BlogPostService) {}

  ngOnInit(): void {
  
    this.blogPost$= this.blogPostService.getAllBlogPost();

  }

}
