import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SearchService } from '../shared/services/search.service';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {

  question;
  hasAnswered;

  constructor(private service: SearchService, private router:Router) {this.question = this.router.getCurrentNavigation().extras.state;}

  ngOnInit(): void {
    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/home');
    }
  }

  displayResult(id){
    this.service.makeVote(id).subscribe(res => {this.router.navigate(['/home/result'], { 
      state: { id: this.question.id } 
    });}, err => {console.log(err);},);
  }

  result(){
    this.router.navigate(['/home/result'], { 
      state: { id: this.question.id } 
    });
  }

}
