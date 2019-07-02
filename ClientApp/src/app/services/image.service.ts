import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Image } from '../Interfaces/image';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private imageUrl:string;

  constructor(@Inject('BASE_URL')private baseUrl:string, private http:HttpClient) {
    this.imageUrl = "api/image/";
   }

   getImage(id:number) : Observable<Image>{
     let url = this.baseUrl + this.imageUrl + id;
     return this.http.get<Image>(url);
   }

   getLatest() : Observable<Image[]>{
     let url = this.baseUrl + this.imageUrl + 'latest';
     return this.http.get<Image[]>(url);
   }

   putImage(image:Image, id:number) : Observable<any>{
     let url = this.baseUrl + this.imageUrl + id;
     return this.http.put<Image>(url, image);
   }

}
