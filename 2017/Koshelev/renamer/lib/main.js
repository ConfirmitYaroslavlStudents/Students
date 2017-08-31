function main() {
  var cli, colors, err, pkg, program;
  program = require("commander");
  colors = require("colors");
  pkg = require("../package.json");
  let rename = require("./Rename").renameByTag;
  let retag = require('./Retag').retagByName;
  if (module.parent){
    module.exports={
      retag, rename
    };
    return;
  }
  program.version(pkg.version)
    .option("--toName [mask]", "find files by mask and replace name by tag")
    .option("--toTag [mask]", "find files by mask and replace tag by name")
    .option("-r, --recursive", "go to nested folders")
    .parse(process.argv);

  if (program.toName && program.toTag || !(program.toName || program.toTag)) {
    console.log("[", "renamer".white, "]" + "wrong flags --toTag | --toName".red);
    return;
  }

  if (program.toName)
    rename(maskToRegExp(program.toName), {
      recursive: program.recursive
    });

  if (program.toTag)
    retag(maskToRegExp(program.toTag), {
      recursive: program.recursive
    });
}

function maskToRegExp(mask) {

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
module.exports = main;