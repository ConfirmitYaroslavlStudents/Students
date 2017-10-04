import Candidate from './candidate';

export default class Trainee extends Candidate{
  constructor(id, name, birthDate, email, comments, mentor) {
    super(id, name, birthDate, email, comments);
    this.mentor = mentor ? mentor : '';
  }
}