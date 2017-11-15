import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Navbar from './UIComponentDecorators/navbar';
import Logo from 'material-ui-icons/AccountCircle';
import IconButton from './UIComponentDecorators/iconButton';
import RenameIcon from 'material-ui-icons/ModeEdit';

export default function MainNavbar(props) {
  return (
    <Navbar
      icon={<Logo />}
      title="Candidate Accounting"
      rightPart={
        <RightPartWrapper>
          <span>{props.userName}</span>
          <IconButton
            color="contrast"
            style={{width: 30, height: 30}}
            icon={<RenameIcon/>}
            onClick={() => {
              props.setUserName(prompt('Enter new userame:'));
            }}
          />
        </RightPartWrapper>}
    />
  );
}

MainNavbar.propTypes = {
  userName: PropTypes.string.isRequired,
  setUserName: PropTypes.func.isRequired,
};

const RightPartWrapper = styled.div`
  display: flex;
  align-items: center;
`;