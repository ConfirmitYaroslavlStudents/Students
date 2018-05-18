export default function getStudentTableHeaders() {
  return [
    {title: 'Name', sortingField: 'name'},
    {title: 'Actions'},
    {title: 'Learning start', sortingField: 'startingDate'},
    {title: 'Learning end', sortingField: 'endingDate'},
    {title: 'Group', sortingField: 'groupName'},
    {title: 'E-mail', sortingField: 'email'},
    {title: 'Phone Number'}
  ]
}