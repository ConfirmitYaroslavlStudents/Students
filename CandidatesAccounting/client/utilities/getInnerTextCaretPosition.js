export default function getInnerTextCaretPosition(DOMelement) {
  let caretPosition = 0
  const document = DOMelement.ownerDocument || DOMelement.document
  const window = document.defaultView || document.parentWindow
  let selection

  if (window.getSelection) {
    selection = window.getSelection()

    if (selection.rangeCount > 0) {
      const range = window.getSelection().getRangeAt(0)
      const previousCaretRange = range.cloneRange()

      previousCaretRange.selectNodeContents(DOMelement)
      previousCaretRange.setEnd(range.endContainer, range.endOffset)
      caretPosition = previousCaretRange.toString().length
    }
  } else if ((selection = document.selection) && selection.type !== 'Control') {
    const textRange = selection.createRange()
    const previousCaretTextRange = document.body.createTextRange()

    previousCaretTextRange.moveToElementText(DOMelement)
    previousCaretTextRange.setEndPoint('EndToEnd', textRange)
    caretPosition = previousCaretTextRange.text.length
  }

  return caretPosition
}