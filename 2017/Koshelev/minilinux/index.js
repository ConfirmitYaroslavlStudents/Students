function core(branches) {
	const {
		commands
	} = require('./commands');
	let error = false;
	let results = [];
	let main = branches[0].operations;

	for (let i = 0; i < main.length; i++) {
		let command = main[i].command;
		let args = main[i].args;
		let isAlways = main[i].isAlways;

		// true - executed successful
		// false - executed unsuccessful
		// null - didn't execute
		try {
			if (isAlways || !error) {
				call(command, args);
				results.push(true);
			} else {
				results.push(null);
			}
		} catch (e) {
			results.push(false);
			error = true;
		}
		branching(command);
	}

	return results;

	function call(command, args) {
		commands[command].apply(null, args);
	}

	function branching(command) {
		for (let i = 1; i < branches.length; i++) {
			if (branches[i].condition == command) {
				const result = results.slice(-1)[0];
				if (result !== null || branches[i].isAlways) {
					if (result) {
						main = main.concat(branches[i].operationsTrue);
						error = false;
					} else {
						main = main.concat(branches[i].operationsFalse);
						error = false;
					}
				}
			}
		}
	}
}

module.exports = {
	core
};