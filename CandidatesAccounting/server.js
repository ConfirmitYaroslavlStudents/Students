var express = require('express');
var http = require('http');
var path = require('path');
var bodyParser  = require('body-parser');
var favicon = require ('serve-favicon');

const app = express();
app.set('port', 3000);
app.set('view endine', 'ejs');

app.use(favicon(path.join(__dirname, 'public', 'assets', 'img', 'favicon.ico')));
app.use(bodyParser.urlencoded({
  extended: true
}));
/*
app.use(express.static(path.join(__dirname, 'public')));
*/
http.createServer(app).listen(app.get('port'), function() {
  console.log('Express server listening on port', app.get('port'));
});

app.get('/*', (req, res) => {
  res.sendFile(path.join(__dirname, 'public', 'index.html'))
});
/*
app.use(function(req, res, next) {
  if (req.url === '/') {
    res.end();
  } else {
    next();
  }
});

app.use(function(req, res) {
  res.status(404).send('Page Not Found');
});

app.use(function(err, req, res, next) {
  if (app.get('env') === 'development') {
    res.status(500).render('error', err);
  } else {
    res.status(500).send('Server error');
  }
});
*/