module.exports = function (fileInfo, api, options) {
  getOptions(options);

  const j                   = api.jscodeshift;
  const root                = getAST(fileInfo.source, j, fileInfo.path);
  const codeNodes           = getBodyNodes(root, j);
  const oldCallNode         = getBodyNodes(getAST(options.old, j, "option --old"), j)[0];
  const newCallExpressions  = getAST(options.new, j, 'option --new').find(j.CallExpression);

  for (let x in codeNodes) {
    const isEqual = equalNodes(oldCallNode, codeNodes[x]);
    if (isEqual) {
      let startPos       = { value: 0 };
      let newCallArgs    = [];
      let changedNewCall = newCallExpressions.forEach(p => substituteArguments(p, newCallArgs)).toSource();
      codeNodes[x]       = getBodyNodes(j(changedNewCall), j)[0];
      newCallExpressions.forEach(p => retrieveArguments(p, newCallArgs, startPos));
    }
  }

  return root.toSource();
};

function getOptions(options) {
  const fs = require('fs');

  let text = fs.readFileSync(options.optPath, 'utf8').split('\n');

  options.old = text[0];
  options.new = text[1];
}

function getAST(code, j, codeName) {
  try {
    return j(code);
  }
  catch (ex) {
    if (codeName !== undefined)
      ex.message += ` in ${codeName}`;
    throw ex;
  }
}

function getBodyNodes(ast, j) {
    return ast.find(j.Program).get('body').value;
}

function equalNodes(nodeA, nodeB) {
  if(nodeA.type !== nodeB.type || typeComparator[nodeA.type] === undefined)
    return false;
  return typeComparator[nodeA.type](nodeA, nodeB);
}

const typeComparator = new Object();
const oldCallArgs    = new Object();

typeComparator['ExpressionStatement'] = function (nodeA, nodeB) {
  return equalNodes(nodeA.expression, nodeB.expression);
}

typeComparator['CallExpression'] = function (nodeA, nodeB) {
  return typeComparator['arguments'](nodeA.arguments, nodeB.arguments) && equalNodes(nodeA.callee, nodeB.callee);
}

typeComparator['arguments'] = function (nodeA, nodeB) {
  if (nodeA.length !== nodeB.length)
    return false;
  for (let i = 0; i < nodeA.length; i++) {
    if(nodeA[i].type == 'Literal') {
       oldCallArgs[nodeA[i].value] = nodeB[i];
    }
    else if(!equalNodes(nodeA[i], nodeB[i])) return false;
  }
  return true;
}

typeComparator['Identifier'] = function (nodeA, nodeB) {
  return nodeA.name === nodeB.name;
}

typeComparator['MemberExpression'] = function (nodeA, nodeB) {
  if (nodeA.computed === nodeB.computed)
    return equalNodes(nodeA.object, nodeB.object) && equalNodes(nodeA.property, nodeB.property);
  return false;
}

typeComparator['Literal'] = function (nodeA, nodeB) {
  return nodeA.value === nodeB.value;
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
    if (args[Number(i)+startPos.value] === undefined)
      continue;
    callExpressionArgs[i] = args[Number(i)+startPos.value];
  }
  startPos.value+=additive;
}
