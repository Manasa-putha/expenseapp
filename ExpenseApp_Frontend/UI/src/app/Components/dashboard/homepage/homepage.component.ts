import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Group, GroupUser } from 'src/app/models/expense.model';
import { GroupService } from 'src/app/Services/group.service';


@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent  implements OnInit {
  groups: any[] = [];
  isAdmin: boolean = false; 

  constructor(private groupService: GroupService) {}

  ngOnInit(): void {
    this.loadGroups();

    this.isAdmin = this.checkIfAdmin();
  }

  // Fetch groups from the service
  loadGroups() {
    this.groupService.getAllGroups().subscribe({
      next: (groups) => {
        this.groups = groups;
      },
      error: (error) => {
        console.error('Error fetching groups:', error);
        
      }
    });
  }

 
  checkIfAdmin(): boolean {

    return true; 
  }

  // Edit group
  editGroup(group: any) {
   
  }

  // // Delete group 
  // deleteGroup(groupId: string) {
  //   console.log('Attempting to delete group with id:', groupId); 
  //   this.groupService.deleteGroup(groupId).subscribe({
  //     next: (response) => {
  //       console.log('Group deletion succeeded. Response:', response); 
  //       alert('Group deleted successfully!');
  //       this.loadGroups(); 
  //     },
  //     error: (error) => {
  //       console.error('Error during group deletion:', error);
  //       alert('Failed to delete group. Please try again.');
  //     },
  //     complete: () => {
  //       console.log('Group deletion request completed'); 
  //     }
  //   });
  // }
  deleteGroup(groupId: number) {
    console.log('Attempting to delete group with id:', groupId);
  
    this.groupService.deleteGroup(groupId).subscribe({
      next: (response) => {
        console.log('Group deletion succeeded. Response:', response); // Log the response to check if it's correct
  
        // Optional: Remove the group from UI
        this.groups = this.groups.filter(group => group.id !== groupId);
  
        // Show success message
        alert('Group deleted successfully!');
  
        // Reload groups after deletion
        this.loadGroups(); 
      },
      error: (error) => {
        // This part should be executed only if there's an actual error
        console.error('Error during group deletion:', error);
  
        // Show error message
        alert('Failed to delete group. Please try again.');
      },
      complete: () => {
        console.log('Group deletion request completed');
      }
    });
  }
  
  
  settleExpense(groupId: number, expenseId: number) {
    this.groupService.settleExpense(groupId, expenseId).subscribe({
      next: () => {
        alert('Expense settled successfully');
        this.loadGroups();
      },
      error: (error) => console.error('Error settling expense:', error)
    });
  }

  getMemberName(group: Group, memberId: string): string {
    const member = group.GroupUser.find(m => m.id === memberId);
    return member ? member.name : 'Unknown Member';
  }

getAmountsOwed(group: Group, member: GroupUser): {to: string, amount: number}[] {
  const owedAmounts: {to: string, amount: number}[] = [];
  
  group.GroupUser.forEach(otherMember => {
    if (otherMember.id !== member.id) {
      const amount = this.calculateAmountOwed(group, member, otherMember);
      if (amount > 0) {
        owedAmounts.push({to: otherMember.name, amount});
      }
    }
  });
  
  return owedAmounts;
}

private calculateAmountOwed(group: Group, from: GroupUser, to:GroupUser): number {

  const expenses = group.expenses || [];
  
  return expenses.reduce((total, expense) => {
    if (expense.paidById === to.id) {
      const split = expense.splitAmong.find((s: { memberId: string; }) => s.memberId === from.id);
      if (split) {
        total += split.amountOwed;
      }
    }
    return total;
  }, 0);
}

}