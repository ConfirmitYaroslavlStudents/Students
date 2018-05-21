export default function mergeTags(currentTags, newTags) {
  newTags.forEach(tag => {
    if (!currentTags.includes(tag)) {
      currentTags.push(tag)
    }
  })
  return currentTags
}