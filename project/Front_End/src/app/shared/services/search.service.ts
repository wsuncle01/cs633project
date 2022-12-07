import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  public ul = "";

  public testing :any
  public setTest(value: any) {
    return this.testing = value;
}


  constructor(private http: HttpClient, private fb: FormBuilder) {}

    formModel = this.fb.group({
      Email: ['', Validators.email],
      Auth: ['', [Validators.required]],
      Passwords: this.fb.group({
        Password: ['', [Validators.required, Validators.minLength(4)]],
        ConfirmPassword: ['', Validators.required]
      }, { validator: this.comparePasswords })

    });

    comparePasswords(fb: FormGroup) {
      let confirmPswrdCtrl = fb.get('ConfirmPassword');
      if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
        if (fb.get('Password').value != confirmPswrdCtrl.value)
          confirmPswrdCtrl.setErrors({ passwordMismatch: true });
        else
          confirmPswrdCtrl.setErrors(null);
      }
    }

    register() {
      var body = {
        email: this.formModel.value.Email,
        role: this.formModel.value.Auth,
        password: this.formModel.value.Passwords.Password
      };
      return this.http.post('http://127.0.0.1:8000/api/register', body);
    }

    login(formData) {
      return this.http.post('http://127.0.0.1:8000/api/login', formData);
    }

    getPolls(){
      return this.http.get('http://127.0.0.1:8000/questions');

    }

    getUserPolls(){
      return this.http.get('http://127.0.0.1:8000/questionsuser', {headers: new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'))});
    }

    getQuestion(id){
      return this.http.get('http://127.0.0.1:8000/questions/' + id, {headers: new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'))});
    }
    
    hasAnswered(id){
      return this.http.get('http://127.0.0.1:8000/answered/' + id, {headers: new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'))});
    }


    makeVote(id){
      let params = new HttpParams().append('choice_id', id)
      return this.http.put('http://127.0.0.1:8000/choices/vote', null, {params: params, headers: new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'))});
    }

    createQuestion(text){
      var body = {
        question_text: text
      };
      return this.http.post('http://127.0.0.1:8000/questions/', body, {headers: new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'))});
    }

    addChoice(text, id){
      var body = {
        choice_text: text
      };
      return this.http.post('http://127.0.0.1:8000/questions/' + id + '/choice', body, {headers: new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'))});
    }

}



