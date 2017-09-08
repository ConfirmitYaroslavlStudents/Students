// 'abstract' class
export default class Candidate {
  constructor(id, name, birthDate, email, comments) {
    this.id = id;
    this.name = name;
    this.birthDate = birthDate;
    this.email = email;
    this.comments = comments;
  }
}