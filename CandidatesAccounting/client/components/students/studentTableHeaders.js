export default function getStudentTableHeaders() {
  return [
    {title: 'Name', sortingField: 'name'},
    {title: 'E-mail', sortingField: 'email'},
    {title: 'Birth Date'},
    {title: 'Group', sortingField: 'group'},
    {title: 'Learning start', sortingField: 'startingDate'},
    {title: 'Learning end', sortingField: 'endingDate'},
    {title: 'Actions'}]
}