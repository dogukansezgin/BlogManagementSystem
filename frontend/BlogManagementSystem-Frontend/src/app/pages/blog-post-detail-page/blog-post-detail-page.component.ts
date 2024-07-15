import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostDetailComponent } from '../../features/components/blog-posts/blog-post-detail/blog-post-detail.component';

@Component({
  selector: 'app-blog-post-detail-page',
  standalone: true,
  imports: [BlogPostDetailComponent],
  templateUrl: './blog-post-detail-page.component.html',
  styleUrl: './blog-post-detail-page.component.scss'
})
export class BlogPostDetailPageComponent implements OnInit {
  userName!: string;
  blogPostId!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getRouteParams();
  }

  getRouteParams(): void {
    this.route.paramMap.subscribe(params => {
      let userName = params.get('authorUsername');
      if (userName) this.userName = userName;

      let blogPostId = params.get('blogPostId');
      if (blogPostId) this.blogPostId = blogPostId;

      if (!this.isValidUserName(this.userName)) {
        this.router.navigate([''])
      }

      if (!this.isValidBlogPostId(this.blogPostId)) {
        this.router.navigate([''])
      }
    });
  }

  isValidUserName(userName: string): boolean {
    return /^[a-zA-Z0-9 _-]+$/.test(userName);
  }

  isValidBlogPostId(blogPostId: string): boolean {
    return /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(blogPostId);
  }
}
