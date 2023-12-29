import { Pipe, PipeTransform } from '@angular/core';
import {Project} from "../models/project";

@Pipe({
  name: 'projectObjectToArray',
  standalone: true
})
export class ProjectObjectToArrayPipe implements PipeTransform {

  transform(object: Object): Project[] {
    let projects: Project[] = []


    let projectsArray = Object.values(object).map(([type, value]) => ({value}))
      console.log('value: ', projectsArray)


    return projects;
  }

}
