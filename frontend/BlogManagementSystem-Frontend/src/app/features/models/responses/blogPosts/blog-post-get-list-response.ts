export interface BlogPostGetListResponse {
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
    createdDate: Date
}