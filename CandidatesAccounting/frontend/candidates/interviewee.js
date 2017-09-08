import Candidate from './candidate';

export default class Interviewee extends Candidate{
  constructor(id, name, birthDate, email, comment, interviewDate, interviewRoom) {
    super(id, name, birthDate, email, comment);
    this.interviewDate = interviewDate;
    this.interviewRoom = interviewRoom;
  }
}