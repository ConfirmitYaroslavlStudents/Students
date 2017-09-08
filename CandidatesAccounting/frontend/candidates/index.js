import Candidate from './candidate';
import Interviewee from './interviewee';
import Student from './student';
import Trainee from './trainee';
import Comment from './comment';

function CreateCandidate(status, args) {
  switch (status) {
    case 'Interviewee':
      return new Interviewee(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
        args.interviewDate,
        args.interviewRoom
      );
    case 'Student':
      return new Student(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
        args.groupName
      );
    case 'Trainee':
      return new Trainee(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
        args.mentor
      );
    default:
      return new Candidate(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
      );
  }
}

module.exports = {Candidate, Interviewee, Student, Trainee, Comment, CreateCandidate};

