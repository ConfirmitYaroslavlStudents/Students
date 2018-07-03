import { Selector } from 'testcafe'

const testEmail = 'test.test@confirmit.com'
const testPassword = 'password'

export function signIn() {
  return async t => {
    const emailInput = Selector('div[data-test-email-input]').find('input')
    const passwordInput = Selector('div[data-test-password-input]').find('input')
    const signInButton = Selector('button[data-test-sign-in-button]')

    await t
    .typeText(emailInput, testEmail)
    .typeText(passwordInput, testPassword)
    .click(signInButton)
  }
}