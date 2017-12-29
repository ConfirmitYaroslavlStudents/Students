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

app.use('/graphql', graphqlHTTP({
  schema: schema,
  rootValue: root,
  graphiql: true,
}));

app.use(favicon(path.join(__dirname, '..', 'public', 'favicon.ico')));

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cookieParser());

app.use(expressSession({
  secret: 'hello-i-am-a-secret-string',
  resave: false,
  saveUninitialized: false
}));

passport.use(new passportLocal.Strategy(Account.authenticate()));
passport.serializeUser(Account.serializeUser());
passport.deserializeUser(Account.deserializeUser());
app.use(passport.initialize());
app.use(passport.session());

app.use(express.static(path.join(__dirname, '..', 'public')));

app.post('/login', function(req, res) {
  return connect()
    .then(() => {
      Account.findOne({username: req.body.username}, function (err, user) {
        if (err) {
          console.log(err);
          res.status(401).end();
          return;
        }
        if (!user) {
          Account.register(new Account({username: req.body.username}), req.body.password, function (err, account) {
            if (err) {
              console.log(err);
              res.status(401).end();
              return;
            }
            passport.authenticate('local')(req, res, function () {
              res.redirect('/');
            });
          });
        } else {
          passport.authenticate('local')(req, res, function () {
            res.redirect('/');
          });
        }
      });
    });
});

app.get('/logout', function(req, res){
  req.logout();
  res.redirect('/');
});

app.get('*', function (req, res) {
  res.sendFile(path.join(__dirname, '..', 'public', 'index.html'));
});

app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'));
  console.log('Waiting for webpack...');
});