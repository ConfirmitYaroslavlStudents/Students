import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateRowControls';
import CommentsEditDialog from './commentsEditDialog';
import AddIcon from 'material-ui-icons/Add';
import IconButton from 'material-ui/IconButton';

export default class IntervieweeTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  render() {
    let rows = (this.props.interviewees.map((interviewee, index) =>
      this.getRow(interviewee, index)
    ));

    return <BasicTable
      heads={ ['#', 'Name', 'Birth Date', 'E-mail', 'Interview date', 'Interview room', 'Comments',
        <span className="float-right">Actions</span>] }
      contentRows={rows}
    />
  }

  getRow(interviewee, index)
  {
    return [
      index + 1,
      interviewee.name,
      interviewee.birthDate,
      interviewee.email,
      interviewee.interviewDate,
      interviewee.interviewRoom,
      <div>
        {interviewee.comments ? interviewee.comments.length + ' comment(s)' : 'no comments'}
        <IconButton onClick={function() {prompt('Type new comment here:')}}><AddIcon /></IconButton>
        <CommentsEditDialog/>
      </div>,
      <CandidateRowControls candidate={interviewee} {...this.props}/>
    ];
  }
}