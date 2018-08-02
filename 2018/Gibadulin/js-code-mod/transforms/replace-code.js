module.exports = function (fileInfo, api, options) {
  getOptions();

  const excludedProperties = {loc: true, start: true, end: true, comments: true, leadingComments: true, trailingComments: true};
  const oldCallArgs        = new Object();

  const j                   = api.jscodeshift;
  const root                = getAST(fileInfo.source, fileInfo.path);
  const firstCodeNode       = root.find(j.Program).get().node;
  let   oldCallNode         = getBodyNodes(getAST(options.old, 'option --old'))[0];
  const newCallExpressions  = getAST(options.new, 'option --new').find(j.CallExpression);

  if (oldCallNode.type == 'ExpressionStatement')
    oldCallNode = oldCallNode.expression;

  changeChildrenNode(firstCodeNode);

  return root.toSource();



  function getOptions() {
    const fs = require('fs');

    let text = fs.readFileSync(options.optPath, 'utf8').split('\n');

    options.old = text[0];
    options.new = text[1];
  }

  function getAST(code, codeName) {
    try {
      return j(code);
    }
    catch (ex) {
      if (codeName !== undefined)
        ex.message += ` in ${codeName}`;
      throw ex;
    }
  }

  function getBodyNodes(ast) {
      return ast.find(j.Program).get('body').value;
  }

  function changeChildrenNode(node) {
    if (node === undefined || typeof(node) !== 'object')
      return node;
    for (let p in node)
    {
      if (excludedProperties[p] === true)
        continue;
      node[p] = changeChildrenNode(node[p]);
      node[p] = changeNode(node[p]);
    }
    return node;
  }

  function changeNode(node) {
    if (node === null)
      return node;

    const isEqual = equalNodes(oldCallNode, node);
    if (isEqual) {
      let startPos       = { value: 0 };
      let newCallArgs    = [];
      let changedNewCall = newCallExpressions.forEach(p => substituteArguments(p, newCallArgs)).toSource();
      let result         = getBodyNodes(j(changedNewCall))[0];
      result.comments    = node.comments;
      newCallExpressions.forEach(p => retrieveArguments(p, newCallArgs, startPos));

      return result;
    }
    else return node;
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
  }

  function collectArgs(argsA, argsB) {
    if (argsA.length !== argsB.length)
      return false;
    for (let i = 0; i < argsA.length; i++) {
      if(argsA[i].type == 'Literal') {
         oldCallArgs[argsA[i].value] = argsB[i];
      }
      else
        if(equalNodes(argsA[i], argsB[i]) === false)
          return false;
    }
    return true;
  }

  function substituteArguments(callExpressionPath, previousArgs) {
    const callExpressionArgs = callExpressionPath.node.arguments;
    for(let i in callExpressionArgs) {
      if(callExpressionArgs[i].type !== 'Literal') {
        previousArgs.push(undefined);
        continue;
      }
      const replacingArgument = oldCallArgs[callExpressionArgs[i].value];
      if(replacingArgument === undefined){
        throw new Error(`The argument ${callExpressionArgs[i].value} is not found`);
      }
      previousArgs.push(callExpressionArgs[i]);
      callExpressionArgs[i] = replacingArgument;
    }
  }

  function retrieveArguments(callExpressionPath, args, startPos) {
    let additive = 0;
    const callExpressionArgs = callExpressionPath.node.arguments;
    for(let i in callExpressionArgs) {
      additive++;
      if (args[Number(i) + startPos.value] === undefined)
        continue;
      callExpressionArgs[i] = args[Number(i) + startPos.value];
    }
    startPos.value += additive;
  }
};
