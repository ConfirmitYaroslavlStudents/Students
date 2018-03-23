import express from 'express';
import webpack from 'webpack';
import config from '../webpack.config';
import path from 'path';
import bodyParser from 'body-parser';
import fileUpload from 'express-fileupload';
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
import {getResume, addResume, getAttachment, addAttachment} from './mongoose';

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
app.use(fileUpload());
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
  graphiql: false,
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
          res.json({username: req.user.username});
        });
      });
    } else {
      passport.authenticate('local')(req, res, function () {
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

app.get('/interviewees/:intervieweeID/resume', function(req, res) {
  return getResume(req.params.intervieweeID).then((result, error) => {
    if (error) {
      return res.status(500).end();
    }
    res.attachment(result.resumeName);
    res.send(result.resumeData);
  });
});

app.post('/interviewees/:intervieweeID/resume', function(req, res) {
  if (!req.isAuthenticated()) {
    return res.status(401).end();
  }
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > 16000000) {
    return res.status(500).end();
  }
  let file = req.files[Object.keys(req.files)[0]];
  return addResume(req.params.intervieweeID, file.name, file.data).then((result, error) => {
    if (error) {
      return res.status(500).end();
    }
    res.end();
  });
});

app.get('/:candidateStatus(interviewees|students|trainees)/:candidateID/comments/:commentID/attachment', function(req, res) {
  return getAttachment(req.params.candidateID, req.params.commentID,).then((result, error) => {
    if (error) {
      return res.status(500).end();
    }
    res.attachment(result.attachmentName);
    res.send(result.attachmentData);
  });
});

app.post('/:candidateStatus(interviewees|students|trainees)/:candidateID/comments/:commentID/attachment', function(req, res) {
  if (!req.isAuthenticated()) {
    return res.status(401).end();
  }
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > 16000000) {
    return res.status(500).end();
  }
  let file = req.files[Object.keys(req.files)[0]];
  return addAttachment(req.params.candidateID, req.params.commentID, file.name, file.data).then((result, error) => {
    if (error) {
      return res.status(500).end();
    }
    res.end();
  });
});

app.get('*', function (req, res) {
  res.sendFile(path.join(__dirname, '..', 'public', 'index.html'));
  //TODO: отправить авторизационную информацию вместе с index.html. Использовать templates
});

app.use(express.static(path.join(__dirname, '..', 'public')));

connect();

app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'));
  console.log('Waiting for webpack...');
});