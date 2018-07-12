'use strict';
const BinarySearchTreeNode = require('./BinarySearchTreeNode');

class BinarySearchTree
{
  constructor()
  {
    this._size = 0;
    this._root = null;
  }

  /**
   *
   */
  get root()
  {
    return this._root;
  }

  /**
   *
   */
  find(key)
  {
    let currentNode = this._root;

    while (currentNode !== null) {
      if (key === currentNode.key) {
        return currentNode;
      }
      if (key < currentNode.key) {
        currentNode = currentNode.leftNode;
      }
      else {
        currentNode = currentNode.rightNode;
      }
    }
    return currentNode;
  }

  /**
   *
   */
  get size()
  {
    return this._size;
  }

  /**
   *
   */
  min()
  {
    return this._findLeftMinNode(this._root).node;
  }

  /**
   *
   */
  max()
  {
    return this._findRightMaxNode(this._root).node;
  }

  /**
   *
   */
  insert(key, value)
  {
    if (this._root !== null) {
      this._insert(key, value, this._root);
    }
    else {
      this._size++;
      this._root = new BinarySearchTreeNode(key, value);
    }
    return this;
  }

  /**
   *
   */
  remove(key)
  {
    this._remove(key, this._root, null);
    return this;
  }

  /**
   *
   */
  _insert(key, value, node)
  {
    if (node === null) {
      this._size++;
      return new BinarySearchTreeNode(key, value);
    }
    if (key === node.key) {
      node.value = value;
    }
    else if (key < node.key) {
      node.leftNode = this._insert(key, value, node.leftNode);
    }
    else {
      node.rightNode = this._insert(key, value, node.rightNode);
    }
    return node;
  }

  /**
   *
   */
  _remove(key, node, parent)
  {
    if (node === null) {
      return;
    }

    if (key < node.key) {
      this._remove(key, node.leftNode, node);
      return;
    }
    if (key > node.key) {
      this._remove(key, node.rightNode, node);
      return;
    }

    if (node.leftNode !== null && node.rightNode !== null)
    {
      let minRightNode = this._findLeftMinNode(node.rightNode);

      node.key = minRightNode.key;
      node.value = minRightNode.value;

      this._remove(minRightNode.key, minRightNode.node, minRightNode.parent);
    }
    else if (node.leftNode !== null) {
      this._size--;
      node.clone(node.leftNode);
    }
    else if (node.rightNode !== null) {
      this._size--;
      node.clone(node.rightNode);
    }
    else if (parent !== null) {
      this._size--;
      if (key === parent.rightNode.key)
        parent.rightNode = null;
      else
        parent.leftNode = null;
    }
  }

  /**
   *
   */
  _findLeftMinNode(node)
  {
    let parent = null;
    while(node.leftNode !== null) {
      parent = node;
      node = node.leftNode;
    }
    return {node, parent};
  }

  /**
   *
   */
  _findRightMaxNode(node)
  {
    let parent = null;
    while(node.rightNode !== null) {
      parent = node;
      node = node.rightNode;
    }
    return {node, parent};
  }
}

module.exports = BinarySearchTree;
