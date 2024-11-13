import { Injectable } from '@angular/core';
import { CanActivate, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { Toastrservice } from '../Services/toastr.service';

@Injectable({
  providedIn: 'root'
})
export class adminGuard implements CanActivate {
  constructor(
    private auth: AuthService,
    private router: Router,
    private toastservice: Toastrservice
  ) {}

  canActivate(): boolean {
    const role = localStorage.getItem('role');
    if (role === 'Admin') {
      return true;
    } else {
      this.toastservice.showError("ERROR", "Only Organizer can access this page!");
      this.router.navigate(['not-authorized']);
      return false;
    }
  }
}
