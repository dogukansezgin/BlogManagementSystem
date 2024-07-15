import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { LoginFormComponent } from './features/components/auth/login-form/login-form.component';
import { RegisterFormComponent } from './features/components/auth/register-form/register-form.component';
import { PreventLoginAccessGuard } from './core/guards/prevent-login-access.guard';
import { BlogPostDetailPageComponent } from './pages/blog-post-detail-page/blog-post-detail-page.component';
import { BlogPostEditorPageComponent } from './pages/blog-post-editor-page/blog-post-editor-page.component';
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';

export const routes: Routes =
    [
        { path: '', pathMatch: 'full', component: HomePageComponent },
        { path: 'homepage', redirectTo: '', pathMatch: 'full' },

        { path: 'p/:authorUsername/:blogPostId', component: BlogPostDetailPageComponent},

        { path: 'write', redirectTo: 'write/new', pathMatch: 'full'},
        {
            path: 'write', canActivate: [AuthGuard, RoleGuard], data: { expectedRoles: ['User'] },
            children: [
                { path: ':operation', component: BlogPostEditorPageComponent },
                { path: ':operation/:blogPostId', component: BlogPostEditorPageComponent }
            ]
        },

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
