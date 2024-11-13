import { ChangeDetectorRef, Component } from '@angular/core';
import { Split } from 'src/app/models/expense.model';
import { AuthService } from 'src/app/Services/auth.service';
import { GroupService } from 'src/app/Services/group.service';

@Component({
  selector: 'app-user-balance',
  templateUrl: './user-balance.component.html',
  styleUrls: ['./user-balance.component.css']
})
export class UserBalanceComponent {
  userGroups: any[] = [];
  userId!: number; 

  constructor(private groupService: GroupService,private authService:AuthService,private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.userId = this.authService.getCurrentUserId(); 
    this.getUserGroups();
  }

  getUserGroups() {
    this.groupService.getUserGroups(this.userId).subscribe(
      (groups) => {
        this.userGroups = groups;
      },
      (error) => {
        console.error('Error fetching user groups', error);
      }
    );
  }

  // Settle expense for a group
  settleExpense(groupId: number, expenseId: number) {
    this.groupService.settleExpense(groupId, expenseId).subscribe(
      (response) => {
        console.log('Expense settled successfully', response);
         
      const group = this.userGroups.find(g => g.id === groupId);
      if (group) {
        const expense = group.expenses.find((e: { id: number; }) => e.id === expenseId);
        if (expense) {
          expense.isSettled = true; 
         group.groupUsers.forEach((user: any) => {
          if (expense.paidById === user.id) {
            
            expense.splitAmong.forEach((split: { memberId: number, amountOwed: number }) => {
              if (split.memberId !== user.id) {
                user.balance -= split.amountOwed;  
              }
            });
          } else {
            
            const split = expense.splitAmong.find((s: { memberId: number }) => s.memberId === user.id);
            if (split) {
              user.balance -= split.amountOwed;  
            }
          }
        });
        this.cdr.detectChanges();
        this.getUserGroups();
        }
      }
   
      },
      (error) => {
        console.error('Error settling expense', error);
        alert('Error settling expense');
      }
    );
  }
   
   calculateTotalExpenses(expenses: any[]): number {
    return expenses.reduce((total, expense) => total + expense.amount, 0);
  }
  totalAmountOwedToPayer(splitAmong: any[], payerId: number): number {
    return splitAmong
      .filter(split => split.memberId !== payerId)
      .reduce((total, split) => total + split.amountOwed, 0); 
  }
  getUserName(memberId: number): string {
    const user = this.userGroups.flatMap(group => group.groupUsers).find(user => user.id === memberId);
    return user ? user.name : 'Unknown';
  }
    

  // Calculate total amount owed by users
  calculateTotalOwed(expenses: any[], groupUsers: any[]): number {
    let totalOwed = 0;

    // expenses.forEach(expense => {
    //   if (!expense.isSettled) { 
    //   expense.splitAmong.forEach((split: Split) => {
    //     const user = groupUsers.find(user => user.id === split.memberId);
    //     if (user) {
    //       totalOwed += split.amountOwed;
    //     }
      
    //   });
    // }
    // });

    // return totalOwed;
    expenses.forEach(expense => {
      if (!expense.isSettled) {
      
        const splitForUser = expense.splitAmong.find((split: Split) => split.memberId === this.userId);
        
        if (splitForUser) {
          totalOwed += splitForUser.amountOwed; 
        }
      }
    });
  
    return totalOwed;
  }
   // Calculate the total amount the current user has lent (i.e., others owe them)
   calculateUserLent(expenses: any[]): number {
    let totalLent = 0;

    expenses.forEach(expense => {
      if (!expense.isSettled && expense.paidById === this.userId) {  
        expense.splitAmong.forEach((split: { memberId: number, amountOwed: number }) => {
          if (split.memberId !== this.userId) {
            totalLent += split.amountOwed;  
          }
          // expense.splitAmong.forEach((split: Split) => {
          //   if (split.memberId === this.userId) {  // Current user owes this amount
          //     totalOwed += split.amountOwed;
          //   }
          // });
        });
      }
    });

    return totalLent;
  }
}

