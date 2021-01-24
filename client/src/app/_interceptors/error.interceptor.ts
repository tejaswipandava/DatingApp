import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toast: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateError = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateError.push(error.error.errors[key]);
                  }
                }
                throw modalStateError.flat();
              } else {
                this.toast.error(error.statusText, error.status);
              }
              break;

            case 401:
              this.toast.error(error.statusText, error.status);
              break;

            case 404:
              this.router.navigateByUrl('/not-found');
              break;

            case 500:
              const navigationExtras: NavigationExtras = { state: { error: error.error } }
              this.router.navigateByUrl('Server-Error', navigationExtras);
              break;

            default:
              this.toast.error("Something unexpected happened");
              console.log(error);
              break;
          }
        }

        return throwError(error);
      })
    );
  }
}
