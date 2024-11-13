
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { GroupService } from 'src/app/Services/group.service';
@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  @Output() groupCreated = new EventEmitter<void>();
  groupForm: FormGroup;
  members: { email: string, userId: number }[] = []; // Updated members array with userId
  isEmailInvalid = false;
  minDate!: Date;
  constructor(private fb: FormBuilder, private groupService: GroupService,private router:Router) {
    this.groupForm = this.fb.group({
      // groupId: ['', Validators.required],
      name: ['', Validators.required],
      description: [''],
      createdDate: [new Date(), Validators.required],
      // expenses: [null, [Validators.required, Validators.min(0)]],
    });
    this.minDate = new Date();
  }

  ngOnInit(): void {}

  // Add member to the members array with validation
  addMember(email: string) {
    this.isEmailInvalid = false; // Reset error state
    if (this.members.length < 10 && this.isValidEmail(email) && !this.members.find(m => m.email === email)) {
      const userId = this.generateUserId(email); // Generate or fetch the userId
      this.members.push({ email, userId });
    } else if (!this.isValidEmail(email)) {
      this.isEmailInvalid = true; // Set error state
    } else if (this.members.length >= 10) {
      alert('Maximum 10 members are allowed.');
    }
  }

  // Validate email format
  isValidEmail(email: string): boolean {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
  }

  // Remove member from the members array
  removeMember(email: string) {
    this.members = this.members.filter(member => member.email !== email);
  }

  // Generate a userId based on email (mock example)
  generateUserId(email: string): number {
    return Math.floor(Math.random() * 100000); // Mock userId generation
  }

  // // Create a new expense group
  // createGroup() {
  //   if (this.groupForm.valid && this.members.length > 0) {
  //     const newGroup = {
  //       ...this.groupForm.value,
  //       groupUsers: this.members.map(member => ({ userId: member.userId, email: member.email }))
  //     };
  //     this.groupService.createGroup(newGroup).subscribe({
  //       next: () => {
  //         alert('Group created successfully!');
  //         this.groupForm.reset();
  //         this.members = [];
  //         this.groupCreated.emit();
  //       },
  //       error: (error) => {
  //         console.error('Error creating group:', error);
  //         alert('Failed to create group. Please try again.');
  //       }
  //     });
  //   }
  // }
  createGroup() {
    if (this.groupForm.valid && this.members.length > 0) {
      const newGroup = {
        ...this.groupForm.value,
        groupUsers: this.members.map(member => ({ email: member.email })) // Pass only email
      };
  
      this.groupService.createGroup(newGroup).subscribe({
        next: (response) => {
          alert('Group created successfully!');
          this.groupForm.reset();
          this.members = [];
          this.groupCreated.emit();
          this.router.navigate(['/home']);
        },
        error: (error) => {
          console.error('Error creating group:', error);
          const errorMessage = error.error || 'Failed to create group. Please try again.';
          alert(errorMessage+'Failed to create group. Please try again.');
          this.members = [];
        }
      });
    } else {
      alert('Please fill the form correctly and select at least one member.');
    }
  }
  
}
