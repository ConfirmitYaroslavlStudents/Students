import express from 'express';
import webpack from 'webpack';
import config from '../webpack.config';
import path from 'path';
import bodyParser from 'body-parser';
import cookieParser from 'cookie-parser';
import favicon from 'serve-favicon';
import webpackDevMiddleware from 'webpack-dev-middleware';
import webpackHotMiddleware from 'webpack-hot-middleware';
import graphqlHTTP from 'express-graphql';
import {schema, root} from './graphQL';
import {Account, connect} from './mongoose';
import expressSession  from 'express-session';
import passport from 'passport';
import passportLocal from 'passport-local';

import mongoose from 'mongoose';
import fs from 'fs';

let gridFs = null;

const app = express();

app.set('port', 3000);
app.set('view endine', 'ejs');

const compiler = webpack(config);
app.use(webpackDevMiddleware(compiler, {
  publicPath: config.output.publicPath,
  hot: true,
  stats: {
    colors: true
  }
}));
app.use(webpackHotMiddleware(compiler));

app.use(favicon(path.join(__dirname, '..', 'public', 'favicon.ico')));
app.use(expressSession({ secret: 'yDyTP3T3Dvc4206O8pm', resave: false, saveUninitialized: false }));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(passport.initialize());
app.use(passport.session());

passport.use(new passportLocal.Strategy(Account.authenticate()));
passport.serializeUser(Account.serializeUser());
passport.deserializeUser(Account.deserializeUser());

app.use('/graphql', function(req, res, next) {
  if (req.isAuthenticated() || !req.body || !req.body.query || !req.body.query.includes('mutation')) {
    next();
  } else {
    res.status(401).end();
  }
});

app.use('/graphql', graphqlHTTP({
  schema: schema,
  rootValue: root,
  graphiql: true,
}));

app.get('/login', function(req, res) {
  if (req.isAuthenticated()) {
    res.json({username: req.user.username});
  } else {
    res.json({username: ''});
  }
});

app.post('/login', function(req, res) {
  return Account.findOne({username: req.body.username}, function (findError, user) {
    if (findError) {
      return res.status(401).end();
    }
    if (!user) {
      Account.register(new Account({username: req.body.username}), req.body.password, function (registerError, account) {
        if (registerError) {
          return res.status(401).end();
        }
        passport.authenticate('local')(req, res, function () {
          console.log('User signed on:', req.user.username);
          res.json({username: req.user.username});
        });
      });
    } else {
      passport.authenticate('local')(req, res, function () {
        console.log('User signed in:', req.user.username);
        res.json({username: req.user.username});
      });
    }
  });
});

app.get('/logout', function(req, res){
  req.logout();
  req.session.destroy(function (err) {
    res.redirect('/');
  });
});

app.get('/resume/*', function(req, res) {
  let file = req.url.split('/')[2];
  let fileName = file.split('.')[0];
  let fileType = file.split('.')[1];
  console.log(fileName, fileType);
  res.setHeader('Content-Type', 'application/' + fileType);
  res.setHeader('Content-Disposition', 'attachment; filename=' + file); //TODO: ask how would it be better
  gridFs.model.readById(fileName, function(error, content){
    if (error) {
      console.log(error);
      return res.status(500).end();
    }
    console.log(content);
    res.send(content);
  });
});

app.put('/resume*', function(req, res) {
  let file = req.headers['Content-Disposition'].split('=')[1];
  let fileType = file.split('.')[1];
  gridFs.model.write({
      filename: file,
      contentType:'application/' + fileType
    },
    req.body,
    function(error, createdFile){
      if (error) {
        console.log(error);
        return res.status(500).end();
      }
      res.json({resumeID: createdFile._id});
    });
});

app.get('*', function (req, res) {
  res.sendFile(path.join(__dirname, '..', 'public', 'index.html'));
});

app.use(express.static(path.join(__dirname, '..', 'public')));

connect().then(() => {
  gridFs = require('mongoose-gridfs')({
    collection:'attachments',
    model:'Attachment',
    mongooseConnection: mongoose.connection
  });
});


app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'));
  console.log('Waiting for webpack...');
});