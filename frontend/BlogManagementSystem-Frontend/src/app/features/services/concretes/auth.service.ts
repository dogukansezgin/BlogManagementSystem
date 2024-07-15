import { Injectable } from "@angular/core";
import { AuthBaseService } from "../abstracts/auth-base.service";
import { catchError, map, Observable } from "rxjs";
import { UserLoginRequest } from "../../models/requests/users/user-login-request";
import { UserRegisterRequest } from "../../models/requests/users/user-register-request";
import { UserLoginResponse } from "../../models/responses/users/user-login-response";
import { UserRegisterResponse } from "../../models/responses/users/user-register-response";
import { HttpClient } from "@angular/common/http";
import { LocalStorageBaseService } from "../abstracts/local-storage-base.service";
import { TokenBaseService } from "../abstracts/token-base.service";
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from "../../../../environments/environment";

@Injectable()
export class AuthService extends AuthBaseService {

    private jwtHelper: JwtHelperService;

    private readonly apiUrl_Register: string = environment.apiUrl + environment.endpoints.auth.register;
    private readonly apiUrl_Login: string = environment.apiUrl + environment.endpoints.auth.login;

    constructor(
        private httpClient: HttpClient,
        private localStorageService: LocalStorageBaseService,
        private tokenService: TokenBaseService,
    ) {
        super();
        this.jwtHelper = new JwtHelperService();
    }

    override register(userRegisterRequest: UserRegisterRequest): Observable<UserRegisterResponse> {
        return this.httpClient.post<UserRegisterResponse>(this.apiUrl_Register, userRegisterRequest);
    }

    override login(userLoginRequest: UserLoginRequest): Observable<UserLoginResponse> {
        return this.httpClient.post<UserLoginResponse>(this.apiUrl_Login, userLoginRequest).pipe(map(response => {
            this.localStorageService.set('token', response.accessToken.token);
            return response;
        }, catchError(responseError => {
            throw responseError;
        })));
    }

    isAuthenticated(): boolean {
        const token = this.tokenService.getToken();
        if (token) {
            let isExpired = this.jwtHelper.isTokenExpired(token);
            if (isExpired) {
                this.localStorageService.remove('token');
            }
            return !isExpired;
        } else {
            return false;
        }
    }

    hasRole(roles: string[]): boolean {
        const userRoles = this.tokenService.getUserRoles();
        if (!roles || !userRoles) {
            return false;
        }
        for (const role of roles) {
            if (userRoles.includes(role)) {
                return true;
            }
        }
        return false;
    }

    logOut(): void {
        this.localStorageService.remove('token');
        setTimeout(() => {
            window.location.reload();
        }, 500);
    }

    getCurrentUserId(): string | null {
        return this.tokenService.getCurrentUserId();
    } 
}