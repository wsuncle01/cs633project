import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginComponent } from '../user/login/login.component';
import { SearchService } from './../shared/services/search.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  ngOnInit(): void {
    this.service.getPolls()
      .subscribe((items: any) => {
        this.polls = items.map(item => {
          return {
            title: item.question_text,
            id: item.id,
            role: item.role,
            owner_id: item.owner_id,
            pub_date: item.pub_date,
          };
        });
      });
  }
  polls: any[] = [];
  question;
  visited;
  constructor(private router:Router, private service: SearchService, private toastr: ToastrService) {}


  displayQuestion(id){
    this.service.getQuestion(id).subscribe(
      res => {this.question = res;
        this.service.hasAnswered(id).subscribe(res => {this.visited = res});
        this.router.navigate(['/home/question'], { 
          state: this.question
        });
      
      },
      err => {this.toastr.error("Please log in first");;},
    );
  }
}