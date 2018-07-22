function fifa() {
  const leftState = { ...state };
  const rightState = { ...state };
  for (let i = 0; i < leftState.sessions.length; i++) {
    const session = leftState.sessions[i];
    leftState.sessions[i] = session.slice(0, session.length / 2);
    rightState.sessions[i] = session.slice(session.length / 2);
  }

  // console.log(rightState.sessions);
  // console.log();
  // console.log(leftState.sessions);

  console.log(state);

  return [[]];
}

module.exports = fifa;
