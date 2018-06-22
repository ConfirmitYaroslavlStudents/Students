import { waitForReact, ReactSelector } from 'testcafe-react-selectors'

const testEmail = 'test.test@confirmit.com'
const testPassword = 'password'

export function signIn() {
  return async t => {
    await waitForReact()

    const emailInput = ReactSelector('Input').withProps({ type: 'email' })
    const passwordInput = ReactSelector('Input').withProps({ type: 'password' })
    const signInButton = ReactSelector('SignInButton')

    await t
    .typeText(emailInput, testEmail)
    .typeText(passwordInput, testPassword)
    .click(signInButton)
  }
}