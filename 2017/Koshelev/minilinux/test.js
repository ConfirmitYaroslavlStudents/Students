const {
	assert
} = require('chai');
const {
	core
} = require('./index.js');
const {
	rmdir
} = require('fs');

const branches = [{
	operations: [{
		command: 'helloWorld',
		args: []
	}, {
		command: 'log',
		args: ['how', 'are', 'you?']
	}]
}, {
	condition: 'log',
	operationsTrue: [{
		command: 'log',
		args: ['nice'],
	}],
	operationFalse: [{
		command: 'log',
		args: ['badd(']
	}],
	isAlways: true
}]

describe('Functions exists', function() {
	it('log', function() {
		const branches = [{
			operations: [{
				command: 'log',
				args: ['work']
			}]
		}];
		const results = core(branches);
		assert.deepEqual(results, [true]);
	});
	it('hello', function() {
		const branches = [{
			operations: [{
				command: 'helloWorld',
				args: []
			}]
		}];
		const results = core(branches);
		assert.deepEqual(results, [true]);
	});
});

describe('Function chain', function() {
	it('log-hello', function() {
		const branches = [{
			operations: [{
				command: 'log',
				args: ['work']
			},
			{
				command: 'helloWorld',
				args: []	
			}]
		}];
		const results = core(branches);
		assert.deepEqual(results, [true, true]);
	})
});

describe('Broken chain', function() {
	it('log-some-hello', function() {
		const branches = [{
			operations: [{
				command: 'log',
				args: ['work']
			},
			{
				command: 'some'
			},
			{
				command: 'helloWorld',
				args: []	
			}]
		}];
		const results = core(branches);
		assert.deepEqual(results, [true, false, null]);
	})
});

describe('Broken chain with !important', function() {
	it('log-some-!hello', function() {
		const branches = [{
			operations: [{
				command: 'log',
				args: ['work']
			},
			{
				command: 'some'
			},
			{
				command: 'helloWorld',
				args: [],
				isAlways: true
			}]
		}];
		const results = core(branches);
		assert.deepEqual(results, [true, false, true]);
	})
});

describe('Chain with fasle branch', function() {
	it('log-some-ifsome-hello', function() {
		const branches = [{
			operations: [{
				command: 'log',
				args: ['work']
			},
			{
				command: 'some'
			}]
		},
		{
			operationsFalse: [{
				command: 'helloWorld'
			}],
			condition: 'some'
		}];
		const results = core(branches);
		assert.deepEqual(results, [true, false, true]);	
	})
});

describe('Chain with true branch', function() {
	it('log-iflog-hello', function() {
		const branches = [{
			operations: [{
				command: 'log',
				args: ['work']
			}]
		},
		{
			operationsTrue: [{
				command: 'helloWorld'
			}],
			condition: 'log'
		}];
		const results = core(branches);
		assert.deepEqual(results, [true, true]);	
	})
});

describe('Branch not important - didn\'t execute', function(){
	it('some-log-iflog-hello', function(){
		const branches = [{
			operations: [{
				command: 'some'
			},
			{
				command: 'log',
				args: ['work']
			}]
		},
		{
			operationsFalse: [{
				command: 'helloWorld'
			}],
			condition: 'log'
		}];
		const results = core(branches);
		assert.deepEqual(results, [false, null]);
	})
});

describe('Branch important - execute', function(){
	it('some-log-!iflog-hello', function(){
		const branches = [{
			operations: [{
				command: 'some'
			},
			{
				command: 'log',
				args: ['work']
			}]
		},
		{
			operationsFalse: [{
				command: 'helloWorld'
			}],
			condition: 'log',
			isAlways: true
		}];
		const results = core(branches);
		assert.deepEqual(results, [false, null, true]);
	})
});