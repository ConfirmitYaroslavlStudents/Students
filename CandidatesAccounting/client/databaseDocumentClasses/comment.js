export default class Comment {
  constructor(author, date, text) {
    this.author = author ? author : '';
    this.date = date ? date : '';
    this.text = text ? text : '';
  }
}