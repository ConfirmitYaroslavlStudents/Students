'use strict';
const BinarySearchTreeNode = require('./../BinarySearchTreeNode');

it('key getter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  expect(node.key).toEqual(5);
})

it('key setter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  node.key = 10;
  expect(node.key).toEqual(10);
})

it('value getter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  expect(node.value).toEqual('5');
})

it('value setter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  node.value = 10;
  expect(node.value).toEqual(10);
})

it('leftNode getter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  expect(node.leftNode).toEqual(null);
})

it('leftNode setter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  const leftNode = new BinarySearchTreeNode(55);

  node.leftNode = leftNode;
  expect(node.leftNode).toEqual(leftNode);
})

it('rightNode getter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  expect(node.rightNode).toEqual(null);
})

it('rightNode setter', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  const rightNode = new BinarySearchTreeNode(55);

  node.rightNode = rightNode;
  expect(node.rightNode).toEqual(rightNode);
})

it('clone method', () =>
{
  const node = new BinarySearchTreeNode(5, '5');
  node.clone(new BinarySearchTreeNode(4, '8'))

  expect(node.key).toEqual(4);
  expect(node.value).toEqual('8');
})
