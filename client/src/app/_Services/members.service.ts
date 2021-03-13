import { member } from './../_Model/member';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseURL = environment.apiUrl;
  members: member[] = [];


  constructor(private http: HttpClient) { }

  getMembers() {
    if (this.members.length > 0) return of(this.members);
    return this.http.get<member[]>(this.baseURL + 'Users/').pipe(
      map(mem => {
        this.members = mem;
        return mem;
      })
    );
  }

  getMember(username: string) {
    const mem = this.members.find(x => x.userName == username);
    if (mem !== undefined) return of(mem);
    return this.http.get<member>(this.baseURL + 'Users/' + username);
  }

  updateMember(mem: member) {
    return this.http.put(this.baseURL + 'users', mem).pipe(
      map(() => {
        const index = this.members.indexOf(mem);
        this.members[index] = mem;
      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseURL + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseURL + 'users/delete-photo/' + photoId);
  }
}
