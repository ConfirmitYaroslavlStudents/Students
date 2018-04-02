import React, { Component } from 'react';
import styled from 'styled-components';
import { connect } from 'react-redux';
import {
  Switch,
  Route
} from 'react-router-dom';
import actions from '../../redux/actions';
import Navbar from './navbar';
import CandidateTable from '../candidates/candidateTable';
import IntervieweeTable from '../interviewees/intervieweeTable';
import StudentTable from '../students/studentTable';
import TraineeTable from '../trainees/traineeTable';
import CommentForm from '../comments/commentsForm';
import SnackBar from '../common/UIComponentDecorators/snackbar';
import ErrorPage from './errorPage';
import Spinner from '../common/UIComponentDecorators/spinner';

export default class AppView extends Component {
  render() {
    return (
      <div>
        <Navbar {...this.props}/>
        <Main>
          {
            this.props.applicationStatus === 'loading'?
              <Spinner size="big"/>
              :
              <Switch>
                <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
                  <CommentForm
                    candidate={this.props.candidates[this.props.history.location.pathname.split('/')[2]]}
                    applicationStatus={this.props.applicationStatus}
                    addComment={this.props.addComment}
                    deleteComment={this.props.deleteComment}
                    username={this.props.username}
                    subscribe={this.props.subscribe}
                    unsubscribe={this.props.unsubscribe}
                    setState={this.props.setState}/>
                }/>

                <Route exact path='/interviewees*' render={() =>
                  <IntervieweeTable {...this.props}/>}
                />
                <Route exact path='/students*' render={() =>
                  <StudentTable {...this.props}/>}
                />
                <Route exact path='/trainees*' render={() =>
                  <TraineeTable {...this.props}/>}
                />
                <Route exact path='/*' render={() =>
                  <CandidateTable {...this.props}/>}
                />
                <Route path='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
              </Switch>
          }
        </Main>
        {
          this.props.applicationStatus === 'refreshing' ?
            <Spinner size="big"/>
            : ''
        }
        <SnackBar message={this.props.errorMessage} setErrorMessage={this.props.setErrorMessage}/>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    applicationStatus: state.applicationStatus,
    username: state.username,
    pageTitle: state.pageTitle,
    errorMessage: state.errorMessage,
    searchRequest: state.searchRequest,
    candidateStatus: state.candidateStatus,
    offset: Number(state.offset),
    candidatesPerPage: Number(state.candidatesPerPage),
    totalCount: Number(state.totalCount),
    sortingField: state.sortingField,
    sortingDirection: state.sortingDirection,
    candidates: state.candidates,
    tags: state.tags,
    notifications: state.notifications,
  };
}

const Main = styled.div`
  margin-top: 108px;
`;

module.exports = connect(mapStateToProps, actions)(AppView);