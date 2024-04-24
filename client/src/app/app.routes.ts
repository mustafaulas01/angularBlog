import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { CategoryEditComponent } from './features/category/category-edit/category-edit.component';
import { BlogListComponent } from './features/blog-post/blog-list/blog-list.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';
import { EditBlogComponent } from './features/blog-post/edit-blog/edit-blog.component';
import { HomeComponent } from './features/public/home/home.component';
import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/guards/auth.guard';

export const routes: Routes = [
    {path:'admin/categories',title:"Categories",component:CategoryListComponent,canActivate:[authGuard]},
    {path:'admin/categories/add',title:"Category-Add",component:AddCategoryComponent,canActivate:[authGuard]},
    {path:'admin/categories/:id',title:"Category-Edit",component:CategoryEditComponent,canActivate:[authGuard]},
    {path:'admin/blogposts',title:"Blog Post Lists",component:BlogListComponent,canActivate:[authGuard]},
    {path:'admin/blogposts/add',title:"Blog Post Add",component:AddBlogpostComponent,canActivate:[authGuard]},
    {path:'admin/blogposts/:id',title:"Blog Post Edit",component:EditBlogComponent,canActivate:[authGuard]},
    {path:'',title:"Home",component:HomeComponent},
    {path:'blog/:url',title:"Blog-Detail",component:BlogDetailsComponent},
    {path:'login',title:"Login",component:LoginComponent}

];
