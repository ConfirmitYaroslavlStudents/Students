import React from 'react'
import ReactDOMServer from 'react-dom/server'
import { ServerStyleSheet } from 'styled-components'
import PagePlaceholder from './components/pagePlaceholder'

const template = (props) => {
  const { assetsRoot, username } = props

  const config = { username }


  const sheet = new ServerStyleSheet()
  const pagePlaceholder = ReactDOMServer.renderToString(sheet.collectStyles(<PagePlaceholder />))
  const styles = sheet.getStyleTags()

  return (
    `<!doctype html>
    <html lang="ru" class="html">
      <head>
        <meta charset="utf-8">
        <meta name="theme-color" content="#3F51B5">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Candidate Accounting</title>
        <link rel="manifest" href="${assetsRoot + 'manifest.json'}">
        <link rel="icon" href="${assetsRoot + 'favicon.ico'}" type= "image/x-icon">
        <link rel="shortcut icon" href="${assetsRoot + 'favicon.ico'}" type="image/x-icon">
        ${styles}
      </head>
      <body>
        <div id="root">
          ${pagePlaceholder}  
        </div>                      
        <script type="text/javascript">
          window['APP_CONFIG'] = ${JSON.stringify(config)}
        </script>
        <script async type="text/javascript" src="${assetsRoot + 'vendors.js'}"></script>
        <script async type="text/javascript" src="${assetsRoot + 'main.js'}"></script>
      </body>
    </html>`
  )
}

export default template