import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { baseUrl} from 'src/app/shared/constants';
import { map } from 'rxjs/operators';
import { User } from '../models/User';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$= this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }
  login(model: any){
    return this.http.post(baseUrl+'account/login', model).pipe(
      map((response: User) =>
      {
        const user = response;
        if(user)
        {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }         
      })
    );
  }
  
  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }
  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
  register(model:any){
    return this.http.post(baseUrl+'account/register',model).pipe(
      map((user: User) =>{
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

}
