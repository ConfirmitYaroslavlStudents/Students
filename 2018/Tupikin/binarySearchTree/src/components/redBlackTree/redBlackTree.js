#!/usr/bin/env node
'use strict';

/**
 * @author Vladislav Tupikin, 2018
 * @licence MIT
 * @version 1.1.5
 */

const {Node}       = require('./node');
const {Exceptions} = require('./exceptions');

const RED   = false;
const BLACK = !RED;

class RedBlackTree{

    constructor(key, value){
        this._root = key != null || key !== undefined ? new Node(BLACK, key, value): null;
    }

    // region Basic functionality

    /**
     * Writes a key-value pair to the tree.
     * If the value is {@code null} or {@code undefined},
     * then the key and the value associated with it are deleted from the tree.
     * @param key the key
     * @param value the value
     * @returns {undefined} (void method)
     * @throws IllegalArgumentException if {@code key} is {@code null} or {@code undefined}
     */
    insert(key, value){
        if (key   == null || key   === undefined) throw Exceptions.IllegalArgumentException(`Key argument equals ${key}`);
        if (value == null || value === undefined) return this.remove(key);

        // Searching the place to insertion, inserting pair and rebalancing of the tree
        this._root       = Node.insert(this._root, key, value);
        this._root.color = BLACK;  // Marking the _root of the tree as black
    }

    /**
     * It walk the tree before the first match of the searched key with key of the node.
     * @param key the key
     * @returns value that corresponds to the key. If such a value is not found, then {@code undefined} is returned
     */
    find(key){
        let currentNode = this._root;  // Start point for tree walk
        while (!Node.isLeaf(currentNode)){  // Walking of the tree, until node not a leaf (analog of binary search)
            let cmp = RedBlackTree.compare(key, currentNode.key);
            if      (cmp < 0) currentNode = currentNode.left;
            else if (cmp > 0) currentNode = currentNode.right;
            else              return currentNode.value
        }
        if (currentNode && currentNode.key === key) return currentNode.value;
        return undefined;  // Returns undefined if the tree does not contain the node with key for searching
    }

    /**
     * Deletes a pair of nodes from the tree corresponding to the specified key.
     * If the transmitted key is equal to {@code null} or {@code undefined} then make an exception
     * @param key the key
     */
    remove(key){
        if (key == null || key === undefined) throw Exceptions.IllegalArgumentException(`argument is ${key}`);
        if (!this.contains(key))              return;

        // If both children of _root are black, set _root to red
        if (!Node.isRed(this._root.left) && !Node.isRed(this._root.right)) this._root.color = RED;

        this._root = Node.remove(this._root, key);  // Searching node for removing, removing and rebalancing of the tree
        if (!Node.isLeaf(this._root)) this._root.color = BLACK;  // Marking _root as black
    }

    // endregion

    // region Additional functionality

    /**
     * Passes from the _root to the rightmost leaf and returns its key
     * @returns maximal key in the tree
     */
    findMax(){
        return Node.max(this._root).key;
    }

    /**
     * Passes from the _root to the leftmost leaf and returns its key
     * @returns minimal key in the tree
     */
    findMin() {
        return Node.min(this._root).key;
    }

    /**
     * Find and remove maximal element in tree
     * @return removed element
     */
    removeMax(){
        let max = this.findMax();
        this.remove(max);
        return max;
    }

    /**
     * Find and remove minimal element in tree
     * @return removed element
     */
    removeMin(){
        let min = this.findMin();
        this.remove(min);
        return min;
    }

    /**
     * Checks the exists of a key in a tree
     * @param key the key
     * @returns {boolean} true if key exists in the tree, else returns false
     */
    contains(key){
        return this.find(key) !== undefined
    }

    /**
     * Returns the maximum tree height (Indicates how well balanced)
     * @returns {number}
     */
    height(){
        return Node.height(this._root)
    }

    /**
     * Returns the number of children in the tree
     * @returns {number}
     */
    length(){
        return Node._length(this._root)
    }

    // endregion

    // region Common part

    /**
     * Returns sorted array of keys
     * @return {Array}
     */
    keys(){
        if (!this._root) return [];
        let array = [];
        Node.keys(this._root, array);
        return array;
    }

    /**
     * Returns array of values
     * @return {Array}
     */
    values(){
        if (!this._root) return [];
        let nodes = [];
        Node.getNodes(this._root, nodes);
        for (let i = 0; i < nodes.length; i++)
            nodes[i] = nodes[i].value
        return nodes
    }

    /**
     * Return _root node
     * @return {null || Node}
     */
    root(){
        return this._root;
    }

    /**
     * Return left child of _root;
     * @return {null || Node}
     */
    left(){
        return this._root.left;
    }

    /**
     * Return right child of _root;
     * @return {null || Node}
     */
    right(){
        return this._root.right;
    }

    /**
     * Convert tree to Json structure
     * @return {String} json representation
     */
    toJson(){
        if (!this._root) return JSON.stringify([]);
        let nodes = [];
        Node.getNodes(this._root, nodes);
        for (let i = 0; i < nodes.length; i++)
            nodes[i] = {key: nodes[i].key, value: nodes[i].value}
        return JSON.stringify(nodes);
    }

    /**
     * Convert Json to tree representation
     * @param json - input json string
     */
    fromJson(json){
        json = JSON.parse(json);
        for (let element of json)
            this.insert(element.key, element.value)
    }

    /**
     * Clone this tree
     * @return {RedBlackTree}
     */
    clone(){
        return new RedBlackTree().fromJson(this.toJson());
    }

    static compare(a, b){
        if (typeof a === 'string' || typeof b === 'string'){
            a = a.toString();
            b = b.toString();
        }
        if      (a < b) return -1;
        else if (a > b) return  1;
        else            return  0
    }

    /**
     * Standard iterator
     * @return {{next: (function(): {value: *, done: boolean})}}
     */
    [Symbol.iterator]() {
        let index = -1;
        let data = this.values();

        return {
            next: () => ({ value: data[++index], done: !(index in data) })
        };
    };

    // endregion

}

module.exports = RedBlackTree;
