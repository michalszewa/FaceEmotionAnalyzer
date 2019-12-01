import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Face } from '../Interfaces/face';

@Injectable({
  providedIn: 'root'
})
export class FaceService {

   private faceUrl : string;

  constructor(
   @Inject('BASE_URL') private baseUrl : string,
   private http : HttpClient
  ) { 
    this.faceUrl = "api/face/";
  }

  getFaces(imageId:number) : Observable<Face[]> {
    let url = this.baseUrl + this.faceUrl + 'all/' + imageId;
    return this.http.get<Face[]>(url);
  }

}
