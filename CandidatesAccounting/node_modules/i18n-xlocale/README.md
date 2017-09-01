# cors-express

Middleware to control i18n middleware based on a X-Locale header

Can see here more info about [i18n original middleware](https://www.npmjs.com/package/i18n)

## Installation

The best way to install it is using **npm**

```sh
npm install 18n-xlocale --save
```

## Loading

```js
var i18n = require('18n-xlocale');
```

## Initialization and Usage

```js
var app = express(),
    options = {};

i18n.configure(options)
app.use(i18n.use);
```

## Options

You can see all available options in the original package: [i18n](https://www.npmjs.com/package/i18n)

## Support

This plugin is proudly supported by [Kubide](http://kubide.es/) [hi@kubide.es](mailto:hi@kubide.es)

