import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-allexpenses',
  templateUrl: './allexpenses.component.html',
  styleUrls: ['./allexpenses.component.css']
})
export class AllexpensesComponent implements OnInit{
  users: any[] = [];  
  displayedColumns: string[] = ['id', 'name', 'email', 'actions']; 
  editUserForm!: FormGroup;  
  selectedUser: any = null;  
  constructor(
    private apiService: AuthService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadUsers();  

    this.editUserForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }


  loadUsers() {
    this.apiService.getUsers().subscribe(users => {
      this.users = users;
    }, error => {
      console.error('Failed to load users', error);
    });
  }

  
  editUser(user: any) {
    this.selectedUser = user;
    this.editUserForm.patchValue({
      name: user.name,
      email: user.email
    });
  }


  onSubmit() {
    if (this.editUserForm.valid && this.selectedUser) {
      const updatedUser = this.editUserForm.value;
      this.apiService.updateUser(this.selectedUser.id, updatedUser).subscribe(response => {
        this.snackBar.open('User updated successfully', 'Close', { duration: 2000 });
        this.loadUsers();  
        this.selectedUser = null;  
      }, error => {
        console.error('Failed to update user', error);
        this.snackBar.open('Failed to update user', 'Close', { duration: 2000 });
      });
    }
  }


  deleteUser(userId: string) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.apiService.deleteUser(userId).subscribe(response => {
        this.snackBar.open('User deleted successfully', 'Close', { duration: 2000 });
        this.loadUsers();  
      }, error => {
        console.error('Failed to delete user', error);
        this.snackBar.open('Failed to delete user', 'Close', { duration: 2000 });
      });
    }
  }
}
