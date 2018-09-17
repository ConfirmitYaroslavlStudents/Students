import express from 'express'
import graphqlHTTP from 'express-graphql'
import authenticationCheckMiddleware from '../middlewares/authenticationCheck'
import graphqlSchema from '../graphql/schema'
import graphqlRoot from '../graphql/root'

const router = express.Router()

router.route('/graphql')
.all(authenticationCheckMiddleware)
.post(graphqlHTTP({
  schema: graphqlSchema,
  rootValue: graphqlRoot,
  graphiql: false
}))

export default router