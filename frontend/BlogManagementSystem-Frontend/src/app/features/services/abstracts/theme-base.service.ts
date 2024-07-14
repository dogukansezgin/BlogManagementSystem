import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export abstract class ThemeBaseService {
    
    abstract initialTheme(): string;
    abstract switchTheme(theme: string): void;
}