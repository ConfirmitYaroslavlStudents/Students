const helloWorld = function(){
		const { spawnSync } = require('child_process');
		let hello = spawnSync(__dirname + '/helloWorld.sh');
		console.log(hello.stdout.toString());		
	}
	helloWorld();
module.exports = helloWorld;