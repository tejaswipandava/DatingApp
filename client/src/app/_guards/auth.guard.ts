import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../_Services/account.service';
import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private account:AccountService, private toastr:ToastrService){}

  canActivate(): Observable<boolean> {
    return this.account.currentUser$.pipe(
      map(user =>{
        if(user) return true;
        this.toastr.error("Consider Logging in");
      })
    )
  }
  
}
