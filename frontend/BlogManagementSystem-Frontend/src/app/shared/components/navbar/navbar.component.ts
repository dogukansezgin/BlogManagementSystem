import { Component, OnInit } from '@angular/core';
import { ThemeBaseService } from '../../../features/services/abstracts/theme-base.service';
import { AuthBaseService } from '../../../features/services/abstracts/auth-base.service';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { TokenBaseService } from '../../../features/services/abstracts/token-base.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {

  menuItems!: MenuItem[];
  authItems!: MenuItem[];
  
  themePreference: string = 'light';
  darkMode: boolean = false;
  
  isLoggedIn: boolean = false;
  dsadas: string='dsadas'
  constructor(
    private themeService: ThemeBaseService,
    private authService: AuthBaseService,
    private tokenService: TokenBaseService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.setThemePreference().subscribe(() => {
      this.checkUserAuth().subscribe(() => {
        this.getMenuItems();
      });
    });
  }
  
  setThemePreference(): Observable<void> {
    this.themePreference = this.themeService.initialTheme();
    if (this.themePreference == 'dark') {
      this.darkMode = true;
    }
    return of(undefined);
  }
  
  switchDarkMode() {
    this.darkMode = !this.darkMode;
    if (this.darkMode) {
      this.changeTheme('dark');
    } else {
      this.changeTheme('light');
    }
  }
  
  changeTheme(theme: string) {
    this.themeService.switchTheme(theme);
  }

  checkUserAuth(): Observable<void> {
    this.isLoggedIn = this.authService.isAuthenticated();
    return of(undefined);
  }

  logOut(): void {
    this.authService.logOut();
    this.router.navigate([''])
  }

  getMenuItems(): void {
    if (this.isLoggedIn) {

      this.menuItems = [
        {
          label: "Blogs",
          routerLink: '/blogs',
        }
      ]

      this.authItems = [
        {
          label: 'Settings',
          icon: 'pi pi-user',
          items: [
            {
              label: 'Hello',
              styleClass: 'header-text',
              disabled: true
            },
            {
              label: this.tokenService.getCurrentEmailAddress(),
              styleClass: 'subtext',
              disabled: true
            },
            {
              separator: true
            },
            {
              label: 'Profile',
            },
            {
              label: 'Your stories',
            },
            {
              separator: true
            },
            {
              label: 'Log Out',
            },
          ]
        }
      ]

    }
    else {

      this.menuItems = [
        {
          label: "Blogs",
          routerLink: '/blogs',
        }
      ]

    }
  }

  menuItemClicked(item: any) {
    if (item.label === 'Log Out') {
      this.logOut();
    }
  }
}
