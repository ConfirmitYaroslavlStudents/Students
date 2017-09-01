/* eslint-disable no-unused-vars */
/* eslint-disable no-undef */

const chai = require('chai');
const supertest = require('supertest');
const express = require('express');
const debug = require('debug')('kubide:errorhandler:test');
const mongoose = require('mongoose');

const Schema = mongoose.Schema;
const should = chai.should();

process.env.NODE_ENV = 'production';
const errorHandler = require('../.');

describe('basic usage', () => {
  // setup server
  before(() => {
    mongoose.connect('mongodb://localhost/errorhandler');

    app = express();
    options = {
      errorhandler: {
        app: {
          user: {
            myOwnError: {
              status: 401,
              message: "you can't see me",
            },
          },
        },
      },
    };


    app.get('/error1', (req, res, next) => {
      next('http.badRequest');
    });

    app.get('/error2', (req, res, next) => {
      next('app.user.myOwnError');
    });

    app.get('/error3', (req, res, next) => {
      const ToySchema = new Schema({
        color: String,
        name: String,
      });

      const Toy = mongoose.model('Toy', ToySchema);

      Toy.schema.path('color').validate(value => /blue|green|white|red|orange|periwinkel/i.test(value), 'Invalid color');
      Toy.schema.path('name').validate(value => /blue|green|white|red|orange|periwinkel/i.test(value), 'Invalid string');

      const toy = new Toy({ color: 'grease', name: 'my name' });

      toy.save((err) => {
        next(err);
      });
    });

    app.get('/error4', (req, res, next) => {
      next(new Error('some.error'));
    });


    app.use(errorHandler.handler(options));
  });

  it('add config error', (done) => {
    const options = {
      test: 'test',
    };

    const newConf = errorHandler.setConfig(options);
    newConf.should.have.property('errorhandler')
      .with.deep.property('app');
    newConf.should.have.property('test', 'test');

    done();
  });

  it('get a default http.badRequest error', (done) => {
    supertest(app)
      .get('/error1')
      .send()
      .set('Accept', 'application/json')
      .expect('Content-Type', /json/)
      .expect(400)
      .end((err, res) => {
        should.not.exist(err);
        res.body.should.have.property('message', 'http_error_bad_request');
        res.body.should.have.property('status', 400);
        res.body.should.have.property('code', 'http.badRequest');
        done();
      });
  });


  it('get a special error', (done) => {
    supertest(app)
      .get('/error2')
      .send()
      .set('Accept', 'application/json')
      .expect('Content-Type', /json/)
      .expect(401)
      .end((err, res) => {
        should.not.exist(err);
        res.body.should.have.property('message', 'you can\'t see me');
        res.body.should.have.property('status', 401);
        res.body.should.have.property('code', 'app.user.myOwnError');
        done();
      });
  });

  it('get a mongoose Error', (done) => {
    supertest(app)
      .get('/error3')
      .send()
      .set('Accept', 'application/json')
      .expect('Content-Type', /json/)
      .expect(400)
      .end((err, res) => {
        should.not.exist(err);
        res.body.should.have.property('message', 'Toy validation failed');
        res.body.should.have.property('status', 400);
        res.body.should.have.property('code', 'mongoose.validation.error');
        done();
      });
  });

  it('get a real Error', (done) => {
    supertest(app)
      .get('/error4')
      .send()
      .set('Accept', 'application/json')
      .expect('Content-Type', /json/)
      .expect(500)
      .end((err, res) => {
        should.not.exist(err);
        res.body.should.have.property('code', 'internalError');
        res.body.should.have.property('message', 'some.error');
        res.body.should.have.property('status', 500);
        done();
      });
  });

  it('change parse', (done) => {
    /*
    la idea es poder añadir "parseadores de datos especiales",
    imaginar que tiene un Error especial y queremos que saque cierta información para ese error??
    // debería ser description
     */
    done();
  });

  it('get a special error translate to another language', (done) => {
    const translator = function translator(text) {
      return 'translated_text';
    };

    errorHandler.setTranslator(translator);

    supertest(app)
      .get('/error1')
      .send()
      .set('Accept', 'application/json')
      .expect('Content-Type', /json/)
      .expect(400)
      .end((err, res) => {
        should.not.exist(err);
        res.body.should.have.property('message', 'translated_text');
        res.body.should.have.property('status', 400);
        res.body.should.have.property('code', 'http.badRequest');
        done();
      });
  });
});


/* eslint-enable no-unused-vars */
/* eslint-enable no-undef */
