const serverConfig = {
  assetsRoot: '/assets/',
  port: 3000,
  databaseConnectionURL: 'mongodb://localhost:27017/CandidateAccounting',
  authorization: {
    allowedLogins: [],
    sessionSecret: '3T3Dvc4206O8pRmKHIUEKsBtevrZ88qU7Bb6L4ptwfrdLGZgPBnBSe0I9ShkSFyDyTPp8YYtKnl123n334hkqOd1'
  }
}

module.exports = serverConfig