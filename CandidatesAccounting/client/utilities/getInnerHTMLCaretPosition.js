import getInnerTextCaretPostion from './getInnerTextCaretPosition'

const getInnerHTMLCaretPosition = (DOMelement) => {
  const innerTextCaretPosition = getInnerTextCaretPostion(DOMelement)
  const htmlContent = DOMelement.innerHTML
  let textOffset = 0
  let htmlOffset = 0
  let isInsideTag = false

  while (textOffset < innerTextCaretPosition) {
    while (htmlContent[htmlOffset] === '<') {
      isInsideTag = true
      htmlOffset++

      while (isInsideTag) {
        if (htmlContent[htmlOffset] === '>') {
          isInsideTag = false
        }
        htmlOffset++
      }
    }

    htmlOffset++
    textOffset++
  }

  return htmlOffset
}

export default getInnerHTMLCaretPosition