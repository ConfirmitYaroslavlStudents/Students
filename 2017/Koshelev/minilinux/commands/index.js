let commands = {};
commands.log = console.log;
commands.helloWorld = require('./helloWorld.js');
console.log(commands.helloWorld);
commands.helloWorld();
module.exports = {
	commands
}