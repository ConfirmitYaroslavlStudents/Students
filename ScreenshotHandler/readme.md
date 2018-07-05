Screenshot testing with Testcafe + ResembleJS

*** Usage ***

import toMatchScreenshot from './toMatchScreenshot/index'

await toMatchScreenshot(<TestController>, <Selector>, <Options object (optional)>)


*** Example ***

import { Selector } from 'testcafe'
import toMatchScreenshot from './toMatchScreenshot'

fixture(`Tests`)
  .page(`http://localhost:3000/`)

test('Simple test', async t => {
  await t
  .click(Selector('button[test-button]'))

  await toMatchScreenshot(t, Selector('div[test-form]'))
})