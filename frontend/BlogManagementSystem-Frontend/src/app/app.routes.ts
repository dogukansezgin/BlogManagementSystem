import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { LoginFormComponent } from './features/components/auth/login-form/login-form.component';
import { RegisterFormComponent } from './features/components/auth/register-form/register-form.component';
import { PreventLoginAccessGuard } from './core/guards/prevent-login-access.guard';

export const routes: Routes =
    [
        { path: '', pathMatch: 'full', component: HomePageComponent },
        { path: 'homepage', redirectTo: '', pathMatch: 'full' },

        {
            path: 'auth', component: AuthPageComponent, canActivate: [PreventLoginAccessGuard],
            children: [
                { path: 'login', component: LoginFormComponent },
                { path: 'register', component: RegisterFormComponent },
                { path: '', redirectTo: 'register', pathMatch: 'full' }
            ]
        },

        { path: '**', redirectTo: '' }
    ];
