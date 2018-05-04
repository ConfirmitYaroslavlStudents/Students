export default (stateSelector, selectors) => {
  const boundSelectors = {}
  Object.keys(selectors).forEach(key => {
    const selector = selectors[key]

    boundSelectors[key] = (state, ...rest) => selector(stateSelector(state), ...rest)
  })
  return boundSelectors
}
