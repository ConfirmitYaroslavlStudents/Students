import moment from 'moment'
import 'moment/locale/ru.js'

export const getCurrentDateTime = () => {
  return moment().format('H:mm:ss DD.MM.YYYY')
}

export const formatDate = (dateString) => {
  if (!dateString) {
    return ''
  }
  const date = dateString.split('.')
  if (date.length !== 3) {
    return ''
  }
  const formatDate = moment(date[2] + '-' + date[1] + '-' + date[0]).locale('ru').format('D MMMM YYYY')
  return (formatDate !== 'Invalid date' ? formatDate : '')
}

export const formatDateTime = (timeDateString) => {
  if (!timeDateString) {
    return ''
  }
  const timeDate = timeDateString.split(' ')
  if (timeDate.length !== 2) {
    return ''
  }
  const time = timeDate[0].split(':')
  if (!time[2])
    time[2] = '00'
  if (time.length !== 3) {
    return ''
  }
  const date = timeDate[1].split('.')
  if (date.length !== 3) {
    return ''
  }
  const formatDateTime = moment(date[2] + '-' + date[1] + '-' + date[0] + ' ' + time[0] + ':' + time[1] + ':' + time[2]).locale('ru').format('H:mm D MMMM YYYY')
  return (formatDateTime !== 'Invalid date' ? formatDateTime : '')
}

export const toDatePickerFormat = (momentDateString) => {
  if (momentDateString && momentDateString !== '') {
    const momentDate = moment(momentDateString, 'DD.MM.YYYY')
    return momentDate.format('YYYY-MM-DD')
  } else {
    return ''
  }
}

export const fromDatePickerFormat = (datePickerDateString) => {
  if (datePickerDateString && datePickerDateString !== '') {
    const datePickerDate = moment(datePickerDateString, 'YYYY-MM-DD')
    return datePickerDate.format('DD.MM.YYYY')
  } else {
    return ''
  }
}

export const toDateTimePickerFormat = (momentDateTimeString) => {
  if (momentDateTimeString && momentDateTimeString!== '') {
    const momentDate = moment(momentDateTimeString, 'HH:mm DD.MM.YYYY')
    return momentDate.format('YYYY-MM-DDTHH:mm')
  } else {
    return ''
  }
}

export const fromDateTimePickerFormat = (datePickerDateTimeString) => {
  if (datePickerDateTimeString && datePickerDateTimeString !== '') {
    const datePickerDate = moment(datePickerDateTimeString, 'YYYY-MM-DDTHH:mm')
    return datePickerDate.format('HH:mm DD.MM.YYYY')
  } else {
    return ''
  }
}

export const isToday = (date) => {
  const currentDate = moment()
  const dateToCheck = moment(date, 'HH:mm DD.MM.YYYY')
  return (dateToCheck.date() === currentDate.date() &&
          dateToCheck.month() === currentDate.month() &&
          dateToCheck.year() === currentDate.year())
}