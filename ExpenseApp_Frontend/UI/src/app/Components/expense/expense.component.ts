import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Expense, Group, Split } from 'src/app/models/expense.model';
import { GroupService } from 'src/app/Services/group.service';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.css']
})
export class ExpenseComponent implements OnInit {
  @Input() group!: Group;
  @Output() expenseAdded = new EventEmitter<void>();
  expenseForm: FormGroup;
  minDate: Date;
  groups: any[] = [];
  selectedGroup: any;
 isAdmin!: false;
  constructor(private fb: FormBuilder, private groupService: GroupService,private route:Router) {
    this.expenseForm = this.fb.group({
      description: ['', Validators.required],
      amount: [0, [Validators.required, Validators.min(1)]],
      paidBy: ['', Validators.required],
      date: [new Date(), Validators.required],
      splitAmong: [[], Validators.required],
      
    });
    this.minDate = new Date();
  }

  ngOnInit(): void {
    this.loadGroups();
  }

  
  loadGroups() {
    this.groupService.getAllGroups().subscribe({
      next: (groups) => {
        this.groups = groups;
      },
      error: (error) => {
        console.error('Error loading groups:', error);
      }
    });
  }

  addExpense() {
    if (this.expenseForm.valid && this.selectedGroup) {
      const expenseData: any = {
        description: this.expenseForm.value.description,
        amount: this.expenseForm.value.amount,
        paidById: this.expenseForm.value.paidBy, 
        date: this.expenseForm.value.date,
        groupId: this.selectedGroup.id,
        splitAmong: this.expenseForm.value.splitAmong.map((memberId: number) => ({
          memberId: memberId,
          amountOwed: this.calculateAmountOwed(),
        })),
        isSettled: false,
        id: 0
      };
    
      this.groupService.addExpense(this.selectedGroup.id, expenseData).subscribe({
        next: (response) => {
          alert('Expense added successfully!');
          this.loadGroupExpenses(); 
          this.expenseForm.reset({
            amount: 0,
            date: new Date(),
            splitAmong: []
          });
          this.route.navigate(['home']);
        },
        error: (error) => {
          console.error('Error adding expense:', error);
          alert('Failed to add expense. Please try again.');
        }
      });
    }
  }
  
 // Example calculation for amount owed; replace with your logic
 private calculateAmountOwed(): number {
  const totalAmount = this.expenseForm.value.amount;
  const numberOfUsers = this.expenseForm.value.splitAmong.length;
  return numberOfUsers > 0 ? totalAmount / numberOfUsers : 0;  // Evenly split the amount
}
loadGroupExpenses() {
  if (this.selectedGroup) {
    this.groupService.getExpensesByGroup(this.selectedGroup.id).subscribe({
      next: (expenses) => {
        this.selectedGroup.expenses = expenses;  // Update the group's expenses
      },
      error: (error) => {
        console.error('Error loading expenses:', error);
      }
    });
  }
}

getMemberNameById(memberId: number): string {
  const member = this.selectedGroup?.members.find((m: { id: number; }) => m.id === memberId);
  return member ? member.name : 'Unknown';
}

markAsSettled(expenseId: number) {
  if (this.selectedGroup) {
    this.groupService.settleExpense(this.selectedGroup.id, expenseId).subscribe(
      () => {
        // Reload the group 
        this.loadGroups();
      },
      (error) => {
        console.error('Error settling expense', error);
      }
    );
  }
}

calculateGroupBalances() {
  const balances: { [memberId: number]: number } = {};
  if (this.selectedGroup?.expenses?.length) {
    this.selectedGroup.expenses.forEach((expense: Expense)=> {
      expense.splitAmong.forEach((split: Split) => { 
        if (!balances[split.memberId]) {
          balances[split.memberId] = 0;
        }
     
        balances[split.memberId] -= split.amountOwed;
      });

      
      if (!balances[expense.paidById]) {
        balances[expense.paidById] = 0;
      }
      balances[expense.paidById] += expense.amount;
    });
  }
  return balances;
}
}
