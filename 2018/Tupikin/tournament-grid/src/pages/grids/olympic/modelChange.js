export const modelChange = (e, tournament) => {
  if (e.propertyName !== 'score1' && e.propertyName !== 'score2') return;
  const data = e.object;
  // eslint-disable-next-line no-restricted-globals
  if (isNaN(data.score1) || isNaN(data.score2)) return;

  const parent = tournament.findNodeForKey(data.parent);
  if (parent === null) return;

  let playerName = parseInt(data.score1, 10) > parseInt(data.score2, 10) ? data.player1 : data.player2;
  if (parseInt(data.score1, 10) === parseInt(data.score2, 10)) playerName = '';
  e.model.setDataProperty(parent.data, (data.parentNumber === 0 ? 'player1' : 'player2'), playerName);
};
