const minimist = require('minimist');
const path = require('path');
const { app, BrowserWindow } = require('electron');

const isDevelopment = process.env.NODE_ENV === 'development';

const argv = minimist(process.argv)
const screenshotMetadataURL =
  argv.metadata ?
    argv.metadata
    :
    argv.M ?
      argv.M
      :
      null

let utilityWindow = null;

const utilityWindowHTMLFileURL = path.resolve(path.join(__dirname, '..', 'renderer', 'index.html'));

const mainWindowOptions = {
  width: 1280,
  height: 1024,
  frame: true,
  autoHideMenuBar: true,
  webPreferences: {
    devTools: isDevelopment,
    webSecurity: false
  }
};

function createWindow () {
  utilityWindow = new BrowserWindow(mainWindowOptions);

  utilityWindow.loadFile(utilityWindowHTMLFileURL);

  if (screenshotMetadataURL === null) {
    throw 'Metadata file URL argument must be passed via --metadata or -M key.';
  } else {
    global.screenshotMetadataFileURL = screenshotMetadataURL;
  }

  if (mainWindowOptions.webPreferences.devTools) {
    utilityWindow.webContents.openDevTools();
  }

  utilityWindow.on('closed', () => {
    utilityWindow = null;
  })
}

app.on('ready', createWindow);

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit();
  }
});

app.on('activate', () => {
  if (utilityWindow === null) {
    createWindow();
  }
});