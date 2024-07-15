import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { PageRequest } from "../../../core/models/page-request";
import { ListItemsDto } from "../../../core/models/list-items-dto";
import { BlogPostGetListResponse } from "../../models/responses/blogPosts/blog-post-get-list-response";
import { BlogPostGetByIdResponse } from "../../models/responses/blogPosts/blog-post-get-by-id-response";
import { BlogPostCreateRequest } from "../../models/requests/blogPosts/blog-post-create-request";
import { BlogPostCreateResponse } from "../../models/responses/blogPosts/blog-post-create-response";
import { BlogPostUpdateRequest } from "../../models/requests/blogPosts/blog-post-update-request";
import { BlogPostUpdateResponse } from "../../models/responses/blogPosts/blog-post-update-response";
import { BlogPostDeleteRequest } from "../../models/requests/blogPosts/blog-post-delete-request";
import { BlogPostDeleteResponse } from "../../models/responses/blogPosts/blog-post-delete-response";

@Injectable({
    providedIn: 'root'
})
export abstract class BlogPostBaseService {

    abstract getList(pageRequest: PageRequest): Observable<ListItemsDto<BlogPostGetListResponse>>;
    abstract getById(blogPostId: string): Observable<BlogPostGetByIdResponse>;

    abstract createBlogPost(blogPostCreateRequest: BlogPostCreateRequest): Observable<BlogPostCreateResponse>
    abstract updateBlogPost(blogPostUpdateRequest: BlogPostUpdateRequest): Observable<BlogPostUpdateResponse>
    abstract deleteBlogPost(blogPostDeleteRequest: BlogPostDeleteRequest): Observable<BlogPostDeleteResponse>
}