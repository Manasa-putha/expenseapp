import { Injectable } from '@angular/core';
import { CanActivate, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { Toastrservice } from '../Services/toastr.service';

// export const authGuard: CanActivateFn = (route, state) => {
//   return true;
// };

@Injectable({
  providedIn: 'root'
})
export class authGuard implements CanActivate {
  constructor(
    private auth : AuthService,
    private router: Router,
    private toastservice: Toastrservice){

  }
  canActivate():boolean{
    if(this.auth.isLoggedIn()){
      return true
    }else{
      this.toastservice.showError("ERROR", "Please Login First!");
      this.router.navigate(['login'])
      return false;
    }
  }
};
