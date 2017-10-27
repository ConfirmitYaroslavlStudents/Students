import moment from 'moment';

export function getCurrentDateTime() {
  return moment().format('H:mm DD MMMM YYYY');
}

export function formatDate(day, month, year) {
  return moment(year + '-' + month + '-' + day).format('D MMMM YYYY');
}

export function formatDateTime(hours, minutes, day, month, year) {
  return moment(year + '-' + month + '-' + day + ' ' + hours + ':' + minutes).format('H:mm D MMMM YYYY');
}

export function isToday(date) {
  const currentDate = moment();
  const dateToCheck = moment(date, 'H:mm DD MMMM YYYY'); //TODO: correct work with DD MMMM YYYY
  return (dateToCheck.date() === currentDate.date() &&
          dateToCheck.month() === currentDate.month() &&
          dateToCheck.year() === currentDate.year());
}

export function isBirthDate(birthdate) {
  const currentDate = moment();
  const dateToCheck = moment(birthdate, 'DD MMMM YYYY');
  return (dateToCheck.date() === currentDate.date() &&
    dateToCheck.month() === currentDate.month());
}