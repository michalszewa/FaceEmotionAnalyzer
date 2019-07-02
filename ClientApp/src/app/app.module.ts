import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router'

import { DropzoneModule, DROPZONE_CONFIG, DropzoneConfigInterface } from 'ngx-dropzone-wrapper';    
import { HomeComponent } from './Components/home/home.component'; 
import { AnalyzeComponent } from './Components/analyze/analyze.component';
import { NavmenuComponent } from './Components/navmenu/navmenu.component';
import { InfoComponent } from './Components/info/info.component';
import { ChartComponent} from './Components/chart/chart.component';


import { HttpClientModule } from '@angular/common/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { ChartsModule } from 'ng2-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
 import { ToastrModule } from 'ngx-toastr';


const DROPZONECONFIG: DropzoneConfigInterface = {        
  url: 'http://localhost:58955/api/upload',  
  maxFilesize: 100,    
  acceptedFiles: 'image/jpg,image/png,image/jpeg/*',
  uploadMultiple: false,
  dictDefaultMessage: 'Przeciągnij i upuść zdjęcie zawierające twarze',
  resizeHeight: 700,
  resizeWidth: 700
}; 

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AnalyzeComponent,
    NavmenuComponent,
    InfoComponent,
    ChartComponent
  ],
  imports: [
    BrowserModule,
    DropzoneModule,
    HttpClientModule,
    NgbModule,
    ChartsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      {path: 'home', component: HomeComponent},
      {path: 'analyze', component: AnalyzeComponent},
      {path: 'info/:id', component: InfoComponent},
      {path: 'info', component:InfoComponent},
      {path: '', component: HomeComponent}
    ])
  ],
  providers: [
    {
      provide: DROPZONE_CONFIG,
      useValue: DROPZONECONFIG
    },
    { provide: 'BASE_URL', useFactory: getBaseUrl}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return 'http://localhost:58955/';
}

