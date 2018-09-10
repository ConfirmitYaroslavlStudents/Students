import express from 'express'
import graphqlHTTP from 'express-graphql'
import graphqlSchema from '../graphql/schema'
import graphqlRoot from '../graphql/root'

const router = express.Router()

router.route('/graphql')
.all((req, res, next) => {
  if (req.isAuthenticated()) {
    next()
  } else {
    return res.status(401).end()
  }
})
.all(graphqlHTTP({
  schema: graphqlSchema,
  rootValue: graphqlRoot,
  graphiql: false
}))

export default router