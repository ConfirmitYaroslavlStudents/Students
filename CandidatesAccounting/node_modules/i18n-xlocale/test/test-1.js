'use strict';
var chai = require("chai"),
    supertest = require('supertest'),
    should = chai.should(),
    express = require('express'),
    i18n = require('../index'),
    app,
    options = {
        "defaultLocale": "en",
        "locales": [
            "en",
            "de"
        ],
        "directory": __dirname
    };

describe('basic usage', function () {
    //setup server
    before(function(){
        app = express();
        i18n.configure(options);
        app.use(i18n.use);
        app.get('/', function (req, res) {
            res.json(i18n.__('test sentence'));
        });
    });

    it('get default language', function (done) {
        supertest(app)
            .get("/")
            .send()
            .set('Accept', 'application/json')
            .expect(200)
            .end(function (err, res) {
                should.not.exist(err);
                res.headers['x-locale'].should.be.equal('en');
                res.body.should.be.equal('test sentence');
               done();
            });
    });

    it('get "de" language', function (done) {
        supertest(app)
            .get("/")
            .send()
            .set('Accept', 'application/json')
            .set('x-locale', 'de')
            .expect(200)
            .end(function (err, res) {
                should.not.exist(err);
                res.headers['x-locale'].should.be.equal('de');
                res.body.should.be.equal('testurteil');
                done();
            });
    });

});

