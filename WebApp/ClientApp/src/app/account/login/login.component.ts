import { Component, OnInit } from '@angular/core';
import { Login } from '../../models/login';
import { AccountService } from "../../services/account.service";
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login = {
    userName: "",
    password: ""
  }
  response: string;

  constructor(private accountService: AccountService,
    private router: Router) { }

  ngOnInit() {
  }

  submit() {
    this.accountService.authenticateUser(this.login);
    return;
  }

  

}
