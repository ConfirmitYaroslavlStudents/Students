import path from 'path'

const template = (props) => {
  const {
    assetsRoot,
    username,
  } = props

  const config = {
    username
  }

  return `
<!doctype html>
<html lang="ru" class="html">
    <head>
        <meta charset="utf-8">
        <title>Candidate Accounting</title>
        <link rel="icon" href ="/favicon.ico" type= "image/x-icon" >
        <link rel="shortcut icon" href ="/favicon.ico" type="image/x-icon" >
    </head>
    <body>
        <div id="root">
        </div>
        <script type="text/javascript">
          window['APP_CONFIG'] = ${JSON.stringify(config)}
        </script>
        <script type="text/javascript" src="${path.join(assetsRoot, 'vendors.js')}"></script>
        <script type="text/javascript" src="${path.join(assetsRoot, 'main.js')}"></script>
    </body>
</html>`
}

export default template