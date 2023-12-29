import { EmployeeObjectToArrayPipe } from './employee-object-to-array.pipe';

describe('EmployeeObjectToArrayPipe', () => {
  it('create an instance', () => {
    const pipe = new EmployeeObjectToArrayPipe();
    expect(pipe).toBeTruthy();
  });
});
