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

app.get('*', function (req, res) {
  res.sendFile(path.join(__dirname, '..', 'public', 'index.html'));
});

app.use(express.static(path.join(__dirname, '..', 'public')));

connect();

app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'));
  console.log('Waiting for webpack...');
});