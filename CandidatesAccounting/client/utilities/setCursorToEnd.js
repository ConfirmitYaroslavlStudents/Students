export default function SetCursorToEnd(DOMelement) {
  let range = document.createRange()
  let sel = window.getSelection()
  range.setStart(DOMelement, 1)
  range.collapse(true)
  sel.removeAllRanges()
  sel.addRange(range)
  DOMelement.focus()
}