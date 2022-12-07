import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SearchService } from '../shared/services/search.service';

@Component({
  selector: 'app-userpolls',
  templateUrl: './userpolls.component.html',
  styleUrls: ['./userpolls.component.css']
})
export class UserpollsComponent implements OnInit {

  polls: any[] = [];
  question;
  constructor(private router:Router, private service: SearchService, private toastr: ToastrService) {}

  ngOnInit(): void {
    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/home');
    }
    else{
      this.service.getUserPolls()
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
  }

  displayQuestion(id){
    this.router.navigate(['/home/result'], { 
      state: { id: id } 
    });
  }

}
