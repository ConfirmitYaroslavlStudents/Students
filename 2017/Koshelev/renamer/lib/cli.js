(function() {
	var colors, pkg, print;
	colors = require("colors");
	pkg = require("../package.json");
	var fs = require("fs");
	var readID3Tags = require("read-id3-tags");
	var ID3Writer = require("browser-id3-writer");

	exports.retagByName = retagByName = function(mask, recursive) {
		var filesForRetag = findFiles(mask, recursive);

		filesForRetag.forEach(function(path) {

			let fd = fs.openSync(path, "rs+");
			let content = fs.readFileSync(fd);
			const writer = new ID3Writer(content);
			let name = path.slice(path.lastIndexOf("/") + 1);
			let artist = name.slice(0, name.indexOf(' - '));
			let title = name.slice(name.indexOf(" - ") + 3, name.indexOf(".mp3"));

			writer.setFrame('TIT2', title)
				.setFrame("TPE1", [artist]);

			writer.addTag();

			const taggedSongBuffer = Buffer.from(writer.arrayBuffer);

			fs.writeFileSync(fd, taggedSongBuffer);
		});

	}

	exports.renameByTag = renameByTag = function(mask, recursive) {
		var filesForRename = findFiles(mask, recursive);

		filesForRename.forEach(function(path) {

			const filePath = path;
			const fileStream = fs.createReadStream(filePath);

			readID3Tags.readID3Tags(filePath).then((data) => {

				let newPath = filePath.slice(0, filePath.lastIndexOf("/"));
				newPath = newPath + "/" + data.artist[0] + " - " + data.title + ".mp3";
				fs.rename(filePath, newPath);

			}).catch((err) => {
				console.error(err);
			});
		});
	}

	function findFiles(mask, recursive, directory) {
		mask = maskToReg(mask);
		var exp = new RegExp(mask);
		var good = [];
		directory = directory || process.cwd();
		var filesFound = [];
		var files = fs.readdirSync(directory)

		for (var i = 0; i < files.length; i++) {

			var stats = fs.statSync(directory + "/" + files[i]);

			if (stats.isFile() && exp.test(files[i])) {
				filesFound.push(directory + "/" + files[i]);
			}

			if (recursive && stats.isDirectory()) {
				subDirectoryFiles = findFiles(mask, recursive, directory + "/" + files[i]);

				subDirectoryFiles.forEach(function(item) {
					filesFound.push(item);
				})

			}
		}

		return filesFound;
	}

	function maskToReg(mask) {

		for (var i = 0; i < mask.length; i++) {
			if (mask[i] == "*") {
				mask = mask.slice(0, i) + "." + mask.slice(i);
				i++;
			}
			if (mask[i] == "?") {
				mask = mask.slice(0, i) + ".+" + mask.slice(i + 1);
				i++;
			}
			if (mask[i] == '.') {
				mask = mask.slice(0, i) + "\\" + mask.slice(i);
				i++;
			}
		}
		mask = "^" + mask + "$";
		return mask;
	}

}).call(this);