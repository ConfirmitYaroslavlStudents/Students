const Bench = require('./middleWare.js').Bench;
const CheckPermission = require('./middleWare.js').CheckPermission;
const fs = require('fs');
const ID3Writer = require("browser-id3-writer");
const getTags = require('./pathsHandling.js').getTags;
const findFiles = require('./filesHandling.js').findFiles;

function retag(path) {
	let newTags = getTags(path);

	setTags(path, newTags);
}

function setTags(path, newTags) {
	let fd = fs.openSync(path, "rs+");
	let content = fs.readFileSync(fd);

	const writer = new ID3Writer(content);
	writer.setFrame('TIT2', newTags.title)
		.setFrame("TPE1", [newTags.artist]);
	writer.addTag();

	const taggedSongBuffer = Buffer.from(writer.arrayBuffer);
	fs.writeFileSync(fd, taggedSongBuffer);
}

exports.retagByName = function(mask, options) {
	let filesForRetag = findFiles(mask, options);
	const bench = new Bench({
		next: retag.bind(retag)
	});
	const chain = new CheckPermission({
		next: bench.go.bind(bench)
	});

	filesForRetag.forEach(chain.go.bind(chain));
}