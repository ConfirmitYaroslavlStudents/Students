import { ReactSelector } from 'testcafe-react-selectors'
import { signIn } from './common'

fixture `Interviewee table`
  .page(`http://localhost:4000/interviewees/`)
  .beforeEach(signIn())

test('Add new interviewee. Total amount changes', async t => {
  const rowAmountDisplayElement = await ReactSelector('RowAmountDisplay')

  const rowAmountDisplayComponent = await rowAmountDisplayElement.getReact()
  const expectedCandidateAmount = rowAmountDisplayComponent.props.total + 1

  const openAddCandidateDialogButton = await ReactSelector('IconButton').withProps({ id: 'add-candidate-button' })
  const nameInput = ReactSelector('CandidateInfoForm TextField').withProps({ label: 'Name' })
  const addCandidateButton = await ReactSelector('IconButton').withProps({ id: 'confirm-add-candidate' })

  await t
  .click(openAddCandidateDialogButton)
  .typeText(nameInput, 'New Interviewee Test')
  .click(addCandidateButton)

  const rowAmountDisplayComponentUpdated = await rowAmountDisplayElement.getReact()
  const currentCandidateAmount = rowAmountDisplayComponentUpdated.props.total

  await t
  .expect(currentCandidateAmount).eql(expectedCandidateAmount)
})