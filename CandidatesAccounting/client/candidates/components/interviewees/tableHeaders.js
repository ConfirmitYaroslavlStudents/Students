export default function getIntervieweeTableHeaders() {
  return [
    {title: 'Name', sortingField: 'name'},
    {title: 'E-mail', sortingField: 'email'},
    {title: 'Birth Date'},
    {title: 'Interview date', sortingField: 'interviewDate'},
    {title: 'Resume'},
    {title: 'Actions'}
  ]
}