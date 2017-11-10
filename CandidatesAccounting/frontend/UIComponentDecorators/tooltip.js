import React from 'react';
import PropTypes from 'prop-types';
import Tooltip from 'material-ui/Tooltip';

export default function CustomTooltip(props) {
  return (
    <Tooltip
      title={props.title}
      label={props.title}
      placement={props.placement ? props.placement : 'bottom'}
    >
      <div style={{display: 'inline-block'}}>
        {props.children}
      </div>
    </Tooltip>
  );
}

CustomTooltip.propTypes = {
  title: PropTypes.string.isRequired,
  placement: PropTypes.string,
};