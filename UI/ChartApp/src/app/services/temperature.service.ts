import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { TemperatureModel } from "../models/temperature";

@Injectable({
    providedIn: "root",
  })
  export class TemperatureService {
    private readonly url;

      constructor(private http: HttpClient) {
        this.url = "https://localhost:55008/api/Temperature/getTemperatures/";
    }

    getTemperatures(): Observable<TemperatureModel[]> {
        return this.http.get<TemperatureModel[]>(`${this.url}`);
      }

    }