import { Component, OnInit } from '@angular/core';
import { SignUp } from '../../models/signup';
import { AccountService} from "../../services/account.service";


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  response: any;
  confirmPassword: string;

  signUpModel: SignUp = {
    name: "",
    userName: "",
    password: "",
    email: ""
  };

  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

  submit() {
    if (this.validate()) {
      this.accountService.registerUser(this.signUpModel)
        .subscribe(res => {
          this.response = res.message;
        });
    } else {
      this.response = "Password and Confirm password doesn't match.";
    }
    return;
  }

  validate(): boolean {
    if (this.signUpModel.password === this.confirmPassword)
      return true;
    return false;
  }

}
