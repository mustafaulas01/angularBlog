import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { UpdateBlogPost } from '../models/update-blog-post.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http:HttpClient) { }

  createBlogPost(model:AddBlogPost):Observable<BlogPost>
  {
   return this.http.post<BlogPost>(`${environment.apiBaseUrl}/api/blogpost`,model);
  }
  getAllBlogPost():Observable<BlogPost[]>
  {
   return this.http.get<BlogPost []>(`${environment.apiBaseUrl}/api/blogpost`);
  }
  getBlogPostById(id:string):Observable<BlogPost>
  {
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/blogpost/${id}`);
  }
  updateBlogPost(id: string, updatedBlogModel: UpdateBlogPost): Observable<BlogPost> {

    return this.http.put<BlogPost>(`${environment.apiBaseUrl}/api/blogpost/${id}`,updatedBlogModel);
  }
  deleteBlogPost(id:string):Observable<any>
  {
    return this.http.delete<any>(`${environment.apiBaseUrl}/api/blogpost/${id}`);
  }

}
