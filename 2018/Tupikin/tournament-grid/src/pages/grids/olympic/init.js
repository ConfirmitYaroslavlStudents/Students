import * as go from 'gojs';

let myDiagram;

export function init(names) {
  const $ = go.GraphObject.make; // for conciseness in defining templates

  // validation function for editing text
  function isValidScore(textblock, oldstr, newstr) {
    if (newstr === '') return true;
    const num = parseInt(newstr, 10);
    return !isNaN(num) && num >= 0 && num < 1000;
  }

  myDiagram =
    $(go.Diagram, 'myDiagramDiv', // create a Diagram for the DIV HTML element
      {
        initialContentAlignment: go.Spot.Center, // center the content
        'textEditingTool.starting': go.TextEditingTool.SingleClick,
        'textEditingTool.textValidation': isValidScore,
        layout: $(go.TreeLayout, { angle: 180 }),
        'undoManager.isEnabled': true
      });

  // define a simple Node template
  myDiagram.nodeTemplate =
    $(go.Node, 'Auto',
      { selectable: false },
      $(go.Shape, 'Rectangle',
        { fill: '#8C8C8C', stroke: null },
        // Shape.fill is bound to Node.data.color
        new go.Binding('fill', 'color')),
      $(go.Panel, 'Table',
        $(go.RowColumnDefinition, { column: 0, separatorStroke: 'black' }),
        $(go.RowColumnDefinition, { column: 1, separatorStroke: 'black', background: '#BABABA' }),
        $(go.RowColumnDefinition, { row: 0, separatorStroke: 'black' }),
        $(go.RowColumnDefinition, { row: 1, separatorStroke: 'black' }),
        $(go.TextBlock, '',
          { row: 0,
            wrap: go.TextBlock.None, margin: 5, width: 90,
            isMultiline: false, textAlign: 'left',
            font: '10pt  Segoe UI,sans-serif', stroke: 'white' },
          new go.Binding('text', 'player1').makeTwoWay()),
        $(go.TextBlock, '',
          { row: 1,
            wrap: go.TextBlock.None, margin: 5, width: 90,
            isMultiline: false, textAlign: 'left',
            font: '10pt  Segoe UI,sans-serif', stroke: 'white' },
          new go.Binding('text', 'player2').makeTwoWay()),
        $(go.TextBlock, '',
          { column: 1, row: 0,
            wrap: go.TextBlock.None, margin: 2, width: 25,
            isMultiline: false, editable: true, textAlign: 'center',
            font: '10pt  Segoe UI,sans-serif', stroke: 'black' },
          new go.Binding('text', 'score1').makeTwoWay()),
        $(go.TextBlock, '',
          { column: 1, row: 1,
            wrap: go.TextBlock.None, margin: 2, width: 25,
            isMultiline: false, editable: true, textAlign: 'center',
            font: '10pt  Segoe UI,sans-serif', stroke: 'black' },
          new go.Binding('text', 'score2').makeTwoWay())
      )
    );

  // define the Link template
  myDiagram.linkTemplate =
    $(go.Link,
      { routing: go.Link.Orthogonal,
        selectable: false },
      $(go.Shape, { strokeWidth: 2, stroke: 'white' }));


  function makeLevel(levelGroups, currentLevel, totalGroups, players) {
    currentLevel--;
    const len = levelGroups.length;
    const parentKeys = [];
    let parentNumber = 0;
    let p = '';
    for (let i = 0; i < len; i++) {
      if (parentNumber === 0) {
        p = `${currentLevel}-${parentKeys.length}`;
        parentKeys.push(p);
      }

      if (players !== null) {
        const p1 = players[i * 2];
        const p2 = players[(i * 2) + 1];
        totalGroups.push({
          key: levelGroups[i], parent: p, player1: p1, player2: p2, parentNumber
        });
      } else {
        totalGroups.push({ key: levelGroups[i], parent: p, parentNumber });
      }

      parentNumber++;
      if (parentNumber > 1) parentNumber = 0;
    }

    // after the first created level there are no player names
    if (currentLevel >= 0) makeLevel(parentKeys, currentLevel, totalGroups, null);
  }

} // end init
