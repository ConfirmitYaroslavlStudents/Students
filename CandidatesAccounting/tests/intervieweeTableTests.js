import { Selector } from 'testcafe'
import { signIn } from './common'
import toMatchScreenshot from '../../ScreenshotHandler'

fixture(`Interviewee table tests`)
  .page(`http://localhost:3000/interviewees/`)
  .beforeEach(signIn)

test('Add new interviewee form. Name input test', async t => {
  await t
  .click(Selector('button[data-test-add-candidate-button]'))
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'), {screenshotName: 'emptyForm'})

  await t
  .typeText(Selector('textarea[mark="data-test-candidate-name-input"]'), 'Иванов Иван Иванович')
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'), {screenshotName: 'formWithName'})

  await t
  .typeText(Selector('textarea[mark="data-test-candidate-nickname-input"]'), 'Никнейм')
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'))
})

test('Add new interviewee form. Email input test', async t => {
  await t
  .click(Selector('button[data-test-add-candidate-button]'))
  .typeText(Selector('textarea[mark="data-test-candidate-email-input"]'), 'ivanov.ivan@mail.com')
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'), {screenshotName: 'formWithEmail'})
})