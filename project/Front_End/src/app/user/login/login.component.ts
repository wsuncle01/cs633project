import { ToastrService } from 'ngx-toastr';
import { SearchService } from './../../shared/services/search.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  formModel = {
    email: '',
    password: ''
  }
  constructor(private service: SearchService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/home');
    }
  }

  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.access_token);
        this.router.navigate(['/home']);
        window.location.reload();
        this.toastr.success('Login successful!');    
      },
      err => {
          this.toastr.error('Incorrect email or password.', 'Authentication failed.');
          console.log(err);
      }
    );
  }
}
