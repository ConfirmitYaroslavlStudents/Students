import React from 'react'
import PropTypes from 'prop-types'
import ExpansionPanel from '../../components/decorators/expansionPanel'

const CommentText = (props) => {
  const { text } = props
  let noTagText = text.replace(/<[/]p>/g, '\n')
  noTagText = noTagText.replace(/<[^>]*>/g, '')

  let lineCount = 0;
  let firstLine = ''
  for (let i = 0; i < noTagText.length; i++) {
    if (noTagText[i] === '\n') {
      lineCount++
    }
    if (lineCount === 0) {
      firstLine += noTagText[i]
    }
  }

  const wholeText = <div dangerouslySetInnerHTML={{__html: text}} />

  if (lineCount > 9 || noTagText.length > 600) {
    return (
      <ExpansionPanel
        summary={firstLine.slice(0, 104) + '...'}
        details={wholeText}
      />
    )
  }

  return wholeText
}

CommentText.propTypes = {
  text: PropTypes.string.isRequired
}

export default CommentText