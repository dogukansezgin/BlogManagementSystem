import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostEditorComponent } from '../../features/components/blog-posts/blog-post-editor/blog-post-editor.component';

@Component({
  selector: 'app-blog-post-editor-page',
  standalone: true,
  imports: [BlogPostEditorComponent],
  templateUrl: './blog-post-editor-page.component.html',
  styleUrl: './blog-post-editor-page.component.scss'
})
export class BlogPostEditorPageComponent implements OnInit {
  operation!: string;
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
      let operation = params.get('operation');
      if (operation) this.operation = operation;

      if (!this.isValidOperation(this.operation)) {
        this.router.navigate([''])
      }

      let blogPostId = params.get('blogPostId');
      if (blogPostId) {
        this.blogPostId = blogPostId;

        if (!this.isValidBlogPostId(this.blogPostId)) {
          this.router.navigate([''])
        }
      }
    });
  }

  isValidOperation(operation: string): boolean {
    return operation === 'new' || operation === 'edit';
  }

  isValidBlogPostId(blogPostId: string): boolean {
    return /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(blogPostId);
  }
}
