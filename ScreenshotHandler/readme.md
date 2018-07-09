Screenshot testing with Testcafe + ResembleJS
=====================

Usage
-----------------------------------
```import toMatchScreenshot from './toMatchScreenshot/index'

await toMatchScreenshot(testController, selector[, options])
```

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

Create your configuration file `.matchScreenshot.config.json` in test file directory or above _(up to project root directory)_.

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
```

#### Comparison options

```
comparison: {
  scaleToSameSize: true,
  ignore: "antialiasing", // "nothing", "less", "antialiasing", "colors", "alpha"
  maxMisMatchPercentage: 0
}
```

`scaleToSameSize` _(boolean)_ - scale new screenshot size to base one size
`ignore` _(string or array)_ - ignore mismatch rules
`maxMisMatchPercentage` _(number)_ - max allowed screenshot mismatch percentage for test to be passed


#### Output options

```
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
```

`errorType` _(string one of `["flat", "movement", "flatDifferenceIntensity", "movementDifferenceIntensity", "diffOnly"]`)_ - screenshots overlay difference output mode:
  * `flat`, `flatDifferenceIntensity` - screenshots overlay with diferences highlighting
  * `movement`, `movementDifferenceIntensity` - screenshots overlay with diferences highlighting (base and new elements have different colors)
  * `diffOnly` - show only differences

`transparency` _(number [0..1])_ - screenshots overlay matched parts transparency (1 - as is, 0 - invisible)

`largeImageThreshold` _(number)_ - screenshot max size to be compared fully (optimization purposes) (0 - no threshold)

`useCrossOrigin` _(boolean)_ - ??? (check [ResembleJS documentation](https://github.com/HuddleEng/Resemble.js))

`outputDiff` _(boolean)_ - ??? ((check [ResembleJS documentation](https://github.com/HuddleEng/Resemble.js))

```
errorColor: {
  red: 255,
  green: 0,
  blue: 0
}
```
_(object)_ - screenshots overlay differences highlight color

```
boundingBox: {
  left: 100,
  top: 100,
  right: 100,
  bottom: 100
}
```
_(object)_ - narrows down the area of comparison (from left top corner)

```
ignoredBox: {
  left: 100,
  top: 100,
  right: 100,
  bottom: 100
}
```
_(object)_ - excludes part of the image from comparison (from left tp corner

Contacts
-----------------------------------
`email`: `dmitry.banokin@gmail.com`
