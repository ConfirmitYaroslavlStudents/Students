const { app, BrowserWindow } = require('electron')
const path = require('path')

let mainWindow

const mainWindowHTMLURL = path.join(__dirname, 'dist', 'index.html')

const screenshotMetadataFileURL = process.argv[3]

const mainWindowOptions = {
  width: 1280,
  height: 1024,
  frame: true,
  autoHideMenuBar: true,
  webPreferences: {
    devTools: false,
    webSecurity: true
  }
}

function createWindow () {
  mainWindow = new BrowserWindow(mainWindowOptions)

  mainWindow.loadFile(mainWindowHTMLURL)

  global.screenshotMetadataFileURL = screenshotMetadataFileURL

  //mainWindow.webContents.openDevTools()

  mainWindow.on('closed', () => {
    mainWindow = null
  })
}

app.on('ready', createWindow)

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit()
  }
})

app.on('activate', () => {
  if (mainWindow === null) {
    createWindow()
  }
})