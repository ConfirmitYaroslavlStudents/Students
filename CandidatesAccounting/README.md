CandidateAccounting
=====================
Usage
-----------------------------------
### Setting up MongoDB
1. Create database `CandidateAccounting` _(configurable)_;
2. Create three collections in the database:
    * `accounts`;
    * `candidates`;
    * `tags`.
3. Database will be automatically connected when CandidateAccounting server starts.

### NPM Scripts

`npm run build` - builds server and client applications into `dist` folder.

`npm run prod` - starts `npm run build` script and then starts CandidateAccounting server in `production mode` from `dist` folder.

`npm run start:client` - builds client in development mode and starts dev server.

`npm run start:server` - builds and starts server in development mode.

Configuration
-----------------------------------
### Server configuration file
There are two server configuration files: `development` and `production`.

Location:
`_{project directory}_/server/development.server.config.js`
`_{project directory}_/server/production.server.config.js`

Example:
```
const serverConfig = {
  assetsRoot: '/assets/',
  port: 3000,
  databaseConnectionURL: "mongodb://localhost:27017/CandidateAccounting",
  authorization: {
      allowedLogins: [],
      sessionSecret: 'secret'
    }
}

module.exports = serverConfig
```
`assetsRoot`: absolute or relative path to assets (webpack output and static files).

`port` _(number, default: 3000)_ - server port number.

`databaseConnectionURL` _(string, default: "mongodb://localhost:27017/CandidateAccounting")_ - MongoDB connection URL.

`allowedLogins` _(array, default: empty)_ - login list for users allowed to register and login. If empty, all users are allowed to register and login.

`sessionSecret` _(string, default: "secret")_ - secret session key used for authorization system.