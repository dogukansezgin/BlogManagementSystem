import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommentBaseService } from '../../../services/abstracts/comment-base.service';
import { CommentCreateRequest } from '../../../models/requests/comments/comment-create-request';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-comment-form',
  standalone: true,
  imports: [ButtonModule, ReactiveFormsModule, ToastModule, CommonModule],
  templateUrl: './blog-comment-form.component.html',
  styleUrl: './blog-comment-form.component.scss',
  providers: [MessageService]
})
export class BlogCommentFormComponent implements OnInit {
  @Input() userId!: string;
  @Input() blogPostId!: string;
  @Input() parentCommentId!: string | null;
  
  @Output() commentAdded = new EventEmitter<any>();
  @Output() replyEnded = new EventEmitter<any>();
  
  commentForm!: FormGroup;
  submitted: boolean = false;

  constructor(
    private fb: FormBuilder,
    private commentService: CommentBaseService,
    private messageService: MessageService
  ) {}
  
  ngOnInit(): void {
    this.createCommentForm();
  }

  createCommentForm(): void {
    this.commentForm = this.fb.group({
      content: new FormControl('', [Validators.required]),
    });
  }

  cancelReply(): void {
    this.replyEnded.emit();
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.userId == 'null') {
      this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'You must log in before posting a comment.', life: 10000 });
      this.submitted = false;
      return;
    }
    if (this.commentForm.valid) {
      let commentCreateModel: CommentCreateRequest = Object.assign({}, this.commentForm.value);
      commentCreateModel.userId = this.userId;
      commentCreateModel.blogPostId = this.blogPostId;
      commentCreateModel.parentId = this.parentCommentId;

      this.commentService.createComment(commentCreateModel).subscribe({
        next: (response) => {
          this.commentAdded.emit(response);
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Your comment has been submitted successfully.', life: 10000 });
        },
        error: (err) => {
          
        }
      });

      this.commentForm.patchValue({
        content: ''
      });
    }
    this.submitted = false;
  }
}
