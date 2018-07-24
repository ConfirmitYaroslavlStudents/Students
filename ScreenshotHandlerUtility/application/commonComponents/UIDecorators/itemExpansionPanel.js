import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'
import ExpansionPanel from '@material-ui/core/ExpansionPanel'
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary'
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import Typography from '@material-ui/core/Typography'

class ItemExpansionPanel extends Component {
  constructor(props) {
    super(props)
    this.state = { expanded: props.expanded ? props.expanded : false }
  }

  handleChange = (e) => {
    if (this.state.expanded) {
      this.setState({ expanded: false})
    } else {
      this.setState({ expanded: true})
    }
    e.stopPropagation()
  }

  render() {
    const { expanded } = this.state
    const { summary, borderColor, children } = this.props

    return (
      <Wrapper onClick={this.handleChange} borderColor={borderColor}>
        <ExpansionPanel classes={{root: 'expansion-panel-root'}} expanded={expanded}>
          <ExpansionPanelSummary
            expandIcon={<ExpandMoreIcon />}
          >
            {summary}
          </ExpansionPanelSummary>
          <ExpansionPanelDetails classes={{root: 'expansion-details-root'}}>
            {children}
          </ExpansionPanelDetails>
        </ExpansionPanel>
      </Wrapper>
    )
  }
}

ItemExpansionPanel.propTypes = {
  summary: PropTypes.oneOfType([PropTypes.string, PropTypes.object]).isRequired,
  expanded: PropTypes.bool,
  borderColor: PropTypes.string
}

export default ItemExpansionPanel

const Wrapper = styled.div`
  border-bottom: 1px solid #999;
  cursor: pointer;
  overflow: hidden;
  
  ${props => props.borderColor && css`
    border-color: ${props.borderColor};
	`}
`