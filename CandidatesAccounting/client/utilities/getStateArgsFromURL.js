export default function getStateArgsFromURL(url) {
  const splitedURL = url.split('?')

  const path = splitedURL[0]
  const splitedPath = path.split('/')
  const tableName = splitedPath[1]
  let tableType = ''
  switch (tableName) {
    case 'interviewees':
      tableType = 'Interviewee'
      break
    case 'students':
      tableType = 'Student'
      break
    case 'trainees':
      tableType = 'Trainee'
      break
  }

  const URLargs = splitedURL[1]
  const args = {}
  if (URLargs) {
    const argsArray = URLargs.split('&')
    argsArray.forEach((arg) => {
      const splitedArg = arg.split('=')
      args[splitedArg[0]] = splitedArg[1]
    })
  }

  return {
    tableType,
    searchRequest: args.q ? decodeURIComponent(args.q) : '',
    offset: args.skip ? Number(args.skip) : 0,
    candidatesPerPage: args.take ? Number(args.take) : 15,
    sortingField: args.sort ? args.sort : '',
    sortingDirection: args.sortDir ? args.sortDir : 'desc'
  }
}