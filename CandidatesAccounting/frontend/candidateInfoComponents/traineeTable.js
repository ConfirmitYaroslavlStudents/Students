import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';
import {formatDate, isBirthDate} from '../customMoment';
import Tag from '../materialUIDecorators/tag';
import { NavLink } from 'react-router-dom';

export default class TraineeTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  getRow(trainee, index)
  {
    return [
      index + 1,
      <div>
        {trainee.name}
        {trainee.tags.map((tag, index) => (<NavLink to={"/tag/" + encodeURIComponent(tag)} key={index}><Tag content={tag} /></NavLink>))}
      </div>,
      trainee.email,
      <span className={isBirthDate(trainee.birthDate) ? 'today' : ''}>{formatDate(trainee.birthDate)}</span>,
      trainee.mentor,
      <CandidateRowControls candidate={trainee} {...this.props}/>
    ];
  }

  render() {
    let rows = (this.props.trainees.map((trainee, index) =>
      this.getRow(trainee, index)
    ));

    return (
      <BasicTable
        heads={ ['#', 'Name', 'E-mail', 'Birth Date', 'Mentor', <span className="float-right">Actions</span>] }
        contentRows={rows}
      />
    );
  }
}

TraineeTable.propTypes = {
  trainees: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};