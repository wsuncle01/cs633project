import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SearchService } from './shared/services/search.service';
import { LoginComponent } from './user/login/login.component';
import {MatDialog} from '@angular/material/dialog';
import { RegistrationComponent } from './user/registration/registration.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  polls: any[] = [];
  isUserLoggedIn: boolean;

  constructor(private router:Router, private service: SearchService,public dialog: MatDialog) {
  }
  onLogin(){
    this.router.navigate(['/user/login']);
  }
  


  ngOnInit(): void {
    if (localStorage.getItem('token') != null){
      this.isUserLoggedIn = true;
    }
    else this.isUserLoggedIn = false;
}
  onLogout(){
    localStorage.removeItem('token');
    this.isUserLoggedIn = false;
    this.router.navigate(['/home']);
    window.location.reload();
  }


  create(){
    this.router.navigate(['/home/create']);

  }

  yourPolls(){
    this.router.navigate(['/home/yourpoll']);
  }

  
 HomePage() {
   window.location.assign('/home');



}


openlogin() {
  const dialogRef = this.dialog.open(LoginComponent,{
    // width:'30%',
  });

  dialogRef.afterClosed().subscribe(result => {
    console.log(`Dialog result: ${result}`);
  });
}

openSignup() {
  const dialogRef = this.dialog.open(RegistrationComponent,{
    panelClass : 'signup',
    
  })

  dialogRef.afterClosed().subscribe(result => {
    console.log(`Dialog result: ${result}`);
  });
}


}


