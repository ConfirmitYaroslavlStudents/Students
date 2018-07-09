Screenshot testing with Testcafe + ResembleJS

*** Usage ***

import toMatchScreenshot from './toMatchScreenshot/index'

await toMatchScreenshot(testController, selector[, options])


*** Example ***

import { Selector } from 'testcafe'
import toMatchScreenshot from './toMatchScreenshot'

fixture(`Tests`)
  .page(`http://localhost:3000/`)

test('Simple test', async t => {
  await t
  .click(Selector('button[test-button]'))

  await toMatchScreenshot(t, Selector('div[test-form]'), { screenshotName: 'testFormAfterButtonClick' })
})


*** Configuration ***

* Config file *

Create your configuration file ".matchScreenshot.config.json" in test file directory or above (up to project root directory).

Configuration file is a json file:

{
  comparison: {
    ...
  },

  output: {
    ...
    difference: {
      ...
      errorColor: {
        ...
      },
      boundingBox: {
        ...
      },
      ignoredBox: {
        ...
      }
    }
  }
}


* Comparison options *

comparison: {
  scaleToSameSize: true,
  ignore: "antialiasing", // "nothing", "less", "antialiasing", "colors", "alpha"
  maxMisMatchPercentage: 0
}

scaleToSameSize (boolean) - scale new screenshot size to base one size
ignore (string or array) - ignore mismatch rules
maxMisMatchPercentage (number [0; 100)) - max allowed screenshot mismatch percentage for test to be passed


* Output options *

difference: {
  errorType: "movementDifferenceIntensity",
  transparency: 0.95,
  largeImageThreshold: 0,
  useCrossOrigin: false,
  outputDiff: true,

  errorColor: {
    ...
  },

  boundingBox: {
    ...
  },

  ignoredBox: {
    ...
  }
}

errorType (string one of ["flat", "movement", "flatDifferenceIntensity", "movementDifferenceIntensity", "diffOnly"]) - screenshots overlay difference output mode:
  * flat, flatDifferenceIntensity - screenshots overlay with diferences highlighting
  * movement, movementDifferenceIntensity - screenshots overlay with diferences highlighting (base and new elements have different colors)
  * diffOnly - show only differences

transparency (number [0; 1]) - screenshots overlay matched parts transparency (1 - as is, 0 - invisible)

largeImageThreshold (number) - screenshot max size to be compared fully (optimization purposes) (0 - no threshold)

useCrossOrigin (boolean) - ??? (check: https://github.com/HuddleEng/Resemble.js)

outputDiff (boolean) - ??? (check: https://github.com/HuddleEng/Resemble.js)

errorColor: {
  red: 255,
  green: 0,
  blue: 0
}
(object) - screenshots overlay differences highlight color

boundingBox: {
  left: 100,
  top: 100,
  right: 100,
  bottom: 100
}
(object) - narrows down the area of comparison (from left top corner)

ignoredBox: {
  left: 100,
  top: 100,
  right: 100,
  bottom: 100
}
(object) - exclude part of the image from comparison (from left tp corner)


*** Contacts ***

email: dmitry.banokin@gmail.com