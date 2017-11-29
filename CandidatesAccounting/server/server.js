import express from 'express';
import webpack from 'webpack';
import config from '../webpack.config';
import http from 'http';
import path from 'path';
import bodyParser from 'body-parser';
import favicon from 'serve-favicon';
import webpackMiddleware from 'webpack-dev-middleware';
import webpackHotMiddleware from 'webpack-hot-middleware';
import graphqlHTTP from 'express-graphql';
import {buildSchema} from 'graphql';
import {database, addCandidate, updateCandidate, deleteCandidate, addComment, deleteComment} from './database';

const app = express();
const compiler = webpack(config);

// Construct a schema, using GraphQL schema language
let schema = buildSchema(`
  type Query {
    hello: String,
    text: String
  }
`);

// The root provides a resolver function for each API endpoint
let root = {
  hello: () => {
    return 'Hello world!';
  },
  text: () => {
    return 'Text!';
  },
};

app.set('port', 3000);
app.set('view endine', 'ejs');

app.use(webpackMiddleware(compiler, {
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

app.get('/api/candidates', (req, res) => {
  console.log('GET: all candidates');
  res.send(database.candidates);
});

app.get('/api/tags', (req, res) => {
  console.log('GET: all tags');
  res.send(database.tags);
});

app.post('/api/candidates', (req, res) => {
  let candidate = req.body.data;
  addCandidate(candidate);
  console.log('POST:', 'add new candidate:', candidate);
  res.end();
});

app.put('/api/candidates/:id', (req, res) => {
  let candidate = req.body.data;
  updateCandidate(parseInt(req.params.id), candidate);
  console.log('PUT:', 'update candidate:', candidate);
  res.end();
});

app.delete('/api/candidates/:id', (req, res) => {
  deleteCandidate(parseInt(req.params.id));
  console.log('DELETE:', 'delete candidate', 'id:', req.params.id);
  res.end();
});

app.post('/api/candidates/:candidateId/comments', (req, res) => {
  addComment(parseInt(req.params.candidateId), req.body.data);
  console.log('POST:', 'add new comment:', 'id:', req.params.candidateId, 'comment:', req.body.data);
  res.end();
});

app.delete('/api/candidates/:candidateId/comments/:commentId', (req, res) => {
  deleteComment(parseInt(req.params.candidateId), parseInt(req.params.commentId));
  console.log('DELETE:', 'delete comment:', 'id:', req.params.candidateId, 'commentID:', req.params.commentId);
  res.end();
});

app.get('/*', (req, res) => {
  res.sendFile(path.join(__dirname, '..', 'public', 'index.html'));
});