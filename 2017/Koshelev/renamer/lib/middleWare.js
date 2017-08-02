const fs = require('fs');

class Bench {

	constructor(options) {
		if (options) {
			this._next = options.next;
			this._logger = options.logger || console;
		} else {
			this._logger = console;
		}
	}

	go() {
		let start = Date.now();
		this._next.apply(this._next, arguments);
		this._logger.log(Date.now() - start + 'ms');
	}
}

class CheckPermission {

	constructor(options) {
		if (options) {
			this._next = options.next;
			this._mode = options.mode || fs.constants.R_OK | fs.constants.W_OK;
		} else {
			this._mode = fs.constants.R_OK | fs.constants.W_OK;
		}
	}

	go(path) {
		try {
			fs.accessSync(path, this.mode);
		} catch (err) {
			console.log(err);
			return;
		}
		console.log('nice');

		this._next.apply(this._next, arguments);
	}
};

module.exports = {
	CheckPermission,
	Bench
};