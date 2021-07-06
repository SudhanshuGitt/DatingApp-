import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css'],
})
export class ServerErrorComponent implements OnInit {
  error: any;
  // we need to inject our router to have access to router states
  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    // we will check if it has the information
    this.error = navigation?.extras?.state?.error;
  }

  ngOnInit(): void {}
}
