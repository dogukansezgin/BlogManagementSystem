import { AfterViewInit, Component, HostListener, Inject, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs';
import { SharedModule } from './shared/shared.module';
import { CommonModule, DOCUMENT } from '@angular/common';
import { ThemeBaseService } from './features/services/abstracts/theme-base.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SharedModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit, AfterViewInit {

  lastScrollTop = 0;
  navbarHeight = 69;
  navbar: HTMLElement | null = null;

  private hiddenRoutes: string[] = ['/auth'];

  constructor(private router: Router, private themeService: ThemeBaseService,  @Inject(DOCUMENT) private document: Document) { }
  
  ngOnInit(): void {
    this.themeService.initialTheme();
    
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      window.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
      });
    });
    
  }
  
  ngAfterViewInit(): void {
    this.navbar = this.document.getElementById('cs-navbar') as HTMLLinkElement;
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    if (!this.navbar) return;
    const currentScroll = window.scrollY || document.documentElement.scrollTop;
    
    if (currentScroll > this.navbarHeight * 1.2){
      this.navbar.classList.add('sticky');
    }

    if (currentScroll > this.lastScrollTop && currentScroll > this.navbarHeight) {
      // Scroll down
      this.navbar.style.transform = `translateY(-${this.navbarHeight - 1}px)`
    } else {
      // Scroll up
      if (currentScroll < this.navbarHeight) {
        this.navbar.classList.remove('xSd2');
      }
      this.navbar.style.transform = 'translateY(0)'
    }

    this.lastScrollTop = currentScroll <= 0 ? 0 : currentScroll;
  }

  isNotOnRestrictedRoutes(): boolean {
    return !this.hiddenRoutes.some(route => this.router.url.includes(route));
  }
}
