export interface CommentCreateRequest{
    content: string,
    userId: string
    blogPostId: string,
    parentId?: string | null
}