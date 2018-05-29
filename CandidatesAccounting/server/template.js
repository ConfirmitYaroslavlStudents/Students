import path from 'path'

const template = (props) => {
  const { assetsRoot, username } = props

  const config = { username }

  return `
    <!doctype html>
    <html lang="ru" class="html">
        <head>
            <meta charset="utf-8">
            <meta name="theme-color" content="#3F51B5">
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <title>Candidate Accounting</title>
            <link rel="manifest" href="${path.join(assetsRoot, 'manifest.json')}/">
            <link rel="icon" href ="/favicon.ico" type= "image/x-icon" >
            <link rel="shortcut icon" href ="/favicon.ico" type="image/x-icon" >
        </head>
        <body style="background-color: #CCC;">
            <div id="root">
                <div style="position: fixed; top: 0; left: 0; width: 100%; height: 60px; background-color: #3F51B5; z-index: 10;"></div>                
                <div style="position: fixed; top: 60px; left: 0; width: 100%; height: 48px; background-color: #f5f5f5; box-shadow: 0 0 4px 4px rgba(0,0,0,0.2);" ></div>
                <div style="margin: -10px; font-size: 190%; font-weight: bold; color: #888; font-family: sans-serif; text-align: center; padding-top: 200px;">
                    CandidateAccounting is loading...
                </div>
            </div>
            <script type="text/javascript">
              window['APP_CONFIG'] = ${JSON.stringify(config)}
            </script>
            <script async type="text/javascript" src="${path.join(assetsRoot, 'vendors.js')}"></script>
            <script async type="text/javascript" src="${path.join(assetsRoot, 'main.js')}"></script>
        </body>
    </html>`
}

export default template