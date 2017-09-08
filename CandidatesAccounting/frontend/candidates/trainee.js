import Candidate from './candidate';

export default class Trainee extends Candidate{
  constructor(id, name, birthDate, email, comment, mentor) {
    super(id, name, birthDate, email, comment);
    this.mentor = mentor;
  }
}