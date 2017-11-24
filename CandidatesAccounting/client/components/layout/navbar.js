import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Navbar from '../common/UIComponentDecorators/navbar';
import Logo from 'material-ui-icons/AccountCircle';
import IconButton from '../common/UIComponentDecorators/iconButton';
import RenameIcon from 'material-ui-icons/ModeEdit';
import SearchForm from './searchForm';

export default function MainNavbar(props) {
  return (
    <Navbar
      icon={<Logo />}
      title="Candidate Accounting"
      rightPart={
        <RightPartWrapper>
          <SearchForm
            searchRequest={props.searchRequest}
            setSearchRequest={props.setSearchRequest}
            search={props.search}
            history={props.history}
          />
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
  history: PropTypes.object.isRequired,
  searchRequest: PropTypes.string.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  search: PropTypes.func.isRequired,
};

const RightPartWrapper = styled.div`
  display: flex;
  align-items: center;
`;