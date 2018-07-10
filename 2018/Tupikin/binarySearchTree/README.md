[![Build Status](https://travis-ci.org/MrRefactoring/red-black-tree-node.svg?branch=master)](https://travis-ci.org/MrRefactoring/red-black-tree-node)

# red-black-tree-node
A red-black tree written 100% in JavaScript. Works both in node.js and in the browser.

The classical variant of the representation of the red-black tree algorithm is the RedBlackTree class
# Install
```text
npm install red-black-tree-node
```
# Example
```js
let Tree = require('red-black-tree-node');

let tree = new Tree();

// Insert some items to tree
tree.insert('hello', 'Hello');
tree.insert('space', ' ');
tree.insert('world', 'World!');

// Remove something
tree.remove(' ');

// Find some items in the tree
console.log(tree.find('hello'), tree.find('world'));  // output: Hello World!
```

# API
```js
let Tree = require('red-black-tree-node');
```

# Path to code
Path to `Tree` sources is `src/components/redBlackTree/redBlackTree.js`

# Methods

## `tree.insert(key, value)`
Inserts new pair `key`-`value` to tree

* `key` is the key of the item to insert
* `value` is the value of the item to insert

## `tree.remove(key)`
Removes the item with `key` in the tree

* `key` is the key of the item to remove

## `tree.find(key)`
Retrieves the value associated to the given `key`

* `key` is the key of the item to find

**Return** The value of the node associated to `key`

## `tree.keys()`
A sorted array of all the keys in the tree

## `tree.values()`
An array of all the values in the tree

## `tree.findMax()`
**Returns** the maximum element in the tree

## `tree.findMin()`
**Returns** the minimum element in the tree

## `tree.removeMax()`
Finds and removes the maximum element in the tree

**Returns** removed element

## `tree.removeMin()`
Finds and removes the minimum element in the tree

**Returns** removed element

## `tree.length()`
**Returns** number of items in the tree

## `tree.height()`
**Returns** current tree height

## `tree.contains(key)`
**Returns** `true` if `key` include in the tree else returns `false`

## `tree.root()`
**Returns** root node of the tree

## `tree.left()`
**Returns** left node of the tree

## `tree.right()`
**Returns** right node of the tree

## `tree.toJson()`
Converts tree to JSON structure

**Returns** JSON string

## `tree.fromJson(json)`
Includes elements from JSON structure to current tree

## `tree.clone()`
**Returns** copy of the current tree

## `tree.iterator`
Anybody tree can be iterating

#### Example:
```js
const Tree = require('red-black-tree-node');

let tree = new Tree();

tree.insert(1, 'hello');
tree.insert(2, 'world!');

for (let key of tree)
    console.log(key);

// output:
// 1
// 2
```

# Credits
(C) 2018 Vladislav Tupikin. MIT License
