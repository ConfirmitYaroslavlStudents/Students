import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../UIComponentDecorators/basicTable';
import CandidateRowControls from '../candidateComponents/candidateControls';
import {formatDateTime, formatDate, isToday, isBirthDate} from '../customMoment';
import Tag from '../UIComponentDecorators/tag';
import { NavLink } from 'react-router-dom';
import ResumeControls from '../candidateComponents/resumeControls';

export default class IntervieweeTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  getRow(interviewee, index)
  {
    return [
      index + 1,
      <div>
        {interviewee.name}
        {interviewee.tags.map((tag, index) => (<NavLink to={"/tag/" + encodeURIComponent(tag)} key={index}><Tag content={tag} /></NavLink>))}
      </div>,
      interviewee.email,
      <span className={isBirthDate(interviewee.birthDate) ? 'today' : ''}>{formatDate(interviewee.birthDate)}</span>,
      <span className={isToday(interviewee.interviewDate) ? 'today' : ''}>{formatDateTime(interviewee.interviewDate)}</span>,
      <ResumeControls fileName={interviewee.resume}/>,
      <CandidateRowControls candidate={interviewee} {...this.props}/>
    ];
  }

  render() {
    let rows = (this.props.interviewees.map((interviewee, index) =>
      this.getRow(interviewee, index)
    ));

    return (
      <BasicTable
        heads={ ['#', 'Name', 'E-mail', 'Birth Date',  'Interview date', 'Resume',
          <span className="float-right">Actions</span>] }
        contentRows={rows}
      />
    );
  }
}

IntervieweeTable.propTypes = {
  interviewees: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};