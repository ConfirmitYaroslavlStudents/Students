ScreenshotHandler Utility
=====================
Usage
-----------------------------------
Run utility after execution of TestCafe screenshot test with toMatchScreenshot from ScreenshotHandler.

### Via command line and Electron
```
electron ./index.js --metadata _screenshotMetadataFileAbsURL_
```

### Via exe file
```
./dist/win-unpacked/ScreenshotHandlerUtility.exe --metadata _screenshotMetadataFileAbsURL_
```

### Command line keys
* `--metadata`, `-M` - (required) absolute path for screenshot test metadata file (`.metadata.json`);
* `--dev`, `-D` - (optional) force development mode.

Scripts
-----------------------------------
* `build` - pack and build project as an Electron application into `dist` folder;
* `start` - start the Electron application via `ScreenshotHandlerUtility.exe`;
* `prod` - pack and build the project as an Electron application into `dist` folder and then start the Electron application via `ScreenshotHandlerUtility.exe`;
* `dev` - build the project in development mode and then start it via `electron`;
* `postinstall` - `electron-builder` private script.