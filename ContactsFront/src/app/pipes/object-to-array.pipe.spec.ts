import { ProjectObjectToArrayPipe } from './project-object-to-array.pipe';

describe('ObjectToArrayPipe', () => {
  it('create an instance', () => {
    const pipe = new ProjectObjectToArrayPipe();
    expect(pipe).toBeTruthy();
  });
});
