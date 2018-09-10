const authenticationCheckMiddleware = (req, res, next) => {
  if (req.isAuthenticated()) {
    next()
  } else {
    return res.status(401).end()
  }
}

export default authenticationCheckMiddleware