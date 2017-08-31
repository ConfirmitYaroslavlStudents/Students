function core(branches) {
	const {
		commands
	} = require('./commands');
	let error = false;
	let results = [];

	for (let cur = branches; cur && cur.command; cur = cur.next) {
		let command = cur.command;
		let args = cur.args;
		let isAlways = cur.isAlways;

		// true - executed successful
		// false - executed unsuccessful
		// null - didn't execute
		try {
			if (isAlways || !error) {
				call(command, args);
				cur.result = true;
			} else {
				cur.result = null;
			}
		} catch (e) {
			cur.result = false;
			error = true;
			console.log(e);
		}
	}

	return results;

	function call(command, args) {
		commands[command].apply(null, args);
	}

}

module.exports = {
	core
};