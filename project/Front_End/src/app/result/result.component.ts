import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SearchService } from '../shared/services/search.service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {

  pollData = [];
  properties;
  question;
  constructor(private service: SearchService, private router:Router) {this.properties = this.router.getCurrentNavigation().extras.state;}

  ngOnInit(): void {
    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/home');
    }
    else{
      this.service.getQuestion(this.properties.id).subscribe(
        res => {this.question = res;
          this.question.choices.forEach(element => {
            this.pollData.push({"name": String(element.choice_text), 'value': Math.floor(element.votes)});
          });
          this.pollData = [...this.pollData];   
        },
        err => {console.log(err);},
      );
    }
    
  }

}
