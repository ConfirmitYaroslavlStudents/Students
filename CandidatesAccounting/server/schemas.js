import mongoose from 'mongoose';

const Schema = mongoose.Schema;

export const AccountSchema = new Schema({
  username: {
    type: String,
    unique: true,
    required: true
  },
  password: String,
  notifications: [{
    recent: Boolean,
    source: {},
    content: {}
  }],
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
    subscribers: [String],
    interviewDate: String,
    resume: String,
    resumeFile: Buffer,
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
    subscribers: [String],
    interviewDate: String,
    resume: String,
    resumeFile: Buffer,
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
    subscribers: [String],
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
    subscribers: [String],
    mentor: String
  });

export const TagSchema = new Schema({
    title: {
      type: String,
      required: true
    }
  });