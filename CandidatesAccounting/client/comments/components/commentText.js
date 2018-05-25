import React from 'react'
import PropTypes from 'prop-types'
import ExpansionPanel from '../../common/UIComponentDecorators/expansionPanel'

export default function CommentText(props) {
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

  if (lineCount > 9 || noTagText.length > 600) {
    return (
      <ExpansionPanel
        summary={firstLine.slice(0, 104) + '...'}
        details={<div dangerouslySetInnerHTML={{__html: text}}/>}
      />
    )
  } else {
    return <div dangerouslySetInnerHTML={{__html: text}}/>
  }
}

CommentText.propTypes = {
  text: PropTypes.string.isRequired
}