import {Project} from "./project";
import {Employee} from "./employee";

export interface Assignment {
  project: Project
  employees: Employee[]
}
