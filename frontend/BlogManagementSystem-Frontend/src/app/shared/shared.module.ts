import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { NavbarComponent } from "./components/navbar/navbar.component";
import { MenubarModule } from "primeng/menubar";
import { TruncatePipe } from "./pipes/truncate-pipe";
import { FilterByAuthorPipe } from "./pipes/filter-by-author-pipe";
import { FilterByDatePipe } from "./pipes/filter-by-date-pipe";

@NgModule({
    declarations:[
        NavbarComponent,
        TruncatePipe,
        FilterByAuthorPipe,
        FilterByDatePipe
    ],
    exports:[
        NavbarComponent,
        TruncatePipe,
        FilterByAuthorPipe,
        FilterByDatePipe
    ],
    imports:[
        CommonModule,
        FormsModule,
        MenubarModule
    ]
})
export class SharedModule{}