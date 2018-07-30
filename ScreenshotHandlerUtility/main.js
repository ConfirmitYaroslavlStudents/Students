const minimist = require('minimist')
const path = require('path')
const { app, BrowserWindow } = require('electron')

const argv = minimist(process.argv.slice(2))
const screenshotMetadataURL =
  argv.metadata ?
    argv.metadata
    :
    argv.M ?
      argv.M
      :
      null

if (!screenshotMetadataURL) {
  throw 'Metadata file URL argument must be passed via --metadata or -M key.'
}

const utilityWindowHTMLURL = path.join(__dirname, 'dist', 'index.html')

let utilityWindow

const mainWindowOptions = {
  width: 1280,
  height: 1024,
  frame: true,
  autoHideMenuBar: true,
  webPreferences: {
    devTools: true,
    webSecurity: true
  }
}

function createWindow () {
  utilityWindow = new BrowserWindow(mainWindowOptions)

  utilityWindow.loadFile(utilityWindowHTMLURL)

  global.screenshotMetadataFileURL = screenshotMetadataURL

  if (mainWindowOptions.webPreferences.devTools) {
    utilityWindow.webContents.openDevTools()
  }

  utilityWindow.on('closed', () => {
    utilityWindow = null
  })
}

app.on('ready', createWindow)

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit()
  }
})

app.on('activate', () => {
  if (utilityWindow === null) {
    createWindow()
  }
})