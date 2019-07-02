import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router"

@Component({
  selector: 'app-analyze',
  templateUrl: './analyze.component.html',
  styleUrls: ['./analyze.component.css']
})
export class AnalyzeComponent implements OnInit {

  uploaded:boolean;
  public imageId: number;

  constructor(
    private router: Router,
    ) 
    { 
    this.uploaded = false;
  }

  ngOnInit() {
  }

  public onUploadSuccess = (event) => {
    this.uploaded = true;
    this.imageId = event[1].id;
    this.router.navigate(["info", this.imageId]);
  }

}
