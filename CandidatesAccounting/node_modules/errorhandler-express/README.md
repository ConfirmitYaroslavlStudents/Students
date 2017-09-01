# errorhandler-express

An express middleware module to generate HTTP responses on error.

TODO: Pending add more checks, test and documentation

## Installation

The best way to install it is using **npm**

```sh
npm install errorhandler-express --save
```

## Loading

```js
var apiSetter = require('errorhandler-express');
```

## Initialization and Usage

Basic usage (you can see how works better with [test]():

```js
    app = express();
    var options = {
        errors : {
            app : {
                user : {
                    myOwnError : {
                        status : 401,
                        msg : "you can't see me"
                    }
                }
            }
        }
    };
    var kError = require('../index')(options);

    app.get('/error1', function (req, res, next) {
        next(kError.createError("http.badRequest"));
    });

    app.get('/error2', function (req, res, next) {
        next(kError.createError("app.user.myOwnError", {detail: new Date()}));
    });


    app.use(kError.errorHandling)

```


## Support

This plugin is proudly supported by [Kubide](http://kubide.es/) [hi@kubide.es](mailto:hi@kubide.es)

