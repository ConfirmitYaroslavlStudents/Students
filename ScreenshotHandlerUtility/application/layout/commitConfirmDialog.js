import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { Modal, Button } from 'confirmit-react-components'

class CommitConfirDialog extends Component {
  constructor(props) {
    super(props)
    this.state = { open: false }
  }

  handleOpen = () => {
    this.setState({ open: true })
  }

  handleClose = () => {
    this.setState({ open: false })
  }

  render() {
    const { open } = this.state
    const { commit, testsToUpdate } = this.props

    const markedToUpdateAmount = testsToUpdate.length

    return (
      <React.Fragment>
        <Button onClick={this.handleOpen} disabled={markedToUpdateAmount <= 0} className='contrast-button'>
          Commit{markedToUpdateAmount > 0 && ` (${markedToUpdateAmount})`}
        </Button>
        <Modal open={open} backdrop={true} onHide={this.handleClose}>
          <Modal.Header>
            <TitleWrapper>
              {
                markedToUpdateAmount === 1 ?
                  `A base screenshot will be updated`
                  :
                  `Base screenshots (${markedToUpdateAmount}) will be updated`
              }
            </TitleWrapper>
          </Modal.Header>
          <Modal.Body>
            <ScrollableBody>
              {
                testsToUpdate.map((test, index) =>
                  <TestInfoWrapper key={index}>
                    {test.index + 1}. {test.testName} №{test.number} ({test.browserName}) {test.screenshotName ? ` (${test.screenshotName})` : ''}
                  </TestInfoWrapper>
                )
              }
            </ScrollableBody>
          </Modal.Body>
          <Modal.Footer>
            <ButtonWrapper>
              <Button onClick={commit} className='primary-button'>
                Confirm
              </Button>
            </ButtonWrapper>
            <ButtonWrapper>
              <Button onClick={this.handleClose} className='error-button'>
                Cancel
              </Button>
            </ButtonWrapper>
          </Modal.Footer>
        </Modal>
      </React.Fragment>
    )
  }
}

CommitConfirDialog.propTypes = {
  commit: PropTypes.func.isRequired,
  testsToUpdate: PropTypes.array.isRequired
}

export default CommitConfirDialog

const TitleWrapper = styled.span`
  font-weight: bold;
`

const ScrollableBody = styled.div`
  max-height: 500px;
  overflow-y: auto;
`

const TestInfoWrapper = styled.div`
  padding: 8px 0;
`

const ButtonWrapper = styled.div`
  display: inline-flex;
  margin: 0 8px;
`