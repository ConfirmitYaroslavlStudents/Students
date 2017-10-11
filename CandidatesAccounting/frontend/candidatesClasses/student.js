import Candidate from './candidate';

export default class Student extends Candidate{
  constructor(id, name, birthDate, email, comments, groupName) {
    super(id, name, birthDate, email, comments);
    this.status = 'Student';
    this.groupName = groupName ? groupName : '';
  }
}