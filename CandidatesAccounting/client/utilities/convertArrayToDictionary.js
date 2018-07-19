const convertArrayToDictinary = (array) => {
  if (array instanceof Array) {
    const dictionary = {}
    array.forEach(element => {
      if (element.id) {
        dictionary[element.id] = element
      } else {
        dictionary[element] = element
      }
    })
    return dictionary
  } else {
    return array
  }
}

export default convertArrayToDictinary