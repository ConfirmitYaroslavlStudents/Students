export default function CreateComment(author, date, text, attachment) {
  return {
    id: '',
    author: author ? author : '',
    date: date ? date : '',
    text: text ? text : '',
    attachment: attachment ? attachment : ''
  }
}