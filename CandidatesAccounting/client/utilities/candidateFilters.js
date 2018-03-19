export function searchById(candidates, id) {
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id === id) {
      return candidates[i];
    }
  }
}