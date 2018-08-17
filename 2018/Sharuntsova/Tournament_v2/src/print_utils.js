const { GridTypes } = require('./tournament-types');

const cornerDownLeft = '╗';
const cornerUpRight = '╚';
const verticalLine = '║';
const cornerUpLeft = '╝';
const cornerDownRight = '╔';


function suplementBracket(inputBracket, numberOfRounds, playerNameLength) {
    let bracket = inputBracket.slice();
    for (let round = 0; round <= numberOfRounds; round++) {
        const numberOfPlayersOfThisRound = Math.pow(2, numberOfRounds - round);

        if (!bracket[round]) {
            bracket[round] = new Array(numberOfPlayersOfThisRound);
        }
       
        for (let player = 0; player < numberOfPlayersOfThisRound; player++) {
            if (!bracket[round][player]) {
                bracket[round][player] = '?'.repeat(playerNameLength);
            }
        }
    }
    return bracket;
}

function print(typeOfDisplay, bracket, numberOfRounds, numberOfPlayers, playerNameLength) {
    let printingColumns = [];
    let printingLines = [];

    printingColumns = buildColumns(typeOfDisplay, bracket, numberOfRounds, playerNameLength);
    printingLines = columnsToLines(printingColumns, numberOfPlayers);
    printLines(printingLines);
}

function buildColumns(gridType, bracket, numberOfRounds, playerNameLength) {
    let printingColumns = [];
    switch (gridType) {
        case GridTypes.Olympics:
            buildOlympicsBracket(printingColumns, bracket, numberOfRounds, playerNameLength);
            break;
        case GridTypes.DoubleBracket:
            buildLeftPartColumns(printingColumns, bracket, numberOfRounds, playerNameLength);
            buildCentralPartColumns(printingColumns, bracket, numberOfRounds, playerNameLength);
            buildRightPartColumns(printingColumns, bracket, numberOfRounds, playerNameLength);
            break;
    }
    return printingColumns;
}

function buildLeftPartColumns(printingColumns, bracket, numberOfRounds, playerNameLength) {
    let edgeSpace, lineSpace;

    for (let i = 0; i < numberOfRounds - 1; i++) {
        const currentRow = [];
        edgeSpace = Math.pow(2, i) - 1;
        lineSpace = Math.pow(2, (i + 1)) - 1;
        if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, playerNameLength);
        }

        for (let j = 0; j < bracket[i].length / 2 - 1; j++) {
            currentRow.push(bracket[i][j]);
            makeEmptySpacesInColumn(currentRow, lineSpace, playerNameLength);
        }
        currentRow.push(bracket[i][bracket[i].length / 2 - 1]);

        if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, playerNameLength);
        }

        printingColumns.push(currentRow);
        printingColumns.push(makeVerticalBrackets(bracket[i].length / 2, lineSpace, edgeSpace, cornerUpLeft, cornerDownLeft)); 
    }
}

function buildCentralPartColumns(printingColumns, bracket, numberOfRounds, playerNameLength) {
    const emptySpaceLength = Math.pow(2, (numberOfRounds - 1)) - 1;

    printingColumns.push(makeCentralColumn(emptySpaceLength, playerNameLength, bracket[numberOfRounds - 1][0]));
    printingColumns.push(makeCentralColumn(emptySpaceLength, 1, '='));
    printingColumns.push(makeCentralColumn(emptySpaceLength, playerNameLength, bracket[numberOfRounds][0]));
    printingColumns.push(makeCentralColumn(emptySpaceLength, 1, '='));
    printingColumns.push(makeCentralColumn(emptySpaceLength, playerNameLength, bracket[numberOfRounds - 1][1]));
}

function makeCentralColumn(emptySpaceLength, length, filling) {
    const currentRow = [];
    makeEmptySpacesInColumn(currentRow, emptySpaceLength, length);
    currentRow.push(filling);
    makeEmptySpacesInColumn(currentRow, emptySpaceLength, length);
    return currentRow;
}

function buildRightPartColumns(printingColumns, bracket, numberOfRounds, playerNameLength) {
    let edgeSpace, lineSpace;

    for (let i = numberOfRounds - 2; i >= 0; i--) {

        const currentRow = [];
        edgeSpace = Math.pow(2, i) - 1;
        lineSpace = Math.pow(2, (i + 1)) - 1;

        printingColumns.push(makeVerticalBrackets(bracket[i].length / 2, lineSpace, edgeSpace, cornerUpRight, cornerDownRight));

        if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, playerNameLength);
        }

        for (let j = bracket[i].length / 2; j < bracket[i].length - 1; j++) {
            currentRow.push(bracket[i][j]);
            makeEmptySpacesInColumn(currentRow, lineSpace, playerNameLength);
        }
        currentRow.push(bracket[i][bracket[i].length - 1]);

        if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, playerNameLength);
        }

        printingColumns.push(currentRow);
    }
}

function buildOlympicsBracket(printingColumns, bracket, numberOfRounds, playerNameLength) {
    let edgeSpace, lineSpace;
    
    for (let i = 0; i < numberOfRounds; i++) {
        const currentRow = [];
        edgeSpace = Math.pow(2, i) - 1;
        lineSpace = Math.pow(2, (i + 1)) - 1;
        if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, playerNameLength);
        }

        for (let j = 0; j < bracket[i].length - 1; j++) {
            currentRow.push(bracket[i][j]);
            makeEmptySpacesInColumn(currentRow, lineSpace, playerNameLength);
        }
        currentRow.push(bracket[i][bracket[i].length - 1]);

        if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, playerNameLength);
        }

        printingColumns.push(currentRow);
        printingColumns.push(makeVerticalBrackets(bracket[i].length, lineSpace, edgeSpace, cornerUpLeft, cornerDownLeft)); 
    }
    const emptySpaceLength = Math.pow(2, numberOfRounds) - 1;
    printingColumns.push(makeCentralColumn(emptySpaceLength, playerNameLength, bracket[numberOfRounds][0]));
}

function makeEmptySpacesInColumn(array, count, length) {
    for (let j = 0; j < count; j++)
        array.push(' '.repeat(length));

}

function makeVerticalBrackets(count, lineSpace, edgeSpace, cornerUp, cornerDown) {
    let currentRow = [];
    for (let i = 0; i < count / 2; i++) {
        if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, 1);
        }
        currentRow.push(cornerDown);
        for (let j = 0; j < lineSpace; j++)
            currentRow.push(verticalLine.repeat(1));
        currentRow.push(cornerUp);
        if (i !== count / 2 - 1) {
            makeEmptySpacesInColumn(currentRow, lineSpace, 1);
        }
        else if (edgeSpace !== 0) {
            makeEmptySpacesInColumn(currentRow, edgeSpace, 1);
        }
    }
    return currentRow;
}

function columnsToLines(printingColumns, numberOfPlayers) {
    const printingLines = [];
    const height = printingColumns[0].length;
    for (let k = 0; k < height; k++)
        printingLines[k] = new Array(printingColumns.length);
    for (let i = 0; i < printingColumns.length; i++) {
        for (let j = 0; j < height; j++) {
            printingLines[j][i] = printingColumns[i][j];
        }
    }
    return printingLines;
}

function printLines(printingLines) {
    for (let i = 0; i < printingLines.length; i++)
        console.log(printingLines[i].join(''));
}


module.exports = { suplementBracket, print }