module.exports = function(fileInfo, api, options) {
  getOptions(options);

  const j            = api.jscodeshift;
  const root         = j(fileInfo.source);
  const codeNodes    = getStartNodes(root, j, fileInfo.path);
  const oldCallNodes = getStartNodes(j(options.old), j, 'options --old')[0];
  const newCallNodes = getStartNodes(j(options.new), j, 'options --new')[0];

  for (let x in codeNodes){
    args = new Object();
    if(equalNodes(oldCallNodes,codeNodes[x])){
      //модифицировать newCallNodes под нужные аргументы
      let q = j(options.new).find(j.CallExpression).forEach(function (p) {
        let nodes = p.node.arguments;
        for(let a in nodes)
          if(nodes[a].type=='Literal'){
            if(args[nodes[a].value] === undefined)
              throw new Error("Uncorrect argumets in options");
            nodes[a]=args[nodes[a].value];
          }
      }).toSource();

      codeNodes[x] = getStartNodes(j(q), j, 'options --new')[0];
      // вернуть в изначально состояние newCallNodes желательно не создавая заново
    }
  }

  return root.toSource();
  //return null;
};

function getOptions(options) {
  // считываем из файла и заносим в переменную options
  // костыль из-за не переносимости nomnom-ом строк
  options.old = "show('p1','p2');\n";
  options.new = "show.must('p1').go('p2').on(p3);\n";
}

function getStartNodes(ast, j, codeName) {
  try {
    return ast.find(j.Program).get('body').value;
  }
  catch (ex) {
    if(codeName !== undefined)
      ex.message += ` in ${codeName}`;
    throw ex;
  }
}

function equalNodes(nodeA, nodeB) {
  if(nodeA.type !== nodeB.type || typeComparator[nodeA.type] === undefined)
    return false;
  return typeComparator[nodeA.type](nodeA,nodeB);
}

const typeComparator = new Object();
let   args           = new Object();

typeComparator['ExpressionStatement'] = function (nodeA, nodeB) {
  return equalNodes(nodeA.expression,nodeB.expression);
}

typeComparator['CallExpression'] = function (nodeA, nodeB) {
  return typeComparator['arguments'](nodeA.arguments, nodeB.arguments) && equalNodes(nodeA.callee, nodeB.callee);
}

typeComparator['arguments'] = function (nodeA, nodeB){
  if(nodeA.length !== nodeB.length)
    return false;
  for (let i=0;i<nodeA.length;i++)
    if(nodeA[i].type == 'Literal') args[nodeA[i].value] = nodeB[i];
    else if(!equalNodes(nodeA[i], nodeB[i])) return false;
  return true;
}

typeComparator['Identifier'] = function (nodeA, nodeB) {
  return nodeA.name === nodeB.name;
}

typeComparator['MemberExpression'] = function (nodeA, nodeB) {
  if (nodeA.computed === nodeB.computed)
    return equalNodes(nodeA.object,nodeB.object) && equalNodes(nodeA.property, nodeB.property);
  return false;
}

typeComparator['Literal'] = function (nodeA, nodeB) {
  return nodeA.value === nodeB.value;
}

function setArguments(node) {

}
