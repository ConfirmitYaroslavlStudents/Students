let assert = require('chai').assert;
let findFiles = require('../lib/filesHandling.js').findFiles;

describe('filesFind', function() {
	let getFiles, isFile;
	beforeEach(function() {
		isFile = function isFile(directory, file) {
			if (file == 'home')
				return false;
			return true;
		};
		getFiles = function getFiles(directory) {
			if (directory == '.')
				return [
					'home',
					'bla.mp3',
					'bla.mp'
				];
			return ['blabla.mp3', 'bal.jp'];
		}
	});

	describe('non-recursive', function() {
		it('bla.mp3', function() {
			let result = findFiles('^.*mp3$', {
				getFiles: getFiles,
				isFile: isFile,
				directory: '.'
			});
			assert.deepInclude(result, './bla.mp3');
		});
	});

	describe('recursive', function() {
		it('bla.mp3, blabla.mp3', function() {
			let result = findFiles('^.*mp3$', {
				getFiles: getFiles,
				isFile: isFile,
				directory: '.',
				recursive: true
			});

			assert.deepInclude(result, './bla.mp3');
			assert.deepInclude(result, './home/blabla.mp3');
		});
	});
})