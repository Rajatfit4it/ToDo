import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Injectable()
export class AccountService  {

  constructor(private http: HttpClient,
    private router: Router) {
    var token = localStorage.getItem('userToken');
    if (token) {
      var usertoken = JSON.parse(token);
      this._userToken = { name: usertoken.name, token: usertoken.token, expiresAt: usertoken.expiresAt };
      if(!this.IsAuthenticated()) {
        this.logout();
      }
    }
  }


  private _userToken: UserToken = { name: "", token: "", expiresAt: 0 };

  IsAuthenticated(): boolean {
    return this._userToken.token && Date.now() < this._userToken.expiresAt;
  }

  Name(): string {
    return this._userToken.name;
  }

  AuthenticationToken(): string {
    return this._userToken.token;
  }

  registerUser(signUp) {
    return this.http.post('/api/users/register', signUp);
  }

  authenticateUser(login) {
    return this.http.post<UserToken>('/api/users/authenticate', login)
      //.map(res => res.json())
      .subscribe(res => {
        this._userToken.name = res.name;
        this._userToken.token = res.token;
        this._userToken.expiresAt = res.expiresAt + Date.now();
        localStorage.setItem("userToken", JSON.stringify(this._userToken));
        this.router.navigate(['/']);
      });
  }

  logout() {
    this._userToken = { name: "", token: "", expiresAt: 0 };
    localStorage.setItem("userToken", JSON.stringify(this._userToken));
  }

}


class UserToken {
  name: string;
  token: string;
  expiresAt: number;
}
