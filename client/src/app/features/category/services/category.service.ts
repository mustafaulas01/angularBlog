import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category';
import { environment } from '../../../../environments/environment.development';
import { UpdateCategoryModel } from '../models/update-category-model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }

  addCategory(model:AddCategoryRequest):Observable<void> {

    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories`,model);
  }

  getAllCategory():Observable<Category []>
  {
    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/categories`);
  }

  getCategoryById(id:string):Observable<Category>
  {
   return this.http.get<Category>(`${environment.apiBaseUrl}/api/categories/${id}`);
  }

  updateCategory(id: string, categoryModel: UpdateCategoryModel): Observable<Category> {


    return this.http.put<Category>(`${environment.apiBaseUrl}/api/categories/${id}`, categoryModel);
  }

  deleteCategory(id:string):Observable<any>
  {
    return this.http.delete<any>(`${environment.apiBaseUrl}/api/categories/${id}`);
  }

}
