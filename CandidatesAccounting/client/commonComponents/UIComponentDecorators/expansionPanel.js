import React, { Component } from 'react'
import PropTypes from 'prop-types'
import ExpansionPanel from '@material-ui/core/ExpansionPanel'
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary'
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import Typography from '@material-ui/core/Typography'

export default class CustomExpansionPanel extends Component {
  constructor(props) {
    super(props)
    this.state = { expanded: false }
  }

  handleChange = () => {
    if (this.state.expanded) {
      this.setState({ expanded: false})
    } else {
      this.setState({ expanded: true})
    }
  }

  render() {
    const { expanded } = this.state
    const { summary, details} = this.props

    return (
      <ExpansionPanel classes={{root: 'expansion-panel-root'}}>
        <ExpansionPanelSummary
          classes={{root: 'expansion-summary-root', expanded: 'expansion-summary-expanded'}}
          expandIcon={<ExpandMoreIcon/>}
          onClick={this.handleChange}
        >
          <Typography>
            { expanded ? '' : summary }
          </Typography>
        </ExpansionPanelSummary>
        <ExpansionPanelDetails
          classes={{root: 'expansion-details-root'}}
        >
          {details}
        </ExpansionPanelDetails>
      </ExpansionPanel>
    )
  }
}

CustomExpansionPanel.propTypes = {
  summary: PropTypes.string.isRequired,
  details: PropTypes.oneOfType([PropTypes.string, PropTypes.object]).isRequired
}