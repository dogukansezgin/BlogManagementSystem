import { Pipe, PipeTransform } from '@angular/core';
import { BlogPostGetListResponse } from '../../features/models/responses/blogPosts/blog-post-get-list-response';

@Pipe({
  name: 'filterByDate'
})
export class FilterByDatePipe implements PipeTransform {
  transform(posts: BlogPostGetListResponse[], selectedDate: Date): BlogPostGetListResponse[] {
    if (!selectedDate) {
      return posts;
    }
    
    return posts.filter(post => new Date(post.createdDate).toDateString() === new Date(selectedDate).toDateString());
  }
}