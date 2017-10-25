import Candidate from './candidate';

export default class Trainee extends Candidate{
  constructor(id, name, birthDate, email, comments, tags, mentor) {
    super(id, name, birthDate, email, comments, tags);
    this.status = 'Trainee';
    this.mentor = mentor ? mentor : '';
  }
}