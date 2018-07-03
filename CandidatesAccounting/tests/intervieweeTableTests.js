import { Selector } from 'testcafe'
import { signIn } from './common'
import handleScreenshot from './handleScreenshot'

const screenshotDirectory = './tests/screenshots/'

fixture `Interviewee table`
  .page(`http://localhost:3000/interviewees/`)
  .beforeEach(signIn())

test('Empty update interviewee form', async t => {
  const screenshotFileName = 'empty-update-interviewee-form.png'

  const openAddCandidateDialogButton = Selector('button[data-test-add-candidate-button]')
  const candidateForm = Selector('div[data-test-candidate-form]')

  await t
  .click(openAddCandidateDialogButton)
  .takeElementScreenshot(candidateForm, screenshotFileName)

  const screenshotHandleResult = await handleScreenshot(screenshotDirectory, screenshotFileName, 0)

  await t
  .expect(screenshotHandleResult).ok('There is a difference between screenshots. Check ' + screenshotDirectory + screenshotFileName.split('.')[0] + '-diff.png')
})