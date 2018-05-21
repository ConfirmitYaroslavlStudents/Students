export default function findDiferences(previousState, newState) {
  let diferences = {}
  for (const key in previousState)
  {
    if (!(previousState[key] instanceof Object) && previousState[key] !== '' && (!newState[key] || previousState[key] !== newState[key]) )
    {
      diferences[key] = {
        previousState: previousState[key],
        newState: newState[key]
      }
    }
  }
  for (const key in newState)
  {
    if (!diferences[key] && !(newState[key] instanceof Object) && newState[key] !== '' && (!previousState[key] || previousState[key] !== newState[key]))
    {
      diferences[key] = {
        previousState: previousState[key],
        newState: newState[key]
      }
    }
  }
  return diferences
}