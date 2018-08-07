const esprima   = require('esprima');
const escodegen = require('escodegen');

const oldCallArgs        = new Object();
const excludedProperties = {range: true, tokens: true, comments: true, leadingComments: true, trailingComments: true};

module.exports = function (fileSource, oldCallSource, newCallSource) {
  let ast                = getAst(fileSource);
  let oldCallNode        = getAst(oldCallSource).body[0];

  if (oldCallNode.type == 'ExpressionStatement')
    oldCallNode = oldCallNode.expression;

  ast = changeChildrenNode(ast, oldCallNode, newCallSource);

  return toSource(ast);
};

function getAst(code) {
  return esprima.parse(code, {range: true, tokens: true, comment: true});
};

function changeChildrenNode(node, oldCallNode, newCallSource) {
  if (node === undefined || typeof(node) !== 'object')
    return node;

  for (let p in node) {
    if (excludedProperties[p] === true)
      continue;
    node[p] = changeChildrenNode(node[p], oldCallNode, newCallSource);
    node[p] = changeNode(node[p], oldCallNode, newCallSource);
  }
  return node;
};

function changeNode(node, oldCallNode, newCallSource) {
  if (node === null)
    return node;

  const isEqual = equalNodes(oldCallNode, node);
  if (isEqual) {
    let startPos       = { value: 0 };

    const newCallAst = getAst(newCallSource);
    substituteArguments(newCallAst);

    let result = newCallAst.body[0];
    if (result.comment !== undefined)
      result.comments = result.comments.concat(node.comments);
    if (result.type === 'ExpressionStatement')
      if (node.typy === result.type)
        return result;
      else
        return result.expression;
    else
      return result;
  }
  return node;
};

function toSource(ast) {
  ast = escodegen.attachComments(ast, ast.comments, ast.tokens);
  return escodegen.generate(ast, {comment: true});
}

function equalNodes(nodeA, nodeB) {
  if (typeof(nodeA) !== typeof(nodeB))
    return false;
  if(typeof(nodeA) !== 'object')
    return nodeA === nodeB;
  if(nodeA === null)
    return nodeB === null;
  if(nodeA.type !== nodeB.type)
    return false;

  for (let p in nodeA) {
    if (excludedProperties[p] === true)
      continue;
    if (p === 'arguments'){
      if(collectArgs(nodeA[p], nodeB[p]) === false)
        return false;
      continue;
    }
    if (equalNodes(nodeA[p], nodeB[p]) === false)
      return false;
  }

  return true;
};

function collectArgs(argsA, argsB) {
  if (argsA.length !== argsB.length)
    return false;
  for (let i = 0; i < argsA.length; i++) {
    if (argsA[i].type == 'Literal') {
       oldCallArgs[argsA[i].value] = argsB[i];
    }
    else
      if (equalNodes(argsA[i], argsB[i]) === false)
        return false;
  }
  return true;
}

function substituteArguments(ast) {
  let callExpressions = findCallExpressions(ast);
  for (let j in callExpressions) {
    let callExpressionArgs = callExpressions[j].arguments;
    for(let i in callExpressionArgs) {
      if(callExpressionArgs[i].type !== 'Literal')
        continue;

      let replacingArgument = oldCallArgs[callExpressionArgs[i].value];
      if(replacingArgument === undefined){
        throw new Error(`The argument ${callExpressionArgs[i].value} is not found`);
      }
      callExpressionArgs[i] = replacingArgument;
    }
  }
}

function findCallExpressions(node) {
  let res = [];

  if (node === undefined || typeof(node) !== 'object')
    return res;

  if (node.type === 'CallExpression')
    res.push(node);

  for (let p in node)
    res = res.concat(findCallExpressions(node[p]));

  return res;
};
