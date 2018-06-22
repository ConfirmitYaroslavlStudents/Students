import { Selector } from 'testcafe'

fixture `Getting Started`
  .page `http://localhost:4000/`

test('My first test', async t => {
  await t
  .typeText('#email-input', 'test@confirmit.com')
  .typeText('#password-input', 'password')
  .click('#sign-in-button')
})