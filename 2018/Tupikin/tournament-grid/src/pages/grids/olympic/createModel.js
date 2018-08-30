import * as go from 'gojs';

const isValidScore = (textblock, oldstr, newstr) => {
  if (newstr === '') return true;
  const num = parseInt(newstr, 10);
  // eslint-disable-next-line no-restricted-globals
  return !isNaN(num) && num >= 0 && num < 1000;
};

export const createModel = () => {
  const $ = go.GraphObject.make;

  const tournament = $(go.Diagram, 'tournament',
    {
      initialContentAlignment: go.Spot.Center, // center the content
      'textEditingTool.starting': go.TextEditingTool.SingleClick,
      'textEditingTool.textValidation': isValidScore,
      layout: $(go.TreeLayout, { angle: 180 }),
      'undoManager.isEnabled': true
    });

  tournament.nodeTemplate =
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

  tournament.linkTemplate =
    $(go.Link,
      { routing: go.Link.Orthogonal,
        selectable: false },
      $(go.Shape, { strokeWidth: 2, stroke: 'white' }));

  return tournament;
};
