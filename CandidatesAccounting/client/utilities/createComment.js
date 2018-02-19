export default function CreateComment(author, date, text) {
  return {
    id: '',
    author: author ? author : '',
    date: date ? date : '',
    text: text ? text : ''
  }
}