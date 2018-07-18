import { Selector } from 'testcafe'

const testEmail = 'test.test@confirmit.com'
const testPassword = 'password'

export const signIn = async (t) => {
  const emailInput = Selector('input[mark=data-test-email-input]')
  const passwordInput = Selector('input[mark=data-test-password-input]')
  const signInButton = Selector('button[mark="data-test-sign-in-button"]')

  await t
  .typeText(emailInput, testEmail)
  .typeText(passwordInput, testPassword)
  .click(signInButton)
}