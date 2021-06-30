import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  // if we dont declare a type then type script will be detect type based on value assigned
  registerMode = false;
  // users: any;
  constructor() {}

  // component initialization method
  ngOnInit(): void {
    // this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  // getUsers() {
  //   this.http.get('https://localhost:5001/api/users').subscribe(
  //     (response) => (this.users = response),
  //     (error) => console.log(error)
  //   );
  // }

  // we want to pass users form home componet to register component

  // getUsers() {
  //   this.http.get('https://localhost:5001/api/users').subscribe(
  //     (response) => {
  //       this.users = response;
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );
  // }
}
