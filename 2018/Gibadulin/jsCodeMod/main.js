const replaceCall = require('./modules/replaceCall.js');
const fileReader = require('./modules/fileReader.js');
const fileSaver = require('./modules/fileSaver.js');
(function () {
    const pathToFile    = process.argv[2];
    const pathToOldCall = process.argv[3];
    const pathToNewCall = process.argv[4];

    const fileSource    = fileReader(pathToFile);
    const oldCallSource = fileReader(pathToOldCall);
    const newCallSource = fileReader(pathToNewCall);

    const newFileSource = replaceCall(fileSource, oldCallSource, newCallSource);

    fileSaver(pathToFile, newFileSource);
}());
