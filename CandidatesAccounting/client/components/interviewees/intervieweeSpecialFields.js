import React from 'react';
import PropTypes from 'prop-types';
import DateTimePicker from '../common/UIComponentDecorators/dateTimePicker';
import styled from 'styled-components';
import ResumeControls from '../interviewees/resumeControls';
import {toDateTimePickerFormat, fromDateTimePickerFormat} from '../../utilities/customMoment';

export default function IntervieweeSpecialFields(props) {
  return (
    <div>
      <DateTimePicker
        label="Interview date"
        defaultValue={toDateTimePickerFormat(props.interviewee.interviewDate)}
        onChange={(value) => {props.changeInfo('interviewDate', fromDateTimePickerFormat(value))}}
      />
      <Label marginBottom="-10px">Resume</Label>
      <ResumeControls interviewee={props.interviewee} authorized/>
    </div>);
}

IntervieweeSpecialFields.propTypes = {
  interviewee: PropTypes.object.isRequired,
  changeInfo: PropTypes.func.isRequired,
};

const Label = styled.p`
  margin-top: 16px;
  margin-bottom: ${props => props.marginBottom ? props.marginBottom : '0'};;
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`;