import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  // we will create observable to store our user in Replay subject is type of observable it is type of buffer object it will store values here any time subcriber subscribe to this observable its going to omit last value inside it
  // we wil specify how many values we are going to store in the buffer we will store only one user object or it will stor null
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          // we gona populate the user object we get back in local storage in our browser
          localStorage.setItem('user', JSON.stringify(user));
          // we will store the user in the buffer
          this.currentUserSource.next(user);
        }
      })
    );
  }
  // when user registers we will consider them logged in to our application
  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
