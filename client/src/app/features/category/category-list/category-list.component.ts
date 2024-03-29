import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit {

  categories$?:Observable<Category[]>;

  constructor(private categoryService:CategoryService) { }
  ngOnInit(): void {
   this.categories$= this.categoryService.getAllCategory();
  }
 



}
