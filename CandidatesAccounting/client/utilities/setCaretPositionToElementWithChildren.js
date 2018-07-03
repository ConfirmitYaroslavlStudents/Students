export default function setCaretPositionToElementWithChildren(DOMelement, position) {
  DOMelement.focus()
  for (const child of DOMelement.childNodes) {
    if (child.nodeType === 3) {
      if (child.length >= position) {
        const range = document.createRange()
        const selection = window.getSelection()
        range.setStart(child, position)
        range.collapse(true)
        selection.removeAllRanges()
        selection.addRange(range)
        return -1
      } else {
        position -= child.length
      }
    } else {
      position = setCaretPositionToElementWithChildren(child, position)
      if(position === -1){
        return -1
      }
    }
  }
  return position
}