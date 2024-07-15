import { Component, Input, OnInit } from '@angular/core';
import { BlogPostBaseService } from '../../../services/abstracts/blog-post-base.service';
import { BlogPostGetByIdResponse } from '../../../models/responses/blogPosts/blog-post-get-by-id-response';
import { CommonModule, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogCommentFormComponent } from '../blog-comment-form/blog-comment-form.component';
import { AuthBaseService } from '../../../services/abstracts/auth-base.service';
import { BlogCommentCardComponent } from '../blog-comment-card/blog-comment-card.component';
import { BlogPostDeleteRequest } from '../../../models/requests/blogPosts/blog-post-delete-request';

@Component({
  selector: 'app-blog-post-detail',
  standalone: true,
  imports: [CommonModule, DatePipe, BlogCommentCardComponent, BlogCommentFormComponent],
  templateUrl: './blog-post-detail.component.html',
  styleUrl: './blog-post-detail.component.scss'
})
export class BlogPostDetailComponent implements OnInit {
  @Input() userName!: string;
  @Input() blogPostId!: string;

  authUserId!: string | null;

  blogPost: BlogPostGetByIdResponse = {
    id: '',
    title: '',
    content: '',
    userId: '',
    userUserName: '',
    createdDate: new Date(),
    comments: []
  }

  constructor(
    private authService: AuthBaseService,
    private blogPostService: BlogPostBaseService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getBlogPostDetails(this.blogPostId);
    this.authUserId = this.authService.getCurrentUserId();
  }

  getBlogPostDetails(blogPostId: string): void {
    this.blogPostService.getById(blogPostId).subscribe({
      next: (response) => {
        this.blogPost = response;
        this.blogPost.comments.sort((a, b) => new Date(b.createdDate).getTime() - new Date(a.createdDate).getTime())
      },
      error: (err) => {

      },
      complete: () => {
        this.route.paramMap.subscribe(params => {
          let userName = params.get('authorUsername');

          if (userName != this.routeFormat(this.blogPost.userUserName)) {
            this.router.navigate([''])
          }
        });
      }
    });
  }

  routeFormat(value: string): string {
    const turkishCharacters: Record<string, string> = {
      'ç': 'c', 'ğ': 'g', 'ı': 'i', 'ö': 'o', 'ş': 's', 'ü': 'u',
      'Ç': 'C', 'Ğ': 'G', 'İ': 'I', 'Ö': 'O', 'Ş': 'S', 'Ü': 'U'
    };

    return value.toLowerCase().replace(/[çğıöşüÇĞİÖŞÜ]/g, match => turkishCharacters[match] || match);
  }

  onCommentAdded(newComment: any): void {
    this.blogPost.comments.unshift(newComment); // Username does not appear in new comments unless the page is refreshed.
  }
  onCommentDeleted(commentId: string): void {
    this.blogPost.comments = this.blogPost.comments.filter(comment => comment.id !== commentId);
  }

  editBlogPost(): void {
    this.router.navigate([`write/edit/${this.blogPostId}`])
  }

  deleteBlogPost(): void {
    if (this.blogPostId) {
      let blogpostDeleteModel: BlogPostDeleteRequest = {
        id: this.blogPostId,
        isPermament: false
      };

      this.blogPostService.deleteBlogPost(blogpostDeleteModel).subscribe({
        next: (response) => {
          this.router.navigate([''])
        },
        error: (err) => {
          
        }
      });
    }
  }
}