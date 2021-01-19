import { AccountService } from './_Services/account.service';
import { User } from './_Model/User';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  constructor(private account:AccountService){}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  title = 'client';
  users: any;

  setCurrentUser()
  {
    const user:User = JSON.parse(localStorage.getItem('user'));
    this.account.setCurrentUser(user);
  }
  
}
