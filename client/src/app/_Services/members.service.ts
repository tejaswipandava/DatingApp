import { member } from './../_Model/member';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseURL = environment.apiUrl;


  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<member[]>(this.baseURL + 'Users/');
  }

  getMember(username: string) {
    return this.http.get<member>(this.baseURL + 'Users/' + username);
  }
}
