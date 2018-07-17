const formatUserName = (email) => {
  return email.split('@')[0].replace('.', ' ')
}

export default formatUserName