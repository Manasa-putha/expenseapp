import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
export interface NavigationItem  {
  value: string;
  link: string;
}
@Component({
  selector: 'app-page-side-nav',
  templateUrl: './page-side-nav.component.html',
  styleUrls: ['./page-side-nav.component.css']
})
export class PageSideNavComponent implements OnInit {
  panelName: string = '';
  navItems: NavigationItem[] = [];

  constructor(private authService: AuthService, private router: Router) {
  }
  ngOnInit() {
    this.checkAuthenticationStatus();

    this.authService.userStatus.subscribe(status => {
      this.updateNavigation(status);
    });
  }

  private checkAuthenticationStatus() {
    if (this.authService.isLoggedIn()) {
      const role = localStorage.getItem('role');
      this.updateNavigation('loggedIn');
    } else {
      this.updateNavigation('loggedOff');
    }
  }

  private updateNavigation(status: string) {
    if (status === 'loggedIn') {
      const role = localStorage.getItem('role');
      if (role === 'Admin') {
        this.panelName = 'Admin Panel';
        this.navItems = [
          { value: 'Expense Groups', link: '/home' },
          { value: 'Add New Group', link: '/group' },
          { value: 'Add New Expense', link: '/expense' },
          { value: 'All Users', link: '/UserGroups' },
          // { value: 'All Expenses', link: '/allexpenses' },
          // { value: 'Register User', link: '/signup' },
        ];
      } else if (role === 'User') {
        this.panelName = 'User Panel';
        this.navItems = [
          { value: 'Expense Groups', link: '/home' },
          { value: 'Add New Expense', link: '/expense' },
          { value: 'Amount Balances', link: '/userDetails' },
        ];
      }
    } else if (status === 'loggedOff') {
      this.panelName = 'Auth Panel';
      this.router.navigateByUrl('/login');
      this.navItems = [];
    }
}

}


