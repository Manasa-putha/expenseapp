
export interface User
{
    id: number;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    mobileNumber: string;
    password: string;
    userType: UserType;
    createdOn: string;
    token:number;
  
  
}
export enum UserType
{
    NONE, ADMIN, USER
}



  
  export interface Balance {
    user: string;
    balance: number;
  }
  
  export interface Group {
    id: number;
    name: string;
    description: string;
    createdDate: string;
    GroupUser: GroupUser[];
    expenses: any[] | null;
  }
  export interface UserExpense {
    userId: number;
    amountOwed: number;
    isSettled: boolean;
  }
  export interface GroupUser {
    id: string;
    name: string;
    email: string;
    balance?: number;
  }
 

export interface Expense {
  id: number;
  description: string;
  amount: number;
  paidById: number;
  paidByName: string;
  date: Date;
  splitAmong: Split[];
  isSettled: boolean;
}
  export interface Split {
    memberId: number;
    amountOwed: number;
  }
  