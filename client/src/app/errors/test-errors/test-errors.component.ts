import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { baseUrl } from 'src/app/shared/constants';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  validationErrors:string[]=[];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  get404Error(){
    this.http.get(baseUrl + 'buggy/not-found').subscribe(response => {
      console.log(response);
    }, error =>{
      console.log(error);
    });
  }
  get400Error(){
    this.http.get(baseUrl + 'buggy/bad-request').subscribe(response => {
      console.log(response);
    }, error =>{
      console.log(error);
    });
  }
  get500Error(){
    this.http.get(baseUrl + 'buggy/server-error').subscribe(response => {
      console.log(response);
    }, error =>{
      console.log(error);
    });
  }
  get401Error(){
    this.http.get(baseUrl + 'buggy/auth').subscribe(response => {
      console.log(response);
    }, error =>{
      console.log(error);
    });
  }
  get400ValidationError(){
    const user ={
      username:'',
      password:'pas'
    }
    this.http.post(baseUrl + 'account/register',user).subscribe(response => {
      console.log(response);
    }, error =>{
      console.log(error);
      this.validationErrors = error;
    });
  }

}
