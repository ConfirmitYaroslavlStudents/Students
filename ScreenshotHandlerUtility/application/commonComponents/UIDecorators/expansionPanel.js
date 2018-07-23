import React, { Component } from 'react'
import PropTypes from 'prop-types'
import ExpansionPanel from '@material-ui/core/ExpansionPanel'
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary'
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import Typography from '@material-ui/core/Typography'

class CustomExpansionPanel extends Component {
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
    const { summary, children } = this.props

    return (
      <ExpansionPanel expanded={expanded}>
        <ExpansionPanelSummary
          expandIcon={<ExpandMoreIcon />}
          onClick={this.handleChange}
        >
          <Typography>
            {summary}
          </Typography>
        </ExpansionPanelSummary>
        <ExpansionPanelDetails>
          {children}
        </ExpansionPanelDetails>
      </ExpansionPanel>
    )
  }
}

CustomExpansionPanel.propTypes = {
  summary: PropTypes.string.isRequired,
  expanded: PropTypes.bool
}

export default CustomExpansionPanel