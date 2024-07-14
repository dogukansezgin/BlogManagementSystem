import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { LocalStorageBaseService } from "../abstracts/local-storage-base.service";

@Injectable()
export class LocalStorageService extends LocalStorageBaseService {

    constructor() { super(); }

    override set(key: string, data: string): void {
        localStorage.setItem(key, data);
    }
    override remove(key: string): void {
        localStorage.removeItem(key);
    }
    override get(key: string): string | null {
        return localStorage.getItem(key);
    }

    override setAsync(key: string, data: string): Observable<void> {
        localStorage.setItem(key, data);
        return of(undefined);
    }
    override removeAsync(key: string): Observable<void> {
        localStorage.removeItem(key);
        return of(undefined);
    }
    override getAsync(key: string): Observable<string | null> {
        const data = localStorage.getItem(key);
        return of(data);
    }
}