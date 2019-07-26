import { Component, OnInit, Input} from '@angular/core';
import { Image } from 'src/app/Interfaces/image';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {
  @Input() image : Image;

  public barChartLabels: Label[] = [];
  public barChartType: ChartType;
  public barChartLegend : boolean;

  public barChartData: any[] = [
    { data: null, label: 'Wykryte emocje na twarzach' }
  ];

  constructor() {
   }


ngOnInit(){
this.barChartLabels = ['Złość', 'Pogarda','Obrzydzenie','Strach','Szczęście','Neutralie','Smutek','Zaskoczenie'];
this.barChartType = 'bar';
this.barChartLegend = true;


//var e1 = this.image.anger; var e2 = this.image.contempt; var e3 = this.image.disgust; var e4 = this.image.fear; var e5 = this.image.happiness;var e6 = this.image.neutral;var e7 = this.image.sadness;var e8 = this.image.surprise;
//let list: Array<number> = [e1,e2,e3,e4,e5,e6,e7,e8];
let list: Array<number> = [10,5,20,4,6,8,9,2];
this.barChartData[0].data = list;
}

public barChartOptions: ChartOptions = {
  responsive: true,
  // We use these empty structures as placeholders for dynamic theming.
  scales: { xAxes: [{}], yAxes: [{}] },
  plugins: {
    datalabels: {
      anchor: 'end',
      align: 'end',
    }
  }
};


public chartColors: Array<any> = [
  {
    backgroundColor: [
      'rgba(255, 99, 132, 0.2)',
      'rgba(54, 162, 235, 0.2)',
      'rgba(255, 206, 86, 0.2)',
      'rgba(75, 192, 192, 0.2)',
      'rgba(153, 102, 255, 0.2)',
      'rgba(255, 159, 64, 0.2)',
      'rgba(255, 159, 64, 0.2)',
      'rgba(255, 159, 64, 0.2)'
    ],
    borderColor: [
      'rgba(255,99,132,1)',
      'rgba(54, 162, 235, 1)',
      'rgba(255, 206, 86, 1)',
      'rgba(75, 192, 192, 1)',
      'rgba(153, 102, 255, 1)',
      'rgba(255, 159, 64, 1)',
      'rgba(255, 159, 64, 0.2)',
      'rgba(255, 159, 64, 0.2)'
    ],
    borderWidth: 2,
  }
];


}