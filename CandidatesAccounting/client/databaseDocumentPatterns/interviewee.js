import Candidate from './candidate';

export default class Interviewee extends Candidate{
  constructor(id, name, birthDate, email, comments, tags, interviewDate, resume) {
    super(id, name, birthDate, email, comments, tags);
    this.status = 'Interviewee';
    this.interviewDate = interviewDate ? interviewDate : '';
    this.resume = resume ? resume : '';
  }
}