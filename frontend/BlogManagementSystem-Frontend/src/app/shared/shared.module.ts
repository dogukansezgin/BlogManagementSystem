import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { NavbarComponent } from "./components/navbar/navbar.component";
import { MenubarModule } from "primeng/menubar";

@NgModule({
    declarations:[
        NavbarComponent
    ],
    exports:[
        NavbarComponent
    ],
    imports:[
        CommonModule,
        FormsModule,
        MenubarModule
    ]
})
export class SharedModule{}