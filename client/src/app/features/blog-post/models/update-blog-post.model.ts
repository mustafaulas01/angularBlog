export interface UpdateBlogPost
{
    title:string;
    shortDescription:string;
    content:string;
    featureImageUrl:string;
    urlHandle:string;
    publishedDate:Date;
    author:string;
    isVisible:boolean;
    categories:string [];
}