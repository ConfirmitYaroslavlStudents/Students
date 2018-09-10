import mongoose from 'mongoose'
import { TagSchema } from '../schemas'

const Tag = mongoose.model('Tag', TagSchema, 'tags')

export const getTags = () => {
  return Tag.find({}).exec()
  .then(tags => tags.map(tag => tag.title))
}

export const updateTags = (newTags) => {
  getTags()
  .then(tags => {
    const tagsToAdd = []
    newTags.forEach(tag => {
      if (!tags.includes(tag)) {
        tagsToAdd.push({ title: tag })
      }
    })
    if (tagsToAdd.length > 0) {
      Tag.create(tags)
    }
  })
}