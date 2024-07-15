import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormControl, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BlogPostBaseService } from '../../../services/abstracts/blog-post-base.service';
import { BlogPostCreateRequest } from '../../../models/requests/blogPosts/blog-post-create-request';
import { BlogPostUpdateRequest } from '../../../models/requests/blogPosts/blog-post-update-request';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { AuthBaseService } from '../../../services/abstracts/auth-base.service';

@Component({
  selector: 'app-blog-post-editor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, ButtonModule],
  templateUrl: './blog-post-editor.component.html',
  styleUrl: './blog-post-editor.component.scss'
})
export class BlogPostEditorComponent implements OnInit {
  @Input() operation!: string;
  @Input() blogPostId!: string | null;

  blogPostForm!: FormGroup;
  authUserId!: string | null;
  submitted: boolean = false;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private blogPostService: BlogPostBaseService,
    private authService: AuthBaseService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.validateParams();
    this.authUserId = this.authService.getCurrentUserId();
  }

  validateParams(): void {
    if (this.operation == "new" && this.blogPostId != null) {
      this.router.navigate(['write/new'])
    } else if (this.operation == "edit" && this.blogPostId == null) {
      this.router.navigate(['write/new'])
    }

    if (this.operation == "new" && this.blogPostId == null) {
      this.router.navigate(['write/new'])
    } else if (this.operation == "edit" && this.blogPostId != null) {
      this.loadBlogPost(this.blogPostId);
    } 
  }

  initializeForm(): void {
    this.blogPostForm = this.fb.group({
      title: new FormControl('', [Validators.required]),
      content: new FormControl('', [Validators.required]),
    });
  }

  loadBlogPost(blogPostId: string): void {
    this.blogPostService.getById(blogPostId).subscribe({
      next: (response) => {
        if (this.authUserId != response.userId) {
          this.router.navigate(['write/new']);
        }

        this.blogPostForm.patchValue({
          title: response.title,
          content: response.content
        });
      },
      error: (err) => {

      }
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.blogPostForm.valid) {
      if (this.operation === 'new') {
        this.createBlogPost();
      } else if (this.operation === 'edit') {
        this.updateBlogPost();
      }
    }
    this.submitted = false;
  }

  createBlogPost(): void {
    let newBlogPost: BlogPostCreateRequest = { ...this.blogPostForm.value, userId: this.authUserId };
    
    this.blogPostService.createBlogPost(newBlogPost).subscribe({
      next: (response) => {
        this.router.navigate([''])
      },
      error: (err) => {

      }
    });
  }

  updateBlogPost(): void {
    let updatedBlogPost: BlogPostUpdateRequest = { ...this.blogPostForm.value, id: this.blogPostId, userId: this.authUserId };

    this.blogPostService.updateBlogPost(updatedBlogPost).subscribe({
      next: (response) => {
        this.router.navigate([''])
      },
      error: (err) => {

      }
    });
  }
}

