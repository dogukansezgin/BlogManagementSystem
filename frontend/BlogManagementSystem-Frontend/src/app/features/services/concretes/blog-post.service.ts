import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { BlogPostBaseService } from "../abstracts/blog-post-base.service";
import { map, Observable } from "rxjs";
import { ListItemsDto } from "../../../core/models/list-items-dto";
import { PageRequest } from "../../../core/models/page-request";
import { BlogPostGetByIdResponse } from "../../models/responses/blogPosts/blog-post-get-by-id-response";
import { BlogPostGetListResponse } from "../../models/responses/blogPosts/blog-post-get-list-response";
import { BlogPostCreateRequest } from "../../models/requests/blogPosts/blog-post-create-request";
import { BlogPostUpdateRequest } from "../../models/requests/blogPosts/blog-post-update-request";
import { BlogPostCreateResponse } from "../../models/responses/blogPosts/blog-post-create-response";
import { BlogPostUpdateResponse } from "../../models/responses/blogPosts/blog-post-update-response";
import { BlogPostDeleteRequest } from "../../models/requests/blogPosts/blog-post-delete-request";
import { BlogPostDeleteResponse } from "../../models/responses/blogPosts/blog-post-delete-response";

@Injectable()
export class BlogPostService extends BlogPostBaseService {
   
    private readonly apiUrl_GetList: string = environment.apiUrl + environment.endpoints.blogPosts.getList;
    private readonly apiUrl_GetById: string = environment.apiUrl + environment.endpoints.blogPosts.getById;
    private readonly apiUrl_Create: string = environment.apiUrl + environment.endpoints.blogPosts.create;
    private readonly apiUrl_Update: string = environment.apiUrl + environment.endpoints.blogPosts.update;
    private readonly apiUrl_Delete: string = environment.apiUrl + environment.endpoints.blogPosts.delete;
    
    constructor(private httpClient: HttpClient) {
        super();
    }
    
    override getList(pageRequest: PageRequest): Observable<ListItemsDto<BlogPostGetListResponse>> {
        const newRequest: { [key: string]: string | number } = {
            pageIndex: pageRequest.pageIndex,
            pageSize: pageRequest.pageSize
        }
        return this.httpClient.get<ListItemsDto<BlogPostGetListResponse>>(this.apiUrl_GetList, { params: newRequest })
            .pipe(
                map((response) => {
                    const newResponse: ListItemsDto<BlogPostGetListResponse> = {
                        items: response.items,
                        index: response.index,
                        size: response.size,
                        count: response.count,
                        pages: response.pages,
                        hasNext: response.hasNext,
                        hasPrevious: response.hasPrevious
                    };
                    return newResponse;
                })
            );
    };

    override getById(blogPostId: string): Observable<BlogPostGetByIdResponse> {
        return this.httpClient.get<BlogPostGetByIdResponse>(this.apiUrl_GetById + blogPostId);
    }

    override createBlogPost(blogPostCreateRequest: BlogPostCreateRequest): Observable<BlogPostCreateResponse> {
        return this.httpClient.post<BlogPostCreateResponse>(this.apiUrl_Create, blogPostCreateRequest);
    }

    override updateBlogPost(blogPostUpdateRequest: BlogPostUpdateRequest): Observable<BlogPostUpdateResponse> {
        return this.httpClient.put<BlogPostUpdateResponse>(this.apiUrl_Update, blogPostUpdateRequest);
    }

    override deleteBlogPost(blogPostDeleteRequest: BlogPostDeleteRequest): Observable<BlogPostDeleteResponse> {
        return this.httpClient.post<BlogPostDeleteResponse>(this.apiUrl_Delete, blogPostDeleteRequest);
    }
}