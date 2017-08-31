function main() {
	var args = process.argv.slice(2);
	const {
		commands
	} = require('./commands');
	const {
		core
	} = require('./index.js');
	let branches = {};
	branches.args = [];
	let k = 0,
		checkout;
	for (let i = 0; i < args.length; i++) {
		let isCommand = args[i].slice(0, 2) == '--';
		if (isCommand) {
			if (branches.command) {
				branches.next = {};
				branches.next.prev = branches;
				branches = branches.next;
				branches.args = [];
			}
			if (~args[i].indexOf('?')) {
				branches.command = '?';
				branches.args[0] = {};
				branches.args[0].prev = branches;
				branches.args[0].args = [];
				branches.args[1] = {};
				branches.args[1].prev = branches;
				branches.args[1].args = [];
				let back = +args[i].slice(args[i].indexOf('~') + 1);

				let condition, j;
				for (condition = branches, j = 0; j < back; j++, condition = condition.prev) {};
					
				branches.args[2] = condition;
				branches.args[3] = branches;

				checkout = branches.args[1];
				branches = branches.args[0];

			} else {
				if (~args[i].indexOf(':')) {
					branches = checkout;
				} else {
					if (args[i][2] == '!') {
						branches.isAlways = true;
						branches.command = args[i].slice(3);
					} else {
						branches.command = args[i].slice(2);
					}
				}
			}
		} else {
			branches.args.push(args[i]);
		}
	}
	while (branches.prev) {
		branches = branches.prev;
	}
	core(branches);
}

module.exports = main;