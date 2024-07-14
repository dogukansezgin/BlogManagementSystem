import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export abstract class TokenBaseService {

    abstract getToken(): string | null;
    abstract getDecodedToken(): string | null;
    abstract getCurrentUserId(): string | null;
    abstract getCurrentEmailAddress(): string;
    abstract getUserRoles(): string[];
}