export default function formatUserName(email) {
  return email.split('@')[0].replace('.', ' ');
}