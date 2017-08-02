const fs = require("fs");



function findFiles(mask, options) {
	let exp = new RegExp(mask);
	
	let getFiles = options.getFiles || fs.readdirSync;
	let isFile = options.isFile || function isFile(directory, file) {
		let stats = fs.statSync(directory + "/" + file);
		return stats.isFile();
	};

	let recursive = options.recursive;
	let directory = options.directory || process.cwd();

	let filesFound = [];
	let files = getFiles(directory);

	for (let i = 0; i < files.length; i++) {

		let file = isFile(directory, files[i]);

		if (file && exp.test(files[i])) {
			filesFound.push(directory + "/" + files[i]);
		}

		if (recursive && !file) {
			subDirectoryFiles = findFiles(mask, {
				recursive: true,
				directory: directory + "/" + files[i],
				isFile: isFile,
				getFiles: getFiles
			});

			subDirectoryFiles.forEach(function(item) {
				filesFound.push(item);
			})

		}
	}

	return filesFound;
}

exports.findFiles = findFiles;