"use strict";

var i18n = require("i18n");

i18n.use = function(req, res, next) {

    // Only if x-locale is set try to change the api locale
    if (req.headers["x-locale"]) {
        var locale  = req.headers["x-locale"];

        //set locale check if locale is correct and save only if it's truth
        i18n.setLocale(locale);
    }

    // set the locale in the header
    res.setHeader("X-Locale", i18n.getLocale());
    next();
};

module.exports = i18n;

    /*

module.exports = {
    configure : i18n.configure,
    __  : i18n.__,
    parent : i18n,

    use : function(req, res, next) {

        // Only if x-locale is set try to change the api locale
        if (req.headers["x-locale"]) {
            var locale  = req.headers["x-locale"];

            //set locale check if locale is correct and save only if it's truth
            i18n.setLocale(locale);
        }

        // set the locale in the header
        res.setHeader("X-Locale", i18n.getLocale());
        next();
    }
};*/