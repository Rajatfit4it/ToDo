//import { Injectable } from '@angular/core';
//import {
//  HttpRequest,
//  HttpHandler,
//  HttpEvent,
//  HttpInterceptor
//} from '@angular/common/http';
//import { AccountService } from "./services/account.service";
//import { Observable } from 'rxjs/Observable';
//@Injectable()
//export class TokenInterceptor implements HttpInterceptor {
//  constructor(public auth: AccountService) { }
//  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//    request = request.clone({
//      setHeaders: {
//        Authorization: `Bearer ${this.auth.IsAuthenticated()}`
//      }
//    });
//    return next.handle(request);
//  }
//}
//# sourceMappingURL=token.interceptor.js.map