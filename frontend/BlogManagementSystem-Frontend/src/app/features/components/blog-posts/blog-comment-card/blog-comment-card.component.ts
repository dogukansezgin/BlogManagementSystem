import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommentBaseService } from '../../../services/abstracts/comment-base.service';
import { CommentDeleteRequest } from '../../../models/requests/comments/comment-delete-request';

@Component({
  selector: 'app-blog-comment-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './blog-comment-card.component.html',
  styleUrl: './blog-comment-card.component.scss'
})
export class BlogCommentCardComponent {
  @Input() commentId!: string;
  @Input() commentAuthorUsername!: string;
  @Input() commentAuthorId!: string;
  @Input() content!: string;
  @Input() createdDate!: Date;
  @Input() authUserId!: string;

  @Output() commentDeleted = new EventEmitter<string>();

  constructor(private commentService: CommentBaseService) {}

  deleteComment(commentId: string): void {
    if (commentId) {
      let commentDeleteModel: CommentDeleteRequest = {
        id: commentId,
        isPermament: false
      };

      this.commentService.deleteComment(commentDeleteModel).subscribe({
        next: (response) => {
          console.log(response);
          this.commentDeleted.emit(commentId);
        },
        error: (err) => {
          
        }
      });
    }
  }
}
