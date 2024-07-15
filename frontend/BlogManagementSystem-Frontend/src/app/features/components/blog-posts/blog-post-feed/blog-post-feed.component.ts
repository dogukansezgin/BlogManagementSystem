import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ListItemsDto } from '../../../../core/models/list-items-dto';
import { PageRequest } from '../../../../core/models/page-request';
import { BlogPostGetListResponse } from '../../../models/responses/blogPosts/blog-post-get-list-response';
import { BlogPostBaseService } from '../../../services/abstracts/blog-post-base.service';
import { BlogPostCardComponent } from '../blog-post-card/blog-post-card.component';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../../../shared/shared.module';

@Component({
  selector: 'app-blog-post-feed',
  standalone: true,
  imports: [CommonModule, BlogPostCardComponent, RouterLink, FormsModule, SharedModule],
  templateUrl: './blog-post-feed.component.html',
  styleUrl: './blog-post-feed.component.scss'
})
export class BlogPostFeedComponent implements OnInit {

  blogPostList: ListItemsDto<BlogPostGetListResponse> = {
    index: 0,
    size: 0,
    count: 0,
    hasNext: false,
    hasPrevious: false,
    pages: 0,
    items: []
  };

  selectedAuthor!: string;
  selectedDate!: Date;

  constructor(private blogPostService: BlogPostBaseService) { }

  ngOnInit(): void {
    this.getBlogPostList({ pageIndex: 0, pageSize: 100 });
  }

  getBlogPostList(pageRequest: PageRequest): void {
    this.blogPostService.getList(pageRequest).subscribe({
      next: (response) => {
        this.blogPostList = response;
      },
      error: (err) => {

      }
    });
  }
  
  routeToDetail(userName: string, blogPostId: string) {
    userName = this.routeFormat(userName);

    return `p/${userName}/${blogPostId}`
  }

  routeFormat(value: string): string {
    const turkishCharacters: Record<string, string> = {
      'ç': 'c', 'ğ': 'g', 'ı': 'i', 'ö': 'o', 'ş': 's', 'ü': 'u',
      'Ç': 'C', 'Ğ': 'G', 'İ': 'I', 'Ö': 'O', 'Ş': 'S', 'Ü': 'U'
    };

    return value.toLowerCase().replace(/[çğıöşüÇĞİÖŞÜ]/g, match => turkishCharacters[match] || match);
  }
}
