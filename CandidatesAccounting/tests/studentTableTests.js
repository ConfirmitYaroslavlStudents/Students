import { Selector } from 'testcafe'
import { signIn } from './common'
import toMatchScreenshot from '../../ScreenshotHandler'

fixture(`Student table tests`)
  .page(`http://localhost:3000/students/`)
  .beforeEach(signIn)

test('Add new student form. Name input test', async t => {
  await t
  .click(Selector('button[data-test-add-candidate-button]'))
  .typeText(Selector('textarea[mark="data-test-candidate-name-input"]'), 'Иванов Иван Иванович')
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'))
})