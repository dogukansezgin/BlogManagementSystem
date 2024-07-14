import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export abstract class LocalStorageBaseService {

    abstract set(key: string, data: string): void;
    abstract remove(key: string): void;
    abstract get(key: string): string | null;

    abstract setAsync(key: string, data: string): Observable<void>;
    abstract removeAsync(key: string): Observable<void>;
    abstract getAsync(key: string): Observable<string | null>;
}