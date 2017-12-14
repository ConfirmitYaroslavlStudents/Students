import express from 'express';
import webpack from 'webpack';
import config from '../webpack.config';
import http from 'http';
import path from 'path';
import bodyParser from 'body-parser';
import favicon from 'serve-favicon';
import webpackDevMiddleware from 'webpack-dev-middleware';
import webpackHotMiddleware from 'webpack-hot-middleware';
import graphqlHTTP from 'express-graphql';
import {schema, root} from './graphQL';

const app = express();
const compiler = webpack(config);

app.set('port', 3000);
app.set('view endine', 'ejs');

app.use(webpackDevMiddleware(compiler, {
  publicPath: config.output.publicPath,
  hot: true,
  stats: {
    colors: true
  }
}));
app.use(webpackHotMiddleware(compiler));
app.use('/graphql', graphqlHTTP({
  schema: schema,
  rootValue: root,
  graphiql: true,
}));
app.use(favicon(path.join(__dirname, '..', 'public', 'favicon.ico')));
app.use(express.static(path.join(__dirname, '..', 'public')));
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

http.createServer(app).listen(app.get('port'), function() {
  console.log('Express server is listening on port', app.get('port'));
  console.log('Waiting for webpack...');
});

/*
const Schema = mongoose.Schema;

mongoose.connect('mongodb://localhost:27017/CandidateAccounting');
mongoose.Promise = global.Promise;

const CandidateSchema = new Schema({
  status: String,
  name: String,
  email: String,
  birthDate: String,
  comments: [{author: String, date: String, text: String}],
  tags: [],
  intervieweeDate: String,
  resume: String,
  groupName: String,
  startingDate: String,
  endingDate: String,
  mentorName: String
});

const Candidate = mongoose.model('Candidate', CandidateSchema);

const db = mongoose.connection;
db.on('error', console.error.bind(console, 'connection error:'));
db.once('open', function() {
  console.log('MongoDB is connected');
});
*/
app.get('/*', (req, res) => {
  res.sendFile(path.join(__dirname, '..', 'public', 'index.html'));
});