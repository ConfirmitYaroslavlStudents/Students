Screenshot testing with [TestCafe](https://github.com/DevExpress/testcafe) && [ResembleJS](https://github.com/HuddleEng/Resemble.js)
=====================

Usage
-----------------------------------
### Without callback (execution according to configuration file)
```
await toMatchScreenshot(testController, selector[, options])
```

### With callback (only takes and compares screenshots, processing of the result depends on user)
```
await toMatchScreenshot(testController, selector, {handleResult: (result) => {
  //comparison result processing
}})
```

Callback gets an object:
```
{
  testName, //string
  browserName, //string
  comparisonPerformed, //boolean
  comparisonPassed, //boolean
  baseScreenshotURL, //string
  newScreenshotURL, //string
  diffScreeshotURL, //string
  analysisTime, //number
  misMatchPercentage, //number
  maxMisMatchPercentage, //number
  isSameDimensions, //boolean
  dimensionDifference, //object
  diffBounds, //object
  getDiffScreenshotBuffer, //function
  handle, //function
  log, //function
  assert //function
}
```

`testName`, `browserName`, `comparisonPerformed`, `newBaseScreenshotWasCreated`, `baseScreenshotURL` are always presented, others aren't (depending on the function execution process).

### Arguments
* `testController` - [Testcafe TestController](https://devexpress.github.io/testcafe/documentation/test-api/test-code-structure.html#test-controller) (specific for the test (contains `testRun` object));
* `selector` - [Testcafe Selector](https://devexpress.github.io/testcafe/documentation/test-api/selecting-page-elements/selectors/) (whole page or an element for testing);
* `options` _(optional)_ - test specific option object (combine with user general options from `.matchScreenshot.config.json`; for details check `Configuration` section).

### Examples
#### Without callback
```import { Selector } from 'testcafe'
import toMatchScreenshot from './toMatchScreenshot'

fixture(`Tests`)
  .page(`http://localhost:3000/`)

test('Simple test', async t => {
  await t
  .click(Selector('button[test-button]'))

  await toMatchScreenshot(t, Selector('div[test-form]'), { screenshotName: 'testFormAfterButtonClick' })
})
```

##### With callback
```import { Selector } from 'testcafe'
import toMatchScreenshot from './toMatchScreenshot'

fixture(`Tests`)
  .page(`http://localhost:3000/`)

test('Simple test', async t => {
  await t
  .click(Selector('button[test-button]'))

  await toMatchScreenshot(t, Selector('div[test-form]'), { screenshotName: 'testFormAfterButtonClick', handleResult: (result) => {
    result.handle()
    console.log(result.comparisonPassed)
    result.assert()
  }})
})
```

**Note: `result.assert` is an `async` function. Perhaps, you need to use `await` keyword.**

Configuration
-----------------------------------
### Config file

Create your configuration file named `.toMatchScreenshot.config.js` in test file directory or above (up to project root directory).

Configuration file is a js file:

```
const options = {
  comparison: {
    ...
  },

  output: {
    ...
    difference: {
      ...
      errorColor: { ... },
      boundingBox: { ... },
      ignoredBox: { ... }
    }
  },

  metadataURL: '',

  handleResult: function (result) { ... }
}

module.exports = options
```

### Comparison options
```
comparison: {
  scaleToSameSize, //boolean, default: true
  ignore, //string or array, default: "antialiasing"
  maxMisMatchPercentage //number, default: 0
}
```
`scaleToSameSize` - scale new screenshot size to base one size.

`ignore` (`["nothing", "less", "antialiasing", "colors", "alpha"]`) - ignore mismatch rules.

`maxMisMatchPercentage` - max allowed screenshot mismatch percentage for test to be passed.

### Output options
```
output: {
  fallenTestSaveStrategy, //string, default: "separate"
  createThumbnails, //boolean, default: false

  difference: {
    errorType, //string, default: "movementDifferenceIntensity",
    transparency, //number, default: 0.95,
    largeImageThreshold, //number, default: 0
    useCrossOrigin, //boolean, default: false
    outputDiff, //boolean, default: true
    errorColor, //rgb object, default: red
    boundingBox, //object, default: none
    ignoredBox //object, default: none
  }
}
```
`fallenTestSaveStrategy` (`["testFolder", "separate"]`) - strategy, which determinates folder for fallen test screenshots:
  * `testFolder` - base screenshot folder;
  * `separate` - separate folder for fallen tests `/__screenshots__/fallenTests/...`.

`createThumbnails` - create thumbnails for screenshots or not.

`errorType` _(string one of `["flat", "movement", "flatDifferenceIntensity", "movementDifferenceIntensity", "diffOnly"]`)_ - screenshots overlay difference output mode:
  * `flat`, `flatDifferenceIntensity` - screenshots overlay with diferences highlighting;
  * `movement`, `movementDifferenceIntensity` - screenshots overlay with diferences highlighting (base and new elements have different colors);
  * `diffOnly` - show only differences.

`transparency` - screenshots overlay matched parts transparency (1 - as is, 0 - invisible).

`largeImageThreshold` - screenshot max size to be compared fully (optimization purposes) (0 - no threshold).

`useCrossOrigin` - check [ResembleJS documentation](https://github.com/HuddleEng/Resemble.js).

`outputDiff` - check [ResembleJS documentation](https://github.com/HuddleEng/Resemble.js).

```
errorColor: {
  red, //number, default: 255
  green, //number, default: 0
  blue, //number, default: 0
}
```
screenshots overlay differences highlight color.


```
boundingBox: {
  left, //number, default: none
  top, //number, default: none
  right, //number, default: none
  bottom, //number, default: none
}
```
narrows down the area of comparison (in pixels, from left top corner).


```
ignoredBox: {
  left, //number, default: none
  top, //number, default: none
  right, //number, default: none
  bottom, //number, default: none
}
```
excludes part of the image from comparison (in pixels, from left top corner).

### Metadata options
```
metadataURL //string, default: './.metadata.json'
```

`metadataURL` - relative path for screenshot metadata file in `__screenshots__` folder.

### Callback options
```
handleResult //function, default: none
```

`handleResult` - callback function which allows user to implement their own comparison result processing.
