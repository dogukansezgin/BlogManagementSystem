import { Inject, Injectable } from "@angular/core";
import { ThemeBaseService } from "../abstracts/theme-base.service";
import { DOCUMENT } from "@angular/common";
import { LocalStorageBaseService } from "../abstracts/local-storage-base.service";

@Injectable()
export class ThemeService extends ThemeBaseService {

    themes: Record<string, string> = {
        light: "aura-light-amber",
        dark: "aura-dark-amber"
    };

    constructor(
        @Inject(DOCUMENT) private document: Document,
        private localStorageService: LocalStorageBaseService
    ) { super(); }

    override initialTheme(): string {
        const preferredTheme = this.localStorageService.get('theme');
        if (preferredTheme) {
            let themeLink = this.document.getElementById('app-theme') as HTMLLinkElement;

            if (themeLink) {
                themeLink.href = this.themes[preferredTheme] + '.css';
            }
            return preferredTheme;
        }
        return 'light';
    }

    override switchTheme(theme: string): void {
        let themeLink = this.document.getElementById('app-theme') as HTMLLinkElement;

        if (themeLink) {
            themeLink.href = this.themes[theme] + '.css';

            this.localStorageService.set('theme', theme);
        }
    }
}