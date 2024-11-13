import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllexpensesComponent } from './Components/allexpenses/allexpenses.component';
import { HomepageComponent } from './Components/dashboard/homepage/homepage.component';
import { PageNotFoundComponent } from './Components/dashboard/page-not-found/page-not-found.component';
import { ExpenseComponent } from './Components/expense/expense.component';
import { GroupComponent } from './Components/group/group.component';
import { LoginComponent } from './Components/login/login.component';
import { SignupComponent } from './Components/signup/signup.component';
import { UserBalanceComponent } from './Components/user-balance/user-balance.component';
import { adminGuard } from './guards/admin.guard';
import { authGuard } from './guards/auth.guard';



const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'signup',component:SignupComponent},
  {path:'home',component:HomepageComponent,canActivate: [authGuard]},
{path:'userDetails',component:UserBalanceComponent,canActivate: [authGuard]},
 {path:'group',component:GroupComponent,canActivate: [authGuard,adminGuard]},
 {path:'expense',component:ExpenseComponent,canActivate: [authGuard]},
 {path:'UserGroups',component:AllexpensesComponent,canActivate:[authGuard,adminGuard]},
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component:PageNotFoundComponent }
  
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
