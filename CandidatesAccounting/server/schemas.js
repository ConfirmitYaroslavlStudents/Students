import mongoose from 'mongoose';

const Schema = mongoose.Schema;

export const AccountSchema = new Schema({
  username: String,
  password: String
});

export const CandidateSchema = new Schema({
    status: String,
    name: {
      type: String,
      required: true
    },
    email: {
      type: String,
      required: true,
      match: /.+@.+\..+/i
    },
    birthDate: String,
    comments: [{
      author: {
        type: String,
        required: true
      },
      date: {
        type: String,
        required: true
      },
      text: {
        type: String,
        required: true
      }
    }],
    tags: [String],
    interviewDate: String,
    resume: String,
    groupName: String,
    startingDate: String,
    endingDate: String,
    mentor: String
  });

export const IntervieweeSchema = new Schema({
    status: String,
    name: {
      type: String,
      required: true
    },
    email: {
      type: String,
      required: true,
      match: /.+@.+\..+/i
    },
    birthDate: String,
    comments: [{
      author: {
        type: String,
        required: true
      },
      date: {
        type: String,
        required: true
      },
      text: {
        type: String,
        required: true
      }
    }],
    tags: [String],
    interviewDate: String,
    resume: String,
  });

export const StudentSchema = new Schema({
    status: String,
    name: {
      type: String,
      required: true
    },
    email: {
      type: String,
      required: true,
      match: /.+@.+\..+/i
    },
    birthDate: String,
    comments: [{
      author: {
        type: String,
        required: true
      },
      date: {
        type: String,
        required: true
      },
      text: {
        type: String,
        required: true
      }
    }],
    tags: [String],
    groupName: String,
    startingDate: String,
    endingDate: String
  });

export const TraineeSchema = new Schema({
    status: String,
    name: {
      type: String,
      required: true
    },
    email: {
      type: String,
      required: true,
      match: /.+@.+\..+/i
    },
    birthDate: String,
    comments: [{
      author: {
        type: String,
        required: true
      },
      date: {
        type: String,
        required: true
      },
      text: {
        type: String,
        required: true
      }
    }],
    tags: [String],
    mentor: String
  });

export const TagSchema = new Schema({
    title: {
      type: String,
      required: true
    }
  });