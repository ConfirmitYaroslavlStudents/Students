import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateRowControls';
import CommentsEditDialog from './commentsEditDialog';
import AddIcon from 'material-ui-icons/Add';
import IconButton from 'material-ui/IconButton';

export default class TraineeTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  render() {
    let rows = (this.props.trainees.map((trainee, index) =>
      this.getRow(trainee, index)
    ));

    return <BasicTable
      heads={ ['#', 'Name', 'Birth Date', 'E-mail', 'Mentor', 'Comments', <span className="float-right">Actions</span>] }
      contentRows={rows}
    />
  }

  getRow(trainee, index)
  {
    return [
      index + 1,
      trainee.name,
      trainee.birthDate,
      trainee.email,
      trainee.mentor,
      <div>
        {trainee.comments ? trainee.comments.length + ' comment(s)' : 'no comments'}
        <IconButton onClick={function() {prompt('Type new comment here:')}}><AddIcon /></IconButton>
        <CommentsEditDialog/>
      </div>,
      <CandidateRowControls candidate={trainee} {...this.props}/>
    ];
  }
}