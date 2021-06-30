import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  // currentUser$: Observable<User>;

  // if we want access service inside the template we need to make it public
  constructor(public accountService: AccountService) {}

  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    // login method is returning us observable and observable is lazy it will not do anything until we subscribe to observable
    this.accountService.login(this.model).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => console.log(error)
    );
  }

  logout() {
    this.accountService.logout();
  }

  // here we are subscribing our observable in account service and then setting logged in status based on current user
  // as it is not http request it will never complete so we need to unsunscribe it
  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe(
  //     (user) => {
  //       // !! will convert that to bollean if user is null then false else true
  //       this.loggedIn = !!user;
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );
  // }
}
