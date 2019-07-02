import { Component, OnInit } from '@angular/core';
import { Image } from 'src/app/Interfaces/image';
import { ActivatedRoute } from "@angular/router";
import { Face } from 'src/app/Interfaces/face';
import {DomSanitizer, SafeHtml} from '@angular/platform-browser';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { FaceService } from 'src/app/services/face.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {
  
  public image: Image;
  faces: Face[];
 
  constructor(
    private sanitizer: DomSanitizer,
    private activatedRoute: ActivatedRoute, 
    private toastr: ToastrService,
    private faceService:FaceService,
    private imageService: ImageService) { 
   }

  ngOnInit() {

    var id = +this.activatedRoute.snapshot.params["id"];

    if (id) {
     this.getImage(id);
     this.getFaces(id);
  }
  else {
      console.log("Invalid id");
  }
}

  private getImage(id:number) : void{
    this.imageService.getImage(id).subscribe(
      result => { this.image = result},
      err => { console.log(err)}
    )
  }

  private getFaces(imageId:number) : void{
     this.faceService.getFaces(imageId).subscribe(
       result => { this.faces = result; },
       err => { console.error(err) }
     );
  }

  public publicate(image:Image){
    image.Publicate = true;
    var id = +this.activatedRoute.snapshot.params["id"];

    this.imageService.putImage(image, id).subscribe(
      res =>{
          this.toastr.success('Opublikowano pomyślnie.', 'Twoja analiza jest publiczna.')
      }, 
      error => console.log(error));
  }

  public createImgPath = (serverPath: string) => {
    return this.sanitizer.bypassSecurityTrustResourceUrl(`http://localhost:58955/${serverPath}`);
  }

}
