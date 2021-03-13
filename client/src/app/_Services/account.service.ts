import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_Model/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  baseURL = environment.apiUrl;

  login(model: any) {
    return this.http.post(this.baseURL + "account/login", model).pipe(
      map((response: User) => {
        const user = response;
        console.log(user);
        if (user) {
          this.setCurrentUser(user)
        }

        return user;
      }
      ))
  }

  register(model: any) {
    return this.http.post(this.baseURL + 'account/Register', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user)
        }

        return user;
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
}
