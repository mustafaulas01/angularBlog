import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { CategoryEditComponent } from './features/category/category-edit/category-edit.component';
import { BlogListComponent } from './features/blog-post/blog-list/blog-list.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';

export const routes: Routes = [
    {path:'admin/categories',title:"Categories",component:CategoryListComponent},
    {path:'admin/categories/add',title:"Category-Add",component:AddCategoryComponent},
    {path:'admin/categories/:id',title:"Category-Edit",component:CategoryEditComponent},
    {path:'admin/blogposts',title:"Blog Post Lists",component:BlogListComponent},
    {path:'admin/blogposts/add',title:"Blog Post Add",component:AddBlogpostComponent}
];
