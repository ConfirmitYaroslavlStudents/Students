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
const Tree = require('red-black-tree-node');

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
const Tree = require('red-black-tree-node');
```

# Path to code
Path to `Tree` sources is `src/components/redBlackTree/redBlackTree.js`

# Methods

## `tree.insert(key, value)`
Inserts new pair `key`-`value` to tree


* `key` is the key of the item to insert
* `value` is the value of the item to insert

**Complexity:** `O(log(n))`

## `tree.remove(key)`
Removes the item with `key` in the tree

* `key` is the key of the item to remove

**Complexity:** `O(log(n))`

Exist small disbalance (if remove 400 000 random elements from 1 000 000 then height of tree is 26, not 19, that need). [Issue](https://github.com/MrRefactoring/red-black-tree-node/issues/1)

## `tree.find(key)`
Retrieves the value associated to the given `key`

* `key` is the key of the item to find

**Complexity:** `O(log(n))`

**Return** The value of the node associated to `key`

## `tree.keys()`
A sorted array of all the keys in the tree

**Complexity:** `O(n)`

## `tree.values()`
An array of all the values in the tree

**Complexity:** `O(n)`

## `tree.findMax()`
**Complexity:** `O(log(n))`

**Returns** the maximum element in the tree

## `tree.findMin()`
**Complexity:** `O(log(n))`

**Returns** the minimum element in the tree

## `tree.removeMax()`
Finds and removes the maximum element in the tree

**Complexity:** `O(log(n))`

**Returns** removed element

## `tree.removeMin()`
Finds and removes the minimum element in the tree

**Complexity:** `O(log(n))`

**Returns** removed element

## `tree.length()`
**Complexity:** `O(1)`

**Returns** number of items in the tree

## `tree.height()`
**Complexity:** `O(n)` :(

You can find a faster algorithm, I'll be glad if you submit your solution or the idea [here](https://github.com/MrRefactoring/red-black-tree-node/issues/2)

**Returns** current tree height

## `tree.contains(key)`
**Complexity:** `O(log(n))`

**Returns** `true` if `key` include in the tree else returns `false`

## `tree.root()`
**Complexity:** `O(1)`

**Returns** root node of the tree

## `tree.left()`
**Complexity:** `O(1)`

**Returns** left node of the tree

## `tree.right()`
**Complexity:** `O(1)`

**Returns** right node of the tree

## `tree.isEmpty()`
**Complexity:** `O(1)`

**Returns** `true` if tree is empty else return `false`

## `tree.toJSON()`
Converts tree to JSON structure

**Complexity:** `O(n)`

**Returns** JSON string

## `tree.fromJSON(json)`
Includes elements from JSON structure to current tree

**Complexity:** `O(n)`

## `tree.clone()`
**Complexity:** `O(n)`

**Returns** copy of the current tree

## `for (let key of tree)`
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
