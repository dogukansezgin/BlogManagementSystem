import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { provideNoopAnimations } from "@angular/platform-browser/animations";
import { provideRouter } from "@angular/router";
import { routes } from "../../app.routes";
import { AuthInterceptor } from "../../core/interceptors/auth.interceptor";
import { ErrorInterceptor } from "../../core/interceptors/error.interceptor";
import { ThemeBaseService } from "../../features/services/abstracts/theme-base.service";
import { ThemeService } from "../../features/services/concretes/theme.service";
import { AuthBaseService } from "../../features/services/abstracts/auth-base.service";
import { AuthService } from "../../features/services/concretes/auth.service";
import { LocalStorageBaseService } from "../../features/services/abstracts/local-storage-base.service";
import { LocalStorageService } from "../../features/services/concretes/local-storage.service";
import { TokenBaseService } from "../../features/services/abstracts/token-base.service";
import { TokenService } from "../../features/services/concretes/token.service";
import { BlogPostBaseService } from "../../features/services/abstracts/blog-post-base.service";
import { BlogPostService } from "../../features/services/concretes/blog-post.service";
import { CommentBaseService } from "../../features/services/abstracts/comment-base.service";
import { CommentService } from "../../features/services/concretes/comment.service";

export function getAppProviders() {
    const themeServiceProviders = {
        provide: ThemeBaseService,
        useClass: ThemeService
    };
    const authServiceProviders = {
        provide: AuthBaseService,
        useClass: AuthService
    };
    const localStorageServiceProviders = {
        provide: LocalStorageBaseService,
        useClass: LocalStorageService
    };
    const tokenServiceProviders = {
        provide: TokenBaseService,
        useClass: TokenService
    };
    const blogPostServiceProviders = {
        provide: BlogPostBaseService,
        useClass: BlogPostService
    };
    const commentServiceProviders = {
        provide: CommentBaseService,
        useClass: CommentService
    }

    return [
        themeServiceProviders,
        authServiceProviders,
        localStorageServiceProviders,
        tokenServiceProviders,
        blogPostServiceProviders,
        commentServiceProviders,

        provideRouter(routes),
        provideHttpClient(withInterceptors([
            AuthInterceptor,
            ErrorInterceptor
        ])),
        provideNoopAnimations()
    ]
}