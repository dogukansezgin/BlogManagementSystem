export interface CommentCreateResponse{
    id: string,
    content: string,
    userId: string
    blogPostId: string,
    parentId?: string | null
    createdDate: Date
}