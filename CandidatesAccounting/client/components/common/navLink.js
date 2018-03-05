import React from 'react';
import PropTypes from 'prop-types';

export default function CustomNavLink(props) {
  return (
    <div onClick={props.onClick} className={(props.className ? props.className : 'nav-link') + (props.active ? ' active-nav-link' : '')}>
      <a style={{margin: 'auto'}}>{props.children}</a>
    </div>
  );
}

CustomNavLink.propTypes = {
  onClick: PropTypes.func.isRequired,
  className: PropTypes.string,
  active: PropTypes.bool,
};