import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CommentCreateRequest } from "../../models/requests/comments/comment-create-request";
import { CommentCreateResponse } from "../../models/responses/comments/comment-create-response";
import { CommentDeleteRequest } from "../../models/requests/comments/comment-delete-request";
import { CommentDeleteResponse } from "../../models/responses/comments/comment-delete-response";

@Injectable({
    providedIn: 'root'
})
export abstract class CommentBaseService {

    abstract createComment(commentCreateRequest: CommentCreateRequest): Observable<CommentCreateResponse>
    abstract deleteComment(commentDeleteRequest: CommentDeleteRequest): Observable<CommentDeleteResponse>
}