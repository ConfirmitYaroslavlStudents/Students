import express from 'express'
import graphqlHTTP from 'express-graphql'
import { root, schema } from '../graphQL'

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
  schema: schema,
  rootValue: root,
  graphiql: false
}))

export default router