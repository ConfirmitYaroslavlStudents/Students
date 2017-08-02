
function getNewPath(path, options) {
	let separator = options.separator || " - ";
	let data = options.data;
	
	let newPath = path.slice(0, path.lastIndexOf("/"));
	newPath = newPath + "/" + data.artist[0] + separator + data.title + ".mp3";

	return newPath;
}

function getTags(path, options) {
	let separator;
	if (options)
		separator = options.separator || ' - ';
	else
		separator = ' - ';

	let name = path.slice(path.lastIndexOf("/") + 1);
	
	let artist = name.slice(0, name.indexOf(separator));
	
	let title = name.slice(name.indexOf(separator) + separator.length, name.indexOf(".mp3"));
	
	return {
		artist: [artist],
		title: title
	}
}

exports.getTags = getTags;
exports.getNewPath = getNewPath;