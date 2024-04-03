import { Category } from "../../category/models/category";

export interface BlogPost {
    id:string;
    title:string;
    shortDescription:string;
    content:string;
    featureImageUrl:string;
    urlHandle:string;
    publishedDate:Date;
    author:string;
    isVisible:boolean;
    categories:Category[];

}