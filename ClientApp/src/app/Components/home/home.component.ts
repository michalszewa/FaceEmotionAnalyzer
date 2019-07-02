import { Component, OnInit, Inject } from '@angular/core';
import {DomSanitizer, SafeHtml} from '@angular/platform-browser';
import { Image } from 'src/app/Interfaces/image';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  images : Image[];

  constructor(
    private sanitizer: DomSanitizer,
    private imageService:ImageService) { }

  ngOnInit() {
     this.getLatestImages();
  }

  private getLatestImages(){
    this.imageService.getLatest().subscribe(
      result => { this.images = result},
      err => { console.log(err)}
    )
  }

  private createImgPath = (serverPath: string) => {
    return this.sanitizer.bypassSecurityTrustResourceUrl(`http://localhost:58955/${serverPath}`);
  }
}
