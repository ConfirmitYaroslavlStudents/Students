let colors, pkg, print;
colors = require("colors");
pkg = require("../package.json");
let fs = require("fs");
let readID3Tags = require("read-id3-tags");
let ID3Writer = require("browser-id3-writer");
let stringHandlers = require('./stringHandling.js');

let retag = function (path) {
	let newTags = stringHandlers.getTags(path);

	setTags(path, newTags);
}

let rename = function (path) {

	const fileStream = fs.createReadStream(path);

	readID3Tags.readID3Tags(path).then((data) => {

		let newPath = stringHandlers.getNewPath(path, {
				data: data
			});
		fs.rename(path, newPath);

	}).catch(err => {
		console.log(err);
	});
}

rename = bench(rename);
rename = checkPermission(rename);

retag = bench(retag);
retag = checkPermission(retag);

function checkPermission(func, mode){
	mode = mode || fs.constants.R_OK | fs.constants.W_OK;
	return function(){
		try{
			fs.accessSync(arguments[0], mode);
		} catch(err) {
			return;
		}
		console.log('nice');
		func.apply(this, arguments);
	}
}

function bench(func, logger){
	logger = logger || console;
	return function(){
		let start = Date.now();
		func.apply(this, arguments);
		logger.log(Date.now() - start + "ms");
	}
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

function findFiles(mask, options) {
	mask = stringHandlers.maskHandler(mask);
	let exp = new RegExp(mask);

	let recursive = options.recursive;
	let directory = options.directory || process.cwd();

	let filesFound = [];
	let files = fs.readdirSync(directory);

	for (let i = 0; i < files.length; i++) {

		let stats = fs.statSync(directory + "/" + files[i]);

		if (stats.isFile() && exp.test(files[i])) {
			filesFound.push(directory + "/" + files[i]);
		}

		if (recursive && stats.isDirectory()) {
			subDirectoryFiles = findFiles(mask, {
				recursive: true,
				directory: directory + "/" + files[i]
			});

			subDirectoryFiles.forEach(function(item) {
				filesFound.push(item);
			})

		}
	}

	return filesFound;
}

exports.retagByName = function(mask, options) {
	var filesForRetag = findFiles(mask, options);

	filesForRetag.forEach(retag);

}

exports.renameByTag = function(mask, options) {
	var filesForRename = findFiles(mask, options);

	filesForRename.forEach(rename);
}