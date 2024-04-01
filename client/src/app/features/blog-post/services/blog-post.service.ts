import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http:HttpClient) { }

  createBlogPost(model:AddBlogPost):Observable<BlogPost>
  {
   return this.http.post<BlogPost>(`${environment.apiBaseUrl}/api/blogpost`,model);
  }
}
