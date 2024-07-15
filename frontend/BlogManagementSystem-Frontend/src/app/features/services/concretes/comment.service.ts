import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { CommentBaseService } from "../abstracts/comment-base.service";
import { Observable } from "rxjs";
import { CommentCreateRequest } from "../../models/requests/comments/comment-create-request";
import { CommentCreateResponse } from "../../models/responses/comments/comment-create-response";
import { CommentDeleteRequest } from "../../models/requests/comments/comment-delete-request";
import { CommentDeleteResponse } from "../../models/responses/comments/comment-delete-response";

@Injectable()
export class CommentService extends CommentBaseService {
    
    private readonly apiUrl_Create: string = environment.apiUrl + environment.endpoints.comments.create;
    private readonly apiUrl_Delete: string = environment.apiUrl + environment.endpoints.comments.delete;
    
    constructor(private httpClient: HttpClient) {
        super();
    }
    
    override createComment(commentCreateRequest: CommentCreateRequest): Observable<CommentCreateResponse> {
        return this.httpClient.post<CommentCreateResponse>(this.apiUrl_Create, commentCreateRequest);
    }
    override deleteComment(commentDeleteRequest: CommentDeleteRequest): Observable<CommentDeleteResponse> {
        return this.httpClient.post<CommentDeleteResponse>(this.apiUrl_Delete, commentDeleteRequest);

    }
}