import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {Project} from "../models/project";
import {ProjectUpdate} from "../models/projectUpdate";
import {ProjectInsert} from "../models/projectInsert";

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  url: string = environment.BACKEND_API_URL;
  constructor(private http: HttpClient, private router: Router) { }

  public insertProject(project: ProjectInsert) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.post<Project>(`${this.url}projects`, project, httpOptions)
  }

  public updateProject(project: ProjectUpdate) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.patch<Project>(`${this.url}projects`, project, httpOptions)
  }

  public getProject(slug: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.get<Project>(`${this.url}projects/${slug}`, httpOptions)
  }

  public deleteProject(slug: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.delete<Project>(`${this.url}projects/${slug}`, httpOptions)
  }

  public getAllProjects() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.get<Project[]>(`${this.url}projects/all`, httpOptions)
  }

  public getAllProjectsOfUser(userId: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    }
    return this.http.post<Project[]>(`${this.url}projects/all`, userId, httpOptions)
  }
}
