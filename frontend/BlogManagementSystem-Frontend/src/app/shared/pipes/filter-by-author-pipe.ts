import { Pipe, PipeTransform } from '@angular/core';
import { BlogPostGetListResponse } from '../../features/models/responses/blogPosts/blog-post-get-list-response';

@Pipe({
  name: 'filterByAuthor'
})
export class FilterByAuthorPipe implements PipeTransform {
  transform(posts: BlogPostGetListResponse[], author: string): BlogPostGetListResponse[] {
    if (!author || author.trim() === '') {
      return posts;
    }
    return posts.filter(post => post.userUserName.toLowerCase().includes(author.toLowerCase()));
  }
}