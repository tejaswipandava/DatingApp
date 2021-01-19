import { AccountService } from './../_Services/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public account: AccountService) { }

  ngOnInit(): void {
  }

  model: any={};

  login()
  {
    this.account.login(this.model).subscribe(Response =>{
      console.log(Response);
    }, error =>{
      console.log(error);
    });
  }

  logout()
  {
    this.account.logout();
  }
}
