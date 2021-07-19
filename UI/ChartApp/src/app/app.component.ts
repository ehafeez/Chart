import { Component, NgModule } from '@angular/core';
import { tempData } from './data';
import { TemperatureService } from './services/temperature.service';

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  tempData:  any = [
    {
      name: 'Temperature',
      series: [
        {
          "name": new Date,
          "value": Number
        }
      ]
    }
  ];

  //tempData:  any = [];
  view: any[] = [700, 300];

  // options
  legend: boolean = true;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Time';
  yAxisLabel: string = 'Temperature';
  timeline: boolean = true;

  colorScheme = {
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  };

  constructor(private temperatureService: TemperatureService) {

    this.temperatureService.getTemperatures().subscribe(result => {
      debugger;

      if(result != null){
        result.forEach(t => {
          this.tempData[0].series.push({"name": String(new Date(t.timeStamp)), "value": t.temperature});

        });

        this.tempData = [...this.tempData];

        //this.tempData = result.map(datum => ({ name: datum.timeStamp, value: datum.temperature }));
      }
    }, (err) => {
      console.log(err);
  });

    //Object.assign(this, { this.tempData });
  }

  onSelect(data): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  // onActivate(data): void {
  //   console.log('Activate', JSON.parse(JSON.stringify(data)));
  // }

  // onDeactivate(data): void {
  //   console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  // }
}