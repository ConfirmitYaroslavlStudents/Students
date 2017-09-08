import Candidate from './candidate';

export default class Interviewee extends Candidate{
  constructor(id, name, birthDate, email, comments, interviewDate, interviewRoom) {
    super(id, name, birthDate, email, comments);
    this.interviewDate = interviewDate;
    this.interviewRoom = interviewRoom;
  }
}