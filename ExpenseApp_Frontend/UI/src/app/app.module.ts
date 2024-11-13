import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { HomepageComponent } from './Components/dashboard/homepage/homepage.component';
import { PageFooterComponent } from './Components/dashboard/page-footer/page-footer.component';
import { PageHeaderComponent } from './Components/dashboard/page-header/page-header.component';
import { PageNotFoundComponent } from './Components/dashboard/page-not-found/page-not-found.component';
import { PageSideNavComponent } from './Components/dashboard/page-side-nav/page-side-nav.component';
import { PageTableComponent } from './Components/dashboard/page-table/page-table.component';
import { MaterialModule } from './material/material.module';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { provideToastr, ToastrModule } from 'ngx-toastr';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { GroupComponent } from './Components/group/group.component';
import { ExpenseComponent } from './Components/expense/expense.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { SignupComponent } from './Components/signup/signup.component';
import { ViewusersComponent } from './Components/viewusers/viewusers.component';
import { AllexpensesComponent } from './Components/allexpenses/allexpenses.component';
import { UserBalanceComponent } from './Components/user-balance/user-balance.component';
import { JwtHelperService } from '@auth0/angular-jwt';
// import { TokenInterceptor } from './interceptors/token.interceptor';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomepageComponent,
    PageFooterComponent,
    PageNotFoundComponent,
    PageHeaderComponent,
    PageSideNavComponent,
    PageTableComponent,
    GroupComponent,
    ExpenseComponent,
    SignupComponent,
    ViewusersComponent,
    AllexpensesComponent,
    UserBalanceComponent
    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MaterialModule,
    HttpClientModule,
    ToastrModule.forRoot() ,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  providers: [ ],
  bootstrap: [AppComponent]
})
export class AppModule { }
