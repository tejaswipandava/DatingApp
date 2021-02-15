import { MembersService } from './../../_Services/members.service';
import { member } from './../../_Model/member';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members: member[];

  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers()
  {
    this.membersService.getMembers().subscribe(Response => {
      this.members = Response;
    });
  }

}
