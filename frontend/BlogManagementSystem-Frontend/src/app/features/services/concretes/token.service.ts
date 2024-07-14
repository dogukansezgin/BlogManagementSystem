import { Injectable } from "@angular/core";
import { TokenBaseService } from "../abstracts/token-base.service";
import { LocalStorageBaseService } from "../abstracts/local-storage-base.service";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class TokenService extends TokenBaseService {

    private jwtHelper: JwtHelperService;

    constructor(private localStorageService: LocalStorageBaseService) {
        super();
        this.jwtHelper = new JwtHelperService();
    }

    override getToken(): string | null {
        return this.localStorageService.get('token');
    }

    override getDecodedToken(): string | null {
        const token = this.getToken();
        if (token != null) {
            return this.jwtHelper.decodeToken(token);
        }
        return null;
    }

    override getCurrentUserId(): string | null {
        var decoded: any = this.getDecodedToken();
        if (decoded != null) {
            var propUserId = Object.keys(decoded).filter(x => x.endsWith("/nameidentifier"))[0]
            var userId = decoded[propUserId]
            return userId;
        }
        return null;
    }

    override getCurrentEmailAddress(): string {
        var decoded: any = this.getDecodedToken();
        if (decoded != null) {
            var propUserEmail = Object.keys(decoded).filter(x => x.endsWith("/emailaddress"))[0]
            var userEmail = decoded[propUserEmail]
            return userEmail;
        }
        return '';
    }

    override getUserRoles(): string[] {
        const token = this.getToken();
        if (token) {
            const decodedToken = this.jwtHelper.decodeToken(token);
            return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        }
        return [];
    }
}