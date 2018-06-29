import { ReactSelector } from 'testcafe-react-selectors'
import { signIn } from './common'

fixture `Interviewee table`
  .page(`http://localhost:3000/interviewees/`)
  .beforeEach(signIn())

test('Add new interviewee. Total amount increased', async t => {
  const rowAmountDisplayElement = ReactSelector('RowAmountDisplay')

  const rowAmountDisplayComponent = await ReactSelector('RowAmountDisplay').getReact()
  const expectedCandidateAmount = rowAmountDisplayComponent.props.total + 1

  const openAddCandidateDialogButton = ReactSelector('AddCandidateDialog').findReact('div').findReact('IconButton')
  const nameInput = ReactSelector('CandidateInfoForm TextField').withProps({ label: 'Name' })
  const addCandidateButton = ReactSelector('AddCandidateDialogActions').findReact('div').findReact('IconButton')

  await t
  .click(openAddCandidateDialogButton)
  .typeText(nameInput, 'New Interviewee Test')
  .click(addCandidateButton)

  const rowAmountDisplayComponentUpdated = await rowAmountDisplayElement.getReact()
  const currentCandidateAmount = rowAmountDisplayComponentUpdated.props.total

  await t
  .expect(currentCandidateAmount).eql(expectedCandidateAmount)
})