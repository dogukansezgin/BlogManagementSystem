import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserLoginRequest } from "../../models/requests/users/user-login-request";
import { UserRegisterRequest } from "../../models/requests/users/user-register-request";
import { UserLoginResponse } from "../../models/responses/users/user-login-response";
import { UserRegisterResponse } from "../../models/responses/users/user-register-response";

@Injectable({
    providedIn: 'root'
})
export abstract class AuthBaseService {

    abstract register(userRegisterRequest: UserRegisterRequest): Observable<UserRegisterResponse>;
    abstract login(userLoginRequest: UserLoginRequest): Observable<UserLoginResponse>;

    abstract isAuthenticated(): boolean;
    abstract hasRole(roles: string[]): boolean;
    abstract logOut(): void;
    abstract getCurrentUserId(): string | null;
}