import { Component } from '@angular/core';
import { BlogPostFeedComponent } from '../../features/components/blog-posts/blog-post-feed/blog-post-feed.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [BlogPostFeedComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

}
