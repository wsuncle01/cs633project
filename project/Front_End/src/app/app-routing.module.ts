import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { QuestionComponent } from './question/question.component';
import { CreatequestionComponent } from './createquestion/createquestion.component';
import { ResultComponent } from './result/result.component';

import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { UserpollsComponent } from './userpolls/userpolls.component';

const routes: Routes = [
  {path:'home', component : HomeComponent},
  {path:'home/question', component : QuestionComponent},
  {path:'home/result', component : ResultComponent},
  {path:'home/create', component : CreatequestionComponent},
  {path:'home/yourpoll', component : UserpollsComponent},
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  {path:'**',redirectTo:'home',pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
