import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgxChartsModule }from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { YouTubePlayerModule } from '@angular/youtube-player';
import { HomeComponent } from './home/home.component';
import { SearchService } from './shared/services/search.service';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatDialogModule} from '@angular/material/dialog';
import {MatInputModule} from '@angular/material/input';
import { QuestionComponent } from './question/question.component';
import { ResultComponent } from './result/result.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { CreatequestionComponent } from './createquestion/createquestion.component';
import { UserpollsComponent } from './userpolls/userpolls.component'; 

@NgModule({
  declarations: [
    AppComponent,    
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    QuestionComponent,
    ResultComponent,
    CreatequestionComponent,
    UserpollsComponent,   

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,HttpClientModule, BrowserAnimationsModule,MatIconModule,MatToolbarModule,
    MatSelectModule,NgxChartsModule,MatDialogModule,MatButtonModule,ReactiveFormsModule, FormsModule,MatCardModule, YouTubePlayerModule, MatProgressBarModule, MatInputModule,ToastrModule.forRoot({progressBar: true})
  ],
  providers: [SearchService],
  bootstrap: [AppComponent]
})
export class AppModule { }
