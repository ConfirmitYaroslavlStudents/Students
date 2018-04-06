import { getCurrentDateTime } from './customMoment'

export default class Comment {
  constructor(author, text, attachment) {
    this.id = ''
    this.author = author
    this.date = getCurrentDateTime()
    this.text = text
    this.attachment = attachment ? attachment : ''
  }
}