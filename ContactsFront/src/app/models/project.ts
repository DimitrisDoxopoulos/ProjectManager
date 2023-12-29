import {Employee} from "./employee";

export interface Project {
  id: number;
  title: string;
  description: string;
  deadline: Date;
  slug: string;
  createdAt: Date;
  updatedAt: Date;
  employees: Employee[]
}
