import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'employeeObjectToArray',
  standalone: true
})
export class EmployeeObjectToArrayPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
