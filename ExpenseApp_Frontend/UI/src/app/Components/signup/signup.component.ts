import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/Services/auth.service';
import { Toastrservice } from 'src/app/Services/toastr.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent  implements OnInit {

  public signUpForm!: FormGroup;
  type: string = 'password';
  isText: boolean = false;
  eyeIcon:string = "fa-eye-slash"
  constructor(
    private fb : FormBuilder,
    private router: Router,
    private http:HttpClient,
    private auth:AuthService,
    private toastService:Toastrservice) { } 

  ngOnInit() {
    this.signUpForm = this.fb.group({
      firstName:['', Validators.required],
      lastName:['', Validators.required],
      userName:['', Validators.required],
      email:['', Validators.required],
      password:['', Validators.required]
    })
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = 'fa-eye' : this.eyeIcon = 'fa-eye-slash'
    this.isText ? this.type = 'text' : this.type = 'password'
  }

  onRegister() {
    if (this.signUpForm.valid) {
      console.log(this.signUpForm.value);
      let signUpObj = {
        ...this.signUpForm.value,
        role:'',
        token:''
      }
      this.auth.signUp(signUpObj)
      .subscribe({
        next:(res=>{
          console.log(res.message);
          this.toastService.showSuccess(res.message);
          this.signUpForm.reset();
          this.router.navigate(['login']);
          // alert(res.message)
    
        }),
        error:(err=>{
          // alert(err?.error.message)
          this.toastService.showError(err?.error.message);
        })
      })
    } else {
      ValidateForm.validateAllFormFields(this.signUpForm); 
      // console.log("form is not valid");
      //logic for throwing error
      // alert("your form is invalid");
      this.toastService.showWarning("your form is invalid");
    }
  }

// onSignUp(){
//   if(this.signUpForm.valid){
//     //perform logiv for signup
//     console.log(this.signUpForm.value);
//   }else{
//     console.log("form is not valid");
//     //logic for throwing error
//     ValidateForm.validateAllFormFields(this.signUpForm);
//     alert("your form is invalid")
//   }
// }
}
