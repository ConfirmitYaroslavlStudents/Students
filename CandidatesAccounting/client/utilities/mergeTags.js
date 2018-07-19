const mergeTags = (currentTags, newTags) => {
  newTags.forEach(tag => {
    if (!currentTags.includes(tag)) {
      currentTags.push(tag)
    }
  })
  return currentTags
}

export default mergeTags