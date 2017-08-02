let assert = require('chai').assert;
let FizzBuzz = require('./fizzBuzz');

describe('Default FizzBuzz', function(){
	let fizzBuzz;

	beforeEach(function(){
		fizzBuzz = new FizzBuzz();
	});

	it('Correct result for 1', function(){
		let result = fizzBuzz.process(1);

		assert.equal(result, '1');
	});

	it('Correct result for 2', function(){
		let result = fizzBuzz.process(2);

		assert.equal(result, '2');
	});

	it('Correct result for 3', function(){
		let result = fizzBuzz.process(3);

		assert.equal(result, 'Fizz');
	});

	it('Correct result for 5', function(){
		let result = fizzBuzz.process(5);

		assert.equal(result, 'Buzz');
	});

	it('Correct result for 10', function(){
		let result = fizzBuzz.process(10);

		assert.equal(result, 'Buzz');
	});

	it('Correct result for 15', function(){
		let result = fizzBuzz.process(15);

		assert.equal(result, 'FizzBuzz');
	});
});

describe('Custom FizzBuzz', function(){

	describe('Constructor add 2-Banana', function(){
		let fizzBuzz;
		
		beforeEach(function(){
			fizzBuzz = new FizzBuzz({
				2: 'Banana'
			});
		});

		it('Correct result for 2', function(){
			let result = fizzBuzz.process(2);

			assert.equal(result, 'Banana');
		});
		
		it('Correct result for 6', function(){
			let result = fizzBuzz.process(6);

			assert.equal(result, 'BananaFizz');
		});

		it('Correct result for 10', function(){
			let result = fizzBuzz.process(10);

			assert.equal(result, 'BananaBuzz');
		});

		it('Correct result for 30', function(){
			let result = fizzBuzz.process(30);

			assert.equal(result, 'BananaFizzBuzz');
		});
	});

	describe('Method add 2-Banana', function(){
		let fizzBuzz;

		beforeEach(function(){
			fizzBuzz = new FizzBuzz();
			fizzBuzz.add({2: "Banana"});
		});

		it('Correct result for 2', function(){
			let result = fizzBuzz.process(2);

			assert.equal(result, 'Banana');
		});
		
		it('Correct result for 6', function(){
			let result = fizzBuzz.process(6);

			assert.equal(result, 'BananaFizz');
		});

		it('Correct result for 10', function(){
			let result = fizzBuzz.process(10);

			assert.equal(result, 'BananaBuzz');
		});

		it('Correct result for 30', function(){
			let result = fizzBuzz.process(30);

			assert.equal(result, 'BananaFizzBuzz');
		});
	});
});