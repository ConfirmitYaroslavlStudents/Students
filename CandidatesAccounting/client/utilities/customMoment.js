import moment from 'moment'

export function getCurrentDate() {
  return moment().format('DD.MM.YYYY')
}

export function getCurrentDateTime() {
  return moment().format('H:mm DD.MM.YYYY')
}

export function formatDate(dateString) {
  const date = dateString.split('.')
  if (date.length !== 3) {
    return ''
  }
  const formatDate = moment(date[2] + '-' + date[1] + '-' + date[0]).locale('ru').format('D MMMM YYYY')
  return (formatDate !== 'Invalid date' ? formatDate : '')
}

export function formatDateTime(timeDateString) {
  const timeDate = timeDateString.split(' ')
  if (timeDate.length !== 2) {
    return ''
  }
  const time = timeDate[0].split(':')
  if (time.length !== 2) {
    return ''
  }
  const date = timeDate[1].split('.')
  if (date.length !== 3) {
    return ''
  }
  const formatDateTime = moment(date[2] + '-' + date[1] + '-' + date[0] + ' ' + time[0] + ':' + time[1]).locale('ru').format('H:mm D MMMM YYYY')
  return (formatDateTime !== 'Invalid date' ? formatDateTime : '')
}

export function toDatePickerFormat(momentDateString) {
  if (momentDateString && momentDateString !== '') {
    const momentDate = moment(momentDateString, 'DD.MM.YYYY')
    return momentDate.format('YYYY-MM-DD')
  } else {
    return ''
  }
}

export function fromDatePickerFormat(datePickerDateString) {
  if (datePickerDateString && datePickerDateString !== '') {
    const datePickerDate = moment(datePickerDateString, 'YYYY-MM-DD')
    return datePickerDate.format('DD.MM.YYYY')
  } else {
    return ''
  }
}

export function toDateTimePickerFormat(momentDateTimeString) {
  if (momentDateTimeString && momentDateTimeString!== '') {
    const momentDate = moment(momentDateTimeString, 'HH:mm DD.MM.YYYY')
    return momentDate.format('YYYY-MM-DDTHH:mm')
  } else {
    return ''
  }
}

export function fromDateTimePickerFormat(datePickerDateTimeString) {
  if (datePickerDateTimeString && datePickerDateTimeString !== '') {
    const datePickerDate = moment(datePickerDateTimeString, 'YYYY-MM-DDTHH:mm')
    return datePickerDate.format('HH:mm DD.MM.YYYY')
  } else {
    return ''
  }
}

export function isToday(date) {
  const currentDate = moment()
  const dateToCheck = moment(date, 'HH:mm DD.MM.YYYY')
  return (dateToCheck.date() === currentDate.date() &&
          dateToCheck.month() === currentDate.month() &&
          dateToCheck.year() === currentDate.year())
}

export function isBirthDate(birthdate) {
  const currentDate = moment()
  const dateToCheck = moment(birthdate, 'DD.MM.YYYY')
  return (dateToCheck.date() === currentDate.date() &&
    dateToCheck.month() === currentDate.month())
}