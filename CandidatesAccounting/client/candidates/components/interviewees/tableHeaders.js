export default function getIntervieweeTableHeaders() {
  return [
    {title: 'Name', sortingField: 'name'},
    {title: 'Actions'},
    {title: 'Resume'},
    {title: 'Interview date', sortingField: 'interviewDate'},
    {title: 'E-mail', sortingField: 'email'},
    {title: 'Phone number'}
  ]
}