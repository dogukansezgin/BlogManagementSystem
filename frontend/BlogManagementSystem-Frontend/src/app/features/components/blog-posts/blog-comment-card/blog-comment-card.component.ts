import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommentBaseService } from '../../../services/abstracts/comment-base.service';
import { CommentDeleteRequest } from '../../../models/requests/comments/comment-delete-request';
import { CommentDto } from '../../../models/responses/blogPosts/blog-post-get-by-id-response';

@Component({
  selector: 'app-blog-comment-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './blog-comment-card.component.html',
  styleUrl: './blog-comment-card.component.scss'
})
export class BlogCommentCardComponent {
  @Input() comment!: CommentDto;
  @Input() authUserId!: string;

  @Output() commentDeleted = new EventEmitter<string>();
  @Output() parentComment = new EventEmitter<string>();
  @Output() scrollToCommentInput = new EventEmitter<void>();

  constructor(private commentService: CommentBaseService) {}

  replyComment(commentId: string): void {
    if (commentId) {
      this.parentComment.emit(commentId);
      this.scrollToCommentInput.emit();
    }
  }

  deleteComment(commentId: string): void {
    if (commentId) {
      let commentDeleteModel: CommentDeleteRequest = {
        id: commentId,
        isPermament: false
      };

      this.commentService.deleteComment(commentDeleteModel).subscribe({
        next: (response) => {
          this.commentDeleted.emit(commentId);
        },
        error: (err) => {
          
        }
      });
    }
  }
}
