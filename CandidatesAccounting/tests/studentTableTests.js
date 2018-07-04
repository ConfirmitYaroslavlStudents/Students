import { Selector } from 'testcafe'
import { signIn } from './common'
import handleScreenshot from './handleScreenshot'

fixture(`Student table tests`)
  .page(`http://localhost:3000/students/`)
  .beforeEach(signIn)

test('Add new student form Name input', async t => {
  await t
  .click(Selector('button[data-test-add-candidate-button]'))
  .typeText(Selector('textarea[mark="data-test-candidate-name-input"]'), 'Иванов Иван Иванович')

  await handleScreenshot(t, Selector('div[data-test-candidate-form]'))
})

test('Add new Student form Email input', async t => {
  await t
  .click(Selector('button[data-test-add-candidate-button]'))
  .typeText(Selector('textarea[mark="data-test-student-group-name-input"]'), 'ПМИ-00')

  await handleScreenshot(t, Selector('div[data-test-candidate-form]'))
})