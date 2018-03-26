import React, { Component } from 'react';
import styled from 'styled-components';
import { connect } from 'react-redux';
import {
  Switch,
  Route
} from 'react-router-dom';
import actions from '../../redux/actions';
import Navbar from './navbar';
import { searchById } from '../../utilities/candidateFilters';
import CommentsForm from '../comments/commentsForm';
import CandidateTable from '../candidates/candidateTable';
import IntervieweeTable from '../interviewees/intervieweeTable';
import StudentTable from '../students/studentTable';
import TraineeTable from '../trainees/traineeTable';
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
              <Spinner big/>
              :
              <Switch>
                <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
                  <CommentsForm
                    candidate={searchById(this.props.candidates, this.props.history.location.pathname.split('/')[2])}
                    addComment={this.props.addComment}
                    deleteComment={this.props.deleteComment}
                    username={this.props.username}
                    subscribe={this.props.subscribe}
                    unsubscribe={this.props.unsubscribe}
                    setState={this.props.setState}/>}
                />
                <Route exact path='/interviewees*' render={() =>
                  <IntervieweeTable interviewees={this.props.candidates} {...this.props}/>}
                />
                <Route exact path='/students*' render={() =>
                  <StudentTable students={this.props.candidates} {...this.props}/>}
                />
                <Route exact path='/trainees*' render={() =>
                  <TraineeTable trainees={this.props.candidates} {...this.props}/>}
                />
                <Route exact path='/*' render={() =>
                  <CandidateTable allCandidates={this.props.candidates} {...this.props}/>}
                />
                <Route path='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
              </Switch>
          }
        </Main>
        {
          this.props.applicationStatus === 'refreshing' ?
            <Spinner big/>
            : ''
        }
        <SnackBar message={this.props.errorMessage} setErrorMessage={this.props.setErrorMessage}/>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    applicationStatus: state.get('applicationStatus'),
    username: state.get('username'),
    pageTitle: state.get('pageTitle'),
    errorMessage: state.get('errorMessage'),
    searchRequest: state.get('searchRequest'),
    candidateStatus: state.get('candidateStatus'),
    offset: Number(state.get('offset')),
    candidatesPerPage: Number(state.get('candidatesPerPage')),
    totalCount: Number(state.get('totalCount')),
    sortingField: state.get('sortingField'),
    sortingDirection: state.get('sortingDirection'),
    candidates: state.get('candidates').toJS(),
    tags: state.get('tags').toArray(),
    notifications: state.get('notifications').toJS(),
  };
}

const Main = styled.div`
  margin-top: 108px;
`;

module.exports = connect(mapStateToProps, actions)(AppView);