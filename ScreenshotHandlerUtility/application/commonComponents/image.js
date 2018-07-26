import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import path from 'path'
import Tooltip from './UIDecorators/tooltip'

class Image extends Component {
  constructor(props) {
    super(props)
    this.state = { fullView: false}
  }

  handleOpen = () => {
    this.setState({ fullView: true })
  }

  handleClose = () => {
    this.setState({ fullView: false })
  }

  render() {
    const { source, alternativeText, height, width } = this.props

    const fullViewImg =
      this.state.fullView ?
        <FullViewWrapper onClick={this.handleClose}>
          <FullViewImageWrapper onClick={(e) => {e.stopPropagation()}}>
            <Tooltip title={path.join(source)} open>
              <img src={path.join(source)} alt={alternativeText} onClick={this.handleClose} style={{ cursor: 'pointer' }} />
            </Tooltip>
          </FullViewImageWrapper>
        </FullViewWrapper>
        : null

    return (
      <React.Fragment>
        <Tooltip title={`Open in original size`}>
          <img
            src={path.join(source)}
            alt={alternativeText}
            height={height}
            width={width}
            style={{cursor: 'pointer'}}
            onClick={this.handleOpen}
          />
        </Tooltip>
        {fullViewImg}
      </React.Fragment>
    )
  }
}

Image.propTypes = {
  source: PropTypes.string.isRequired,
  alternativeText: PropTypes.string,
  height: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
  width: PropTypes.oneOfType([PropTypes.number, PropTypes.string])
}

export default Image

const FullViewWrapper = styled.div`
  display: flex;
  text-align: center;
  position: fixed;
  left: 0;
  top: 0;
  height: 100vmin;
  width: 100%;
  z-index: 101;
  background-color: rgba(0, 0, 0, 0.5);
`

const FullViewImageWrapper = styled.div`
  display: inline-block;
  margin: auto;
`