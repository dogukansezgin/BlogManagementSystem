import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { RegisterFormComponent } from '../../features/components/auth/register-form/register-form.component';
import { LoginFormComponent } from '../../features/components/auth/login-form/login-form.component';

@Component({
  selector: 'app-auth-page',
  standalone: true,
  imports: [RouterOutlet, RegisterFormComponent, LoginFormComponent, RouterLink],
  templateUrl: './auth-page.component.html',
  styleUrl: './auth-page.component.scss'
})
export class AuthPageComponent {

}
