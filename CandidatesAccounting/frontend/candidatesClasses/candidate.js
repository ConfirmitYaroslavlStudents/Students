export default class Candidate {
  constructor(id, name, birthDate, email, comments) {
    this.status = 'Candidate';
    this.id = id ? id : '';
    this.name = name ? name : '';
    this.birthDate = birthDate ? birthDate : '';
    this.email = email ? email : '';
    this.comments = [];
    if (comments) {
      for (let i = 0; i < comments.length; i++) {
        this.comments.push(comments[i]);
      }
    }
  }
}