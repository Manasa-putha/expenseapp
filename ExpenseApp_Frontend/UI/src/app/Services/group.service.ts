import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Balance, Expense, Group } from '../models/expense.model';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private apiUrl = 'https://localhost:7144/api/Group';

  constructor(private http: HttpClient) {}

  // Fetch all groups
  getAllGroups(): Observable<Group[]> {
    return this.http.get<Group[]>(`${this.apiUrl}`);
  }

  // Create a new group
  createGroup(group: Group): Observable<Group> {
    return this.http.post<Group>(`${this.apiUrl}`, group);
  }

  // Get group by ID (with expenses)
  // getGroupById(groupId: string): Observable<GroupWithExpenses> {
  //   return this.http.get<GroupWithExpenses>(`${this.apiUrl}/${groupId}`);
  // }

  // Delete a group by ID
  deleteGroup(groupId: number): Observable<any> {
    return this.http.delete<void>(`${this.apiUrl}/${groupId}`,{ responseType: 'text' as 'json'  });
  }

  // Fetch all expenses for a given group
  getExpensesByGroup(groupId: string): Observable<Expense[]> {
    return this.http.get<Expense[]>(`${this.apiUrl}/group/${groupId}`);
  }

  // Add a new expense to a group
 
  addExpense(groupId: number, expense: Expense): Observable<Expense> {
    return this.http.post<Expense>(`${this.apiUrl}/${groupId}/add-expense`, expense);
  }
  
  // Mark an expense as settled
  
  settleExpense(groupId: number, expenseId: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${groupId}/settle-expense/${expenseId}`, {});
  }


  // Fetch user balances within a group
  getGroupBalances(groupId: string): Observable<Balance[]> {
    return this.http.get<Balance[]>(`${this.apiUrl}/group/${groupId}/balances`);
  }
    // Fetch user groups dynamically
    getUserGroups(userId: number): Observable<any[]> {
      return this.http.get<any[]>(`${this.apiUrl}/group/${userId}/groups`);
    }
}
