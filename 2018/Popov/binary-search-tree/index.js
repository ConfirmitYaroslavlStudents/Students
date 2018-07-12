const BinarySearchTree = require('./src/BinarySearchTree');

let tree = new BinarySearchTree();

tree.insert(1, '1');
tree.insert(2, '2');
tree.insert(0, '0');
tree.insert(10, '10');

tree.remove(0);
// tree.remove(10);
tree.insert(2.5, '2.5');
tree.insert(-1, '-1');
tree.remove(2);

// console.log(tree);
// console.log(tree.find(-1));

console.log(tree.max());
console.log(tree.min());
