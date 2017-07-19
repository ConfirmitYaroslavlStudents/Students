(function() {
  var cli, colors, err, pkg, program;
  program = require("commander");
  colors = require("colors");
  pkg = require("../package.json");
  cli = require("./cli");
  
  program.version(pkg.version)
    .option("--toName [mask]", "find files by mask and replace name by tag")
    .option("--toTag [mask]", "find files by mask and replace tag by name")
    .option("-r, --recursive", "go to nested folders")
    .parse(process.argv);

  if (program.toName && program.toTag || !(program.toName || program.toTag)) {
    console.log("[", "renamer".white, "]" + "wrong flags -toTag | -toName".red);
    return;
  }

  if (program.toName)
    cli.renameByTag(program.toName, program.recursive);
  
  if (program.toTag)
    cli.retagByName(program.toTag, program.recursive);

}).call(this);