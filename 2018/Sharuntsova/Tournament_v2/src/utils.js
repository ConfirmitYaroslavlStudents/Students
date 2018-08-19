const readLine = require('readline-sync');

shuffleArray = (a) => {
    for (let i = a.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [a[i], a[j]] = [a[j], a[i]];
    }
    return a;
}

makeSelectRequest = (requestMessage, options) => {
    const index = readLine.keyInSelect(options, requestMessage);
    if (index < 0)
        process.exit(0);
    return options[index];
}


askForWinner = (array) => {
    const index = readLine.keyInSelect(array, 'Who won?');
    return index;
}

module.exports = {
    shuffleArray,
    askForWinner,
    makeSelectRequest
}