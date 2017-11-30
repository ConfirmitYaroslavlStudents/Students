export default class Candidate {
  constructor(id, name, birthDate, email, comments, tags) {
    this.status = 'Candidate';
    this.id = id ? parseInt(id) : '';
    this.name = name ? name : '';
    this.birthDate = birthDate ? birthDate : '';
    this.email = email ? email : '';
    this.comments = [];
    if (comments) {
      for (let i = 0; i < comments.length; i++) {
        this.comments.push(comments[i]);
      }
    }
    this.tags = [];
    if (tags) {
      for (let i = 0; i < tags.length; i++) {
        this.tags.push(tags[i]);
      }
    }
  }
}