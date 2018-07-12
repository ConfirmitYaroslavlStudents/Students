'use strict';
const BinarySearchTree = require('./../BinarySearchTree');

it('root getter', () =>
{
  const tree = new BinarySearchTree();
  expect(tree.root).toEqual(null);

  tree.insert(55);
  expect(tree.root.key).toEqual(55);
});

it('find method', () =>
{
  const tree = new BinarySearchTree();

  tree.insert(3);
  tree.insert(1);
  tree.insert(2);
  tree.insert(0);

  expect(tree.find(1).key).toEqual(1);
})

it('size method', () =>
{
  const tree = new BinarySearchTree();

  tree.insert(3);
  tree.insert(1);
  tree.insert(2);
  tree.insert(0);

  expect(tree.size).toEqual(4);
})

it('min method', () =>
{
  const tree = new BinarySearchTree();

  tree.insert(3);
  tree.insert(1);
  tree.insert(2);
  tree.insert(0);

  expect(tree.min().key).toEqual(0);
})

it('max method', () =>
{
  const tree = new BinarySearchTree();

  tree.insert(3);
  tree.insert(1);
  tree.insert(2);
  tree.insert(0);

  expect(tree.max().key).toEqual(3);
})

it('insert method', () =>
{
  const tree = new BinarySearchTree();

  tree.insert(3);
  tree.insert(1);
  tree.insert(-2);
  tree.insert(2);
  tree.insert(5);

  expect(tree.size).toEqual(5);
  expect(tree.root.key).toEqual(3);
  expect(tree.root.rightNode.key).toEqual(5);
  expect(tree.root.leftNode.key).toEqual(1);
  expect(tree.root.leftNode.leftNode.key).toEqual(-2);
  expect(tree.root.leftNode.rightNode.key).toEqual(2);
})

it('remove method', () =>
{
  const tree = new BinarySearchTree();

  tree.insert(3);
  tree.insert(1);
  tree.insert(-2);
  tree.remove(1);

  expect(tree.find(1)).toEqual(null);
})
