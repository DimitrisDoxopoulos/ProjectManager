import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {ProjectAssignment} from "../models/project-assignment";

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {
  url: string = environment.BACKEND_API_URL;

  constructor(private http: HttpClient, private router: Router) { }

  public assignProject(assignment: any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.post(`${this.url}assign-projects`, assignment, httpOptions)
  }

  public removeAssignment(assignment: any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.post(`${this.url}assign-projects/delete`, assignment, httpOptions)
  }

  public getAllAssignments() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.post(`${this.url}assign-projects/all`, httpOptions)
  }

  public getAssAssignmentsOfUser(userId: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.post<ProjectAssignment[]>(`${this.url}assign-projects/user?userId=${userId}`, httpOptions)
  }
}
