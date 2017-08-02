const Bench = require('./middleWare.js').Bench;
const CheckPermission = require('./middleWare.js').CheckPermission;
const fs = require('fs');
const readID3Tags = require("read-id3-tags");
const getNewPath = require('./pathsHandling.js').getNewPath;
const findFiles = require('./filesHandling.js').findFiles;

function rename(path) {
	const fileStream = fs.createReadStream(path);

	readID3Tags.readID3Tags(path).then((data) => {

		let newPath = getNewPath(path, {
			data: data
		});
		fs.rename(path, newPath);

	}).catch(err => {
		console.log(err);
	});
}

exports.renameByTag = function(mask, options) {
	let filesForRename = findFiles(mask, options);
	const bench = new Bench({
		next: rename.bind(rename)
	});
	const chain = new CheckPermission({
		next: bench.go.bind(bench)
	});

	filesForRename.forEach(chain.go.bind(chain));
}