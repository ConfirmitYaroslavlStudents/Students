import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import Paper from 'material-ui/Paper'
import Table, {
  TableBody,
  TableHead,
  TableRow,
  TableCell,
  TableFooter
} from 'material-ui/Table'

export default function CustomTable(props) {
  let heads = props.headers.map((head, index) =>
    <TableCell key={'th' + index}>{head}</TableCell>)
  let contentRows = props.rows.map((row, index) =>
    <TableRow key={'tr' + index}>
      {row.cells.map((cell, cellIndex) =>
        <TableCell key={'td' + cellIndex} classes={{root: row.isDisabled ? 'disabled-cell' : ''}}>{cell}</TableCell>)}
    </TableRow>
  )
  if (contentRows.size === 0 || contentRows.length === 0) {
    contentRows = (<TableRow><TableCell><EmptyTable>The table is empty</EmptyTable></TableCell></TableRow>);
  }
  return (
    <Paper style={
      {
        width: '100%',
        overflowX: 'auto',
      }
    }>
      <Table style={{minWidth: 1240}}>
        <TableHead>
          <TableRow>
            {heads}
          </TableRow>
        </TableHead>
        <TableBody>
          {contentRows}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TableCell colSpan={props.headers.length}>
              {props.footerActions}
            </TableCell>
          </TableRow>
        </TableFooter>
      </Table>
    </Paper>
  )

}

CustomTable.propTypes = {
  headers: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  rows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  footerActions: PropTypes.object.isRequired,
}

const EmptyTable = styled.div`
  color: #aaa;
  text-align: center;
  position: absolute;
  width: 100%;
  margin-top: -12px;
`