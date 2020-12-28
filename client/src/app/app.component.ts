
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.getUsers();
  }

  title = 'client';
  users: any;

  getUsers()
  {
    this.http.get("https://localhost:5001/api/Users/getUsers")
    .subscribe(Response => {
      this.users = Response;
    }, error => console.log(error));
  }
  
}
