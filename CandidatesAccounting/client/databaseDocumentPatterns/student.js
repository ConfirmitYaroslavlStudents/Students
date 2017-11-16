import Candidate from './candidate';

export default class Student extends Candidate{
  constructor(id, name, birthDate, email, comments, tags, groupName, startingDate, endingDate) {
    super(id, name, birthDate, email, comments, tags);
    this.status = 'Student';
    this.groupName = groupName ? groupName : '';
    this.startingDate = startingDate ? startingDate : '';
    this.endingDate = endingDate ? endingDate : '';
  }
}