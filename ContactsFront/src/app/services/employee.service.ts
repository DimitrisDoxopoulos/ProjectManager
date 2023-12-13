import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {Employee} from "../models/employee";
import {EmployeeInsert} from "../models/employeeInsert";
import {EmployeeUpdate} from "../models/employeeUpdate";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  url: string = environment.BACKEND_API_URL;
  constructor(private http: HttpClient, private router: Router) { }

  public getAllEmployees() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.get<Employee[]>(`${this.url}employees/all`, httpOptions)
  }

  public insertEmployee(employee: EmployeeInsert) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.post(`${this.url}employees`, employee, httpOptions)
  }

  public updateEmployee(employee: EmployeeUpdate) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.patch(`${this.url}employees`, employee, httpOptions)
  }

  public getEmployee(slug: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.get(`${this.url}employees/${slug}`, httpOptions)
  }

  public deleteEmployee(slug: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.delete(`${this.url}employees/${slug}`, httpOptions)
  }
}
