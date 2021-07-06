import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  e;
  // we need to inject the router because for certain error we need to redirect the user to error page
  // for some error we will just display the toastr notification
  constructor(private router: Router, private toastr: ToastrService) {}
  // here we can either intercepts the request that goes out or response that comes back in the next
  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    // as it return the observable we need pipe method to modify before subscribr
    return next.handle(request).pipe(
      // for majority we will catch the error whats not catched we will return the error
      catchError((error) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                // we are throwing modal state errors back to the component as we need to display our error in the forms
                // if we have array of arrays we can flatten them using flat method
                throw modalStateErrors.flat();
              } else {
                // else for normal 400 errors
                this.toastr.error(error.statusText, error.status);
              }
              break;
            case 401:
              this.toastr.error(error.statusText, error.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              // we use navigate error as we need to show error info
              // we are adding the error to route state
              const navigationExtras: NavigationExtras = {
                state: { error: error.error },
              };
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }

        return throwError(error);
      })
    );
  }
}
