import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit {

  categories?: Category[];

  constructor(private categoryService:CategoryService) { }
  ngOnInit(): void {
    this.categoryService.getAllCategory().subscribe(
      {
        next:(response)=>{this.categories=response;},
        error:(error)=> {console.error("list error:"+error)}
      }
    )
  }
 



}
