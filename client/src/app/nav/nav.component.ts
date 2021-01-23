import { AccountService } from './../_Services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public account: AccountService, private router:Router, private toastr: ToastrService ) { }

  ngOnInit(): void {
  }

  model: any={};

  login()
  {
    this.account.login(this.model).subscribe(Response =>{
      console.log(Response);
      this.router.navigateByUrl('/member');
    }, error =>{
      console.log(error);
      this.toastr.error(error.error);
    });
  }

  logout()
  {
    this.account.logout();
    this.router.navigateByUrl('/');
  }
}
