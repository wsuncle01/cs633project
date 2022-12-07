import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SearchService } from '../shared/services/search.service';

@Component({
  selector: 'app-createquestion',
  templateUrl: './createquestion.component.html',
  styleUrls: ['./createquestion.component.css']
})
export class CreatequestionComponent implements OnInit {

  constructor(private router: Router, private service: SearchService, private toastr: ToastrService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/home');
    }
  }

  choices = [{value: ""}];
  question : string;
  details;

  createQuestion(event: any){

  }

  submit(){
    this.service.createQuestion(this.question).subscribe(
      res => {
        this.details = res;
        this.choices.forEach(element => {
          this.service.addChoice(element.value, this.details.id).subscribe(err => {console.log(err)});
      });
      this.toastr.success('Poll created!');
    
    },
      err => {console.log(err);
        this.toastr.error("Not successful");
      },
    );
  }

  addInput()  {
    this.choices.push({value: ''});
    console.log(this.choices);
  }
}
