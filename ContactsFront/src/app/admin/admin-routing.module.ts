import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmployeesComponent} from "./components/employees/employees.component";

const routes: Routes = [
  { path: '', loadComponent: () => import('./admin/admin.component').then(m => m.AdminComponent), loadChildren: () => [
      { path: 'home', loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent) },
      { path: 'manage', loadComponent: () => import('./components/manage-profile/manage-profile.component').then(m => m.ManageProfileComponent) },
      { path: 'change-password', loadComponent: () => import('./components/change-password/change-password.component').then(m => m.ChangePasswordComponent) },
      { path: 'employees', loadComponent: () => import('./components/employees/employees.component').then(m => m.EmployeesComponent) },
      { path: 'projects', loadComponent: () => import('./components/projects/projects.component').then(m => m.ProjectsComponent)},
      { path: 'manage-assignments', loadComponent: () => import('./components/manage-assignments/manage-assignments.component').then(m => m.ManageAssignmentsComponent)}
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
