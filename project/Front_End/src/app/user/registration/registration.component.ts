import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SearchService } from 'src/app/shared/services/search.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {
  isSubmitted = false;
  Auth: any = [0, 1, 2];
  authType = this.service.formModel.value.Auth;

  constructor(private router: Router, public service: SearchService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.formModel.reset();
  }

  changeAuth(e: any) {
    this.service.formModel.value.Auth?.setValue(e.target.value, {
      onlySelf: true,
    });
  }

  onSubmit() {
    this.service.register().subscribe(
      (res: any) => {
        localStorage.setItem('token', res.access_token);
        this.toastr.success('New user created!', 'Registration successful.');        
        this.router.navigate(['/home']);
        window.location.reload();
      },
      err => {
        this.toastr.error(err);
      }
    );
  }

}
