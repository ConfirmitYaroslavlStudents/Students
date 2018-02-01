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
//app.use(express.static(path.join(__dirname, '..', 'public')));

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
  graphiql: false,
}));

app.use(favicon(path.join(__dirname, '..', 'public', 'favicon.ico')));
app.use(cookieParser());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(expressSession({ secret: 'SECRET', resave: true, saveUninitialized: true }));


passport.use(new passportLocal.Strategy(Account.authenticate()));
passport.serializeUser(Account.serializeUser());
passport.deserializeUser(Account.deserializeUser());
app.use(passport.initialize());
app.use(passport.session());

app.get('/', function (req, res) {
  console.log(req.isAuthenticated());
  res.sendFile(path.join(__dirname, '..', 'public', 'index.html'));
});

app.post('/register', function(req, res) {
  return connect().then(() => {
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
    })
});


app.post('/login', function(req, res) {
  return connect()
    .then(() => {
      passport.authenticate('local')(req, res, function () {
        res.redirect('/');
      });
    });
});

app.get('/ping', function(req, res){
  res.status(200).send("pong!");
});



/*

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
*/

app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'));
  console.log('Waiting for webpack...');
});