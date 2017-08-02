let assert = require('chai').assert;
let stringHandlers = require('../lib/pathsHandling.js');
require('./filesHandlingTest.js');

describe('stringHandlers', function() {

	describe('getNewPath', function() {
		it('default separator', function() {
			let path = '/home/.mp3';
			let data = {
				artist: ['The Weeknd'],
				title: 'All I Know'
			};
			let newPath = stringHandlers.getNewPath(path, {
				data: data
			});
			assert.equal(newPath, '/home/The Weeknd - All I Know.mp3');
		});

		it('custom separator', function() {
			let path = '/home/.mp3';
			let data = {
				artist: ['The Weeknd'],
				title: 'All I Know'
			};
			let newPath = stringHandlers.getNewPath(path, {
				data: data,
				separator: ': '
			});
			assert.equal(newPath, '/home/The Weeknd: All I Know.mp3');
		});

	});

	describe('getTags', function() {
		it('default separator', function() {
			let path = '/home/The Weeknd - All I Know.mp3';
			let tags = stringHandlers.getTags(path);
			assert.equal(tags.artist[0], 'The Weeknd');
			assert.equal(tags.title, 'All I Know');
		});

		it('custom separator', function() {
			let path = '/home/The Weeknd: All I Know.mp3';
			let tags = stringHandlers.getTags(path, {
				separator: ': '
			});
			assert.equal(tags.artist[0], 'The Weeknd');
			assert.equal(tags.title, 'All I Know');
		});

	});
	
});