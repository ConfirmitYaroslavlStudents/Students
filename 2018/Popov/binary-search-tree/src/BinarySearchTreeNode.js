'use strict';

class BinarySearchTreeNode
{
  constructor(key, value)
  {
    this._key = key;
    this._value = value;
    this._leftNode = null;
    this._rightNode = null;
  }

  /**
   *
   */
  clone(node)
  {
    this._key = node.key;
    this._value = node.value;
    this._leftNode = node.leftNode;
    this._rightNode = node.rightNode;
  }

  /**
   * @returns {*}
   */
  get key()
  {
    return this._key;
  }

  /**
   * @param {*} data
   */
  set key(data)
  {
    this._key = data;
  }

  /**
   * @returns {*}
   */
  get value()
  {
    return this._value;
  }

  /**
   * @param {*} data
   */
  set value(data)
  {
    this._value = data;
  }

  /**
   * @returns {BinarySearchTreeNode}
   */
  get leftNode()
  {
    return this._leftNode;
  }

  /**
   * @param {BinarySearchTreeNode} node
   */
  set leftNode(node)
  {
    this._leftNode = node;
  }

  /**
   * @returns {BinarySearchTreeNode}
   */
  get rightNode()
  {
    return this._rightNode;
  }

  /**
   * @param {BinarySearchTreeNode} node
   */
  set rightNode(node)
  {
    this._rightNode = node;
  }
}

module.exports = BinarySearchTreeNode;
