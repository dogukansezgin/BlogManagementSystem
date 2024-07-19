export interface BlogPostGetByIdResponse {
    id: string,
    title: string,
    content: string,
    userId: string,
    userUserName: string,
    createdDate: Date,
    comments: CommentDto[]
}

export interface CommentDto {
    id: string,
    content: string,
    userId: string,
    userUserName: string,
    createdDate: Date,
    parentId: string,
    replies: CommentDto[]
}