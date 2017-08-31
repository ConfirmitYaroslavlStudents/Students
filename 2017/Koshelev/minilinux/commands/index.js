let commands = {};
commands.log = console.log;
commands.helloWorld = require('./helloWorld.js');
commands['?'] = require('./if.js');
module.exports = {
	commands
}