import Candidate from './candidate';
import Interviewee from './interviewee';
import Student from './student';
import Trainee from './trainee';
import Comment from './comment';

function createCandidate(status, args) {
  switch (status) {
    case 'Interviewee':
      return new Interviewee(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
        args.tags,
        args.interviewDate,
        args.resume
      );
    case 'Student':
      return new Student(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
        args.tags,
        args.groupName,
        args.startingDate,
        args.endingDate
      );
    case 'Trainee':
      return new Trainee(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
        args.tags,
        args.mentor
      );
    default:
      return new Candidate(
        args.id,
        args.name,
        args.birthDate,
        args.email,
        args.comments,
        args.tags,
      );
  }
}

function writeCandidate(candidate) {
  return ('status: ' + (candidate.status ? candidate.status : candidate.constructor.name));
}

function checkCandidateValidation(candidate) {
  const email = /.+@.+\..+/i;
  return (candidate.name && candidate.name.trim() !== '' && candidate.email && email.test(candidate.email));
}

module.exports = {Candidate, Interviewee, Student, Trainee, Comment, createCandidate, writeCandidate, checkCandidateValidation};

