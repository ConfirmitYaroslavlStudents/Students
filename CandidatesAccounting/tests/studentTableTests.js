import { Selector } from 'testcafe'
import { signIn } from './common'
import toMatchScreenshot from './toMatchScreenshot'

fixture(`Student table tests`)
  .page(`http://localhost:3000/students/`)
  .beforeEach(signIn)

test('Add new student form. Name input test', async t => {
  await t
  .click(Selector('button[data-test-add-candidate-button]'))
  .typeText(Selector('textarea[mark="data-test-candidate-name-input"]'), 'Иванов Иван Иванович')
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'))
})

test('Add new Student form. Email input test', async t => {
  await t
  .click(Selector('button[data-test-add-candidate-button]'))
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'))

  await t
  .typeText(Selector('textarea[mark="data-test-student-group-name-input"]'), 'ПМИ-1')
  await toMatchScreenshot(t, Selector('div[data-test-candidate-form]'))
})