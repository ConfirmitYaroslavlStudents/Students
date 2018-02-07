import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Navbar from '../common/UIComponentDecorators/navbar';
import Logo from 'material-ui-icons/AccountCircle';
import SearchForm from './searchForm';
import LoginDialog from './loginDialog';
import FlatButton from '../common/UIComponentDecorators/flatButton';
import formatUserName from '../../utilities/formatUserName';

export default function MainNavbar(props) {
  return (
    <Navbar
      icon={<Logo />}
      title={props.title}
      rightPart={
        <RightPartWrapper>
          <SearchForm
            searchRequest={props.searchRequest}
            setSearchRequest={props.setSearchRequest}
            history={props.history}
          />
          {
            props.authorizationStatus === 'not-authorized' ?
            <LoginDialog login={props.login}/>
            :
            <div style={{display: 'inline-block'}}>
              <span style={{marginRight: 5, marginLeft: 5}}>{formatUserName(props.userName)}</span>
              <FlatButton color="contrast" onClick={() => {
                props.logout();
              }}>
                Sign out
              </FlatButton>
            </div>
          }
        </RightPartWrapper>}
    />
  );
}

MainNavbar.propTypes = {
  title: PropTypes.string.isRequired,
  userName: PropTypes.string.isRequired,
  authorizationStatus: PropTypes.string.isRequired,
  login: PropTypes.func.isRequired,
  logout: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
  searchRequest: PropTypes.string.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
};

const RightPartWrapper = styled.div`
  display: flex;
  align-items: center;
`;