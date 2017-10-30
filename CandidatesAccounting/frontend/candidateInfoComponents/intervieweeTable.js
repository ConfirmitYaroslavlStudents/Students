import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';
import {isToday, isBirthDate} from '../moment';

export default class IntervieweeTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  getRow(interviewee, index)
  {
    return [
      index + 1,
      interviewee.name,
      interviewee.email,
      <span className={isBirthDate(interviewee.birthDate) ? 'today' : ''}>{interviewee.birthDate}</span>,
      <span className={isToday(interviewee.interviewDate) ? 'today' : ''}>{interviewee.interviewDate}</span>,
      <div>
        <span>resume.pdf </span>
        <button>
        view
        </button>
        <button>
          upload
        </button>
        <button>
        download
        </button>
      </div>,
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