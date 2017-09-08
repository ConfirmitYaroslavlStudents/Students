import Candidate from './candidate';

export default class Student extends Candidate{
  constructor(id, name, birthDate, email, comment, groupName) {
    super(id, name, birthDate, email, comment);
    this.groupName = groupName;
  }
}