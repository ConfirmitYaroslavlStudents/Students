export default class Candidate {
  constructor(id, name, birthDate, email, comments) {
    this.id = id;
    this.name = name;
    this.birthDate = birthDate;
    this.email = email;
    this.comments = [];
    if (comments) {
      for (let i = 0; i < comments.length; i++) {
        this.comments.push(comments[i]);
      }
    }
  }
}