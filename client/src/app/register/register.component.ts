import { AccountService } from './../_Services/account.service';
import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private account: AccountService) { }

  ngOnInit(): void {
  }

  model: any = {};
  @Output() cancelRegister = new EventEmitter();

  register() {
    this.account.register(this.model).subscribe(Response =>{
      console.log(Response);
      this.cancel();
    }, error=>console.log(error));
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
