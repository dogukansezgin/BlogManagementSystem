import { DatePipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { SharedModule } from '../../../../shared/shared.module';

@Component({
  selector: 'app-blog-post-card',
  standalone: true,
  imports: [SharedModule, DatePipe],
  templateUrl: './blog-post-card.component.html',
  styleUrl: './blog-post-card.component.scss'
})
export class BlogPostCardComponent {
  @Input() authorUsername!: string;
  @Input() title!: string;
  @Input() content!: string;
  @Input() commentCount!: number;
  @Input() createdDate!: Date;
  
  constructor() {}
}
