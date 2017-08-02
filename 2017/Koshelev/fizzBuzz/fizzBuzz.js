Number.prototype.isDividable = function (number){
	return this % number == 0;
}

class FizzBuzz{
	
	constructor(options){
		this._register = options || {
			'3': 'Fizz',
			'5': 'Buzz'
		};
		
		if (!this._register['3']){
			this._register['3'] = 'Fizz';
		}

		if (!this._register['5'])
			this._register['5'] = 'Buzz';
		
	}

	process(number){
		let result = '';
		let register = this._register;

		for (let key in register){
			if (number.isDividable(key))
			result += register[key];
		}

		if (!result)
			return number.toString();
		return result;
	}

	add(options){
		for(let key in options){
			this._register[key] = options[key];
		}
	}
}

module.exports = FizzBuzz;