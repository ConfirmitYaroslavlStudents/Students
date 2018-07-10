#!/usr/bin/env node
'use strict';

const RED   = false;
const BLACK = !RED;

class Node{

    constructor(color, key, value){
        this.key    = key;
        this.value  = value;
        this.color  = color;

        this.left   = null;
        this.right  = null;

        this.length = 1;
    }

    // region Insertion region

    static insert(node, key, value){
        if (!node) return new Node(RED, key, value);

        let cmp = Node.compare(key, node.key);

        if      (cmp < 0) node.left  = Node.insert(node.left,  key, value);
        else if (cmp > 0) node.right = Node.insert(node.right, key, value);
        else              node.value = value;


        // fix-up any right-leaning links
        if (Node.isRed(node.right) && !Node.isRed(node.left))      node = Node.rotateLeft (node);
        if (Node.isRed(node.left)  &&  Node.isRed(node.left.left)) node = Node.rotateRight(node);
        if (Node.isRed(node.left)  &&  Node.isRed(node.right))     Node.flipColors(node);

        node.length = Node._length(node.left) + Node._length(node.right) + 1;
        return node;
    }

    static rotateLeft(node){
        let currentNode        = node.right;
        node.right             = currentNode.left;
        currentNode.left       = node;
        currentNode.color      = currentNode.left.color;
        currentNode.left.color = RED;
        currentNode.length       = node.length;
        node.length              = Node._length(node.left) + Node._length(node.right) + 1;
        return currentNode;
    }

    static rotateRight(node){
        let currentNode         = node.left;
        node.left               = currentNode.right;
        currentNode.right       = node;
        currentNode.right       = node;
        currentNode.color       = currentNode.right.color;
        currentNode.right.color = RED;
        currentNode.length        = node.length;

        node.length               = Node._length(node.left) + Node._length(node.right) + 1;
        return currentNode;
    }

    static flipColors(node){
        node.color = !node.color;
        node.left.color = !node.left.color;
        node.right.color = !node.right.color;
    }

    // endregion

    // region Remove region

    /**
     * Removes a key-value pair from a tree
     * @param node - the node from the subtree of which you want to delete the element
     * @param key the key
     * @returns {Node} new balanced tree
     */
    static remove(node, key){
        if (Node.compare(key, node.key) < 0) {
            if (!Node.isRed(node.left) && !Node.isRed(node.left.left)) node = Node.moveRedLeft(node);
            node.left = Node.remove(node.left, key);
        } else {
            if (Node.isRed(node.left))
                node = Node.rotateRight(node);
            if (Node.compare(key, node.key) === 0 && (node.right == null))
                return null;
            if (!Node.isRed(node.right) && !Node.isRed(node.right.left))
                node = Node.moveRedRight(node);
            if (Node.compare(key, node.key) === 0) {
                let minimalNode = Node.min(node.right);
                node.key   = minimalNode.key;
                node.val   = minimalNode.val;
                node.right = Node.remove(node.right, Node.min(node.right).key);
            } else
                node.right = Node.remove(node.right, key);
        }
        return Node.balance(node);
        // while (node){
        //     let cmp = Node.compare(key, node.key);
        //     if      (cmp < 0) node = node.left;
        //     else if (cmp > 0) node = node.right;
        //     else              break;
        // }
        // if (!node) return;
        // if (!node.left)  // case 1
    }

    /**
     * Assuming that node is red and both node.left and node.left.left are black,
     * make node.left or one of its children red.
     * @param node the target node
     * @returns {Node} new connection of nodes
     */
    static moveRedLeft(node){
        Node.flipColors(node);
        if (Node.isRed(node.right.left)) {
            node.right = Node.rotateRight(node.right);
            node =       Node.rotateLeft (node);
            Node.flipColors(node);
        }
        return node;
    }

    /**
     * Assuming that node is red and both node.right and node.right.left are black,
     * make node.right or one of its children red.
     * @param node the target node
     * @returns {Node} new connection of nodes
     */
    static moveRedRight(node){
        Node.flipColors(node);
        if (Node.isRed(node.left.left)) {
            node = Node.rotateRight(node);
            Node.flipColors(node);
        }
        return node;
    }

    /**
     * Balances the tree
     * @param node the node that needs balance
     * @returns {Node} rebalanced node
     */
    static balance(node){
        if (Node.isRed(node.right))                              node = Node.rotateLeft (node);
        if (Node.isRed(node.left) && Node.isRed(node.left.left)) node = Node.rotateRight(node);
        if (Node.isRed(node.left) && Node.isRed(node.right))     Node.flipColors(node);

        node.length = Node._length(node.left) + Node._length(node.right) + 1;
        return node;
    }

    // endregion

    // region Helpers

    /**
     * Going down to the most left leaf from the transferred node and returns it
     * @param node the start node
     * @returns minimal node
     */
    static min(node) {
        if (node == null || node === undefined) return undefined;
        while (node.left)  // While can select left subtree
            node = node.left;
        return node;
    }

    /**
     * Going down to the most right leaf from the transferred node and returns it
     * @param node the start node
     * @returns maximal node
     */
    static max(node){
        if (node == null || node === undefined) return undefined;
        while (node.right)  // While can select right subtree
            node = node.right;
        return node;
    }

    /**
     * Returns the number of children of this node
     * @param node the node that needs to know the number of children
     * @returns {number}
     */
    static _length(node){
        return node ? node.length: 0;
    }

    /**
     * Returns the maximum node height (Indicates how well balanced)
     * @returns {number}
     */
    static height(node){
        return node ? Math.max(Node.height(node.left), Node.height(node.right)) + 1: -1;
    }

    /**
     * Returns the {@code true} if this node is red otherwise returns {@code false}
     * @param node
     * @returns {boolean}
     */
    static isRed(node){
        return node ? node.color === RED: false;
    }

    /**
     * Returns the {@code true} if this node is leaf otherwise returns {@code false}
     * @param node
     * @returns {boolean}
     */
    static isLeaf(node){
        return node ? !node.left && !node.right: true;
    }

    /**
     * Passes through a tree and collects a sorted array
     * @param node the start node
     * @param array - output array
     * @return {Array}
     */
    static keys(node, array){
        if (!node) return;
        Node.keys(node.left, array);
        array.push(node.key);
        Node.keys(node.right, array);
    }

    /**
     * Passes through a tree and collects nodes a sorted array
     * @param startNode the start node
     * @param outputArray - output array
     * @return {Array}
     */
    static getNodes(startNode, outputArray){
        if (!startNode) return;
        Node.getNodes(startNode.left, outputArray);
        outputArray.push(startNode);
        Node.getNodes(startNode.right, outputArray);
    }

    static compare(a, b){
        if (typeof a === 'string' || typeof b === 'string'){
            a = a.toString();
            b = b.toString();
        }
        if      (a < b) return -1;
        else if (a > b) return  1;
        else            return  0;
    }

    // endregion

}

module.exports = {Node};
