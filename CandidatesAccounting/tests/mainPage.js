import { ReactSelector } from 'testcafe-react-selectors'
import { signIn } from './common'

fixture `Main page`
  .page(`http://localhost:3000/`)
  .beforeEach(signIn())

test('Authorization. Username has correct format', async t => {
  await t
  .expect(ReactSelector('UsernameWrapper').innerText).eql('test test')
})