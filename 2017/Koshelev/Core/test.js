const { assert } = require('chai');
const { core } = require('./index.js');
const { rmdir } = require('fs');

describe('Functions exists', function(){
	it('log', function(){
		let result = core(['log'],[]);
		assert.include(result, true);
	});
	it('hello', function(){
		let result = core(['hello'],[]);
		assert.include(result, true);
	});
	it('createFolder', function(){
		let result = core(['createFolder'],['']);
		assert.include(result, false);
	});
});

describe('Function chain', function(){
	it('log-hello-createFolder', function(){
		let result = core(['log','hello','createFolder'],['some','','./dir']);
		assert.deepEqual(result, [true, true, true]);
		rmdir('./dir');
	})
});

describe('Broken chain', function(){
	it('log-some-createFolder', function(){
		let result = core(['log', 'some', 'createFolder'], ['log', '', './dir']);
		assert.deepEqual(result, [true, false, false]);
	})
});

describe('Broken chain with !important', function(){
	it('log-some-!createFolder', function(){
		let result = core(['log', 'some', '!createFolder'], ['log', '', './dir']);
		assert.deepEqual(result, [true, false, true]);
		rmdir('./dir');
	})
});

describe('Broken chain with if', function(){
	it('log-some-!ifsome-createFolder', function(){
		let result = core(['log', 'some', '!ifsome', 'createFolder'], ['log', '', '', './dir']);
		assert.deepEqual(result, [true, false, true]);
		rmdir('./dir');
	})
});

describe('Broken chain with broken branch', function(){
	it('log-some-!ifsome-some-!createFolder', function(){
		let result = core(['log', 'some', '!ifsome', 'some', '!createFolder'], ['log', '', '', '', './dir']);
		assert.deepEqual(result, [true, false, false, true]);
		rmdir('./dir');
	})
})