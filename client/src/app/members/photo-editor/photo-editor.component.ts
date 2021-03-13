import { MembersService } from './../../_Services/members.service';
import { Photo } from './../../_Model/Photo';
import { AccountService } from './../../_Services/account.service';
import { environment } from 'src/environments/environment';
import { member } from './../../_Model/member';
import { Component, OnInit, Input } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { User } from 'src/app/_Model/User';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() member: member;
  uploader: FileUploader;
  hasBaseDropzoneOver: boolean = false;
  baseUrl = environment.apiUrl;
  user: User;


  constructor(private accountService: AccountService, private membersService: MembersService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(x => this.user = x);
  }

  ngOnInit(): void {
    this.InitializeUploader();
  }

  fileOverBase(event: any) {
    this.hasBaseDropzoneOver = event;
  }

  InitializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    //we are setting with with credentials as false because we are trying to 
    //bypass the CORS authentication policy because we ddint specify CORS credentials
    //Thats why token was sent with it
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }

    this.uploader.onSuccessItem = (item, response, status, header) => {
      if (response) {
        const photo = JSON.parse(response);
        this.member.photo.push(photo);
      }
    }
  }

  setMainPhoto(photo: Photo) {
    this.membersService.setMainPhoto(photo.id).subscribe(() => {
      this.user.photoUrl = photo.url;
      this.accountService.setCurrentUser(this.user);
      this.member.photoUrl = photo.url;

      this.member.photo.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) p.isMain = true;
      })
    });
  }

  deletePhoto(photoId: number) {
    this.membersService.deletePhoto(photoId).subscribe(() => {
      this.member.photo = this.member.photo.filter(x => x.id != photoId);
    });
  }
}
