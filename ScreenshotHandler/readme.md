Screenshot testing with Testcafe && ResembleJS
=====================

Usage
-----------------------------------
```import toMatchScreenshot from './toMatchScreenshot/index'

const result = await toMatchScreenshot(testController, selector[, options])
```

`toMatchScreenshot` is a `async` function, which takes screenshot of selected element and compared it with base one (if base one doesn't exist, creates it). The function performs screenshot creation, comparison, result logging and Testcafe assertion).

### Arguments
*`testController` - Testcafe TestController _(specific for the test (has `testRun`))_
*`selector` - Testcafe Selector _(whole page or an element for testing)_
*`options` _(optional)_ - test specific option object _(comdine with user general options from `.matchScreenshot.config.json`)_

### Return
Returns object:
```
{
  testName, //string
  browserName, //string
  newBaseScreenshotWasCreated, //boolean
  baseScreenshotURL, //string
  maxMisMatchPercentage, //number
  comparisonPerformed: //boolean,
  comparisonPassed: //boolean,
  misMatchPercentage, //number, difference degree between new screenshot and base one
  isSameDimensions, //bollean
  dimensionDifference, //object
  diffBounds, //object
  comparisonResult.analysisTime, //number, ms
  getDiffScreenshotBuffer, //function
  newScreenshotURL, //string
  diffScreeshotURL, //string
}
```
_Some properties may not be depending on the function execution process._

Example
-----------------------------------
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

Configuration
-----------------------------------
### Config file

Create your configuration file named `.matchScreenshot.config.json` in test file directory or above _(up to project root directory)_.

Configuration file is a json file:

```
{
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
  }
}
```

### Comparison options
```
comparison: {
  scaleToSameSize: //boolean, default: true
  ignore: "antialiasing", //string or array, default: "antialiasing"
  maxMisMatchPercentage: //number, default: 0
}
```

`scaleToSameSize` - scale new screenshot size to base one size
`ignore` _(some strings from `["nothing", "less", "antialiasing", "colors", "alpha"]`)_ - ignore mismatch rules
`maxMisMatchPercentage` - max allowed screenshot mismatch percentage for test to be passed

### Output options
```
"output": {
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
`fallenTestSaveStrategy` _(one of `["testFolder", "separate"]``)_ - strategy which determinates folder for fallen test screenshots:
  *`testFolder` - base screenshot folder
  *`separate` - separate folder for fallen tests `/__screenshots__/fallenTests/...`

'createThumbnails' - create thumbnails for screenshots or not

`errorType` _(string one of `["flat", "movement", "flatDifferenceIntensity", "movementDifferenceIntensity", "diffOnly"]`, default: `movementDifferenceIntensity`)_ - screenshots overlay difference output mode:
  * `flat`, `flatDifferenceIntensity` - screenshots overlay with diferences highlighting
  * `movement`, `movementDifferenceIntensity` - screenshots overlay with diferences highlighting (base and new elements have different colors)
  * `diffOnly` - show only differences

`transparency` - screenshots overlay matched parts transparency (1 - as is, 0 - invisible)

`largeImageThreshold` - screenshot max size to be compared fully (optimization purposes) (0 - no threshold)

`useCrossOrigin` - ??? (check [ResembleJS documentation](https://github.com/HuddleEng/Resemble.js))

`outputDiff` - ??? ((check [ResembleJS documentation](https://github.com/HuddleEng/Resemble.js))

```
errorColor: {
  red: 255,
  green: 0,
  blue: 0
}
```
- screenshots overlay differences highlight color

```
boundingBox: {
  left: 100,
  top: 100,
  right: 100,
  bottom: 100
}
```
- narrows down the area of comparison (from left top corner)

```
ignoredBox: {
  left: 100,
  top: 100,
  right: 100,
  bottom: 100
}
```
- excludes part of the image from comparison (from left tp corner

Contacts
-----------------------------------
*email: [dmitry.banokin@gmail.com](mailto:dmitry.banokin@gmail.com)*