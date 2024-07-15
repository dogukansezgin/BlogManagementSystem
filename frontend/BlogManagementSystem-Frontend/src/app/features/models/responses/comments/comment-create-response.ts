export interface CommentCreateResponse{
    id: string,
    content: string,
    userId: string
    blogPostId: string,
    createdDate: Date
}