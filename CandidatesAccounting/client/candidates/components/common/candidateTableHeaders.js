export default function getCandidateTableHeaders() {
  return [
    {title: 'Name', sortingField: 'name'},
    {title: 'Actions'},
    {title: 'Status', sortingField:'status'},
    {title: 'E-mail', sortingField: 'email'},
    {title: 'Phone Number'}
  ]
}