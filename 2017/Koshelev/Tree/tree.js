'use strict'

class Tree {

	constructor() {
		this.root = null;
	}

	findNode(val) {
		if (this.root && this.root.findNode(val).getValue() == val)
			return this.root.findNode(val);
		return null;
	}

	addNode(val) {
		let node = null;
		
		if (this.root)
			node = this.root.findNode(val);
		else{
			this.root = new Node(val);
			return true;
		}

		if (node.getValue() == val)
			return false;
		
		node.addNode(val);
	}

	deleteNode(val) {
		let node = null;
		if (this.root)
			node = this.root.findNode(val);
		if (!node)
			return false;
		if (node.getValue() != val)
			return false;
		if (node == this.root)
			this.root = node.deleteNode();
		else
			node.deleteNode();
		return true;
	}

	showTree(){
		if (this.root)
			this.root.showTree();
	}
}

class Node {

	constructor(val) {
		this.value = val;
		this.parent = this.left = this.right = null;
	}

	getValue() {
		return this.value;
	}

	setValue(val) {
		this.value = val;
	}

	getLeft() {
		return this.left;
	}

	setLeft(node) {
		this.left = node;
	}

	getRight() {
		return this.right;
	}

	setRight(node) {
		this.right = node;
	}

	getParent() {
		return this.parent;
	}

	setParent(node) {
		this.parent = node;
	}

	addNode(val) {
		if (val > this.value) {
			this.right = new Node(val);
			this.right.setParent(this);
		} else {
			this.left = new Node(val);
			this.left.setParent(this);
		}
	}

	findNode(val) {
		if (val == this.value)
			return this;
		if (val < this.value)
			if (this.left)
				return this.left.findNode(val);
			else 
				return this;
		if (val > this.value)
			if (this.right)
				return this.right.findNode(val);
			else 
				return this;
	}

	deleteNode() {
		if (!this.parent)
			return this.removeRoot();

		if (this.parent.getValue() > this.value) {
			if (this.right) {
				this.replaceByRightSon();
			} else {
				if (this.left)
					this.left.setParent(this.parent);
				this.parent.setLeft(this.left);
			}
		} else {
			if (this.left) {
				this.replaceByLeftSon();
			} else {
				if (this.right)
					this.right.setParent(this.parent);
				this.parent.setRight(this.right);
			}
		}
	}

	replaceByRightSon() {
		let low = this.findLowestFromRight();

		low.setLeft(this.left);
		if (this.left)
			this.left.setParent(low);

		this.right.setParent(this.parent);
		if (this.parent)
			this.parent.setLeft(this.right);
	}

	replaceByLeftSon() {
		let low = this.findLowestFromLeft();

		low.setRight(this.right);
		if (this.right)
			this.right.setParent(low);

		this.left.setParent(this.parent);
		if (this.parent)
			this.parent.setRight(this.left);
	}

	findLowestFromLeft() {
		let self = this.left;

		while (self.getRight()) {
			self = self.getRight();
		}

		return self;
	}

	findLowestFromRight() {
		let self = this.right;

		while (self.getLeft()) {
			self = self.getLeft();
		}

		return self;
	}

	removeRoot() {
		if (this.left) {
			this.replaceByLeftSon();
			return this.left;
		}
		if (this.right) {
			this.replaceByRightSon();
			return this.right;
		}
		return null;
	}

	spaces(){
		let self = this;
		let i = 0
		while(self.getParent())
		{
			self = self.getParent();
			i++;
		}
		let spaces = "";
		for (let j = 0; j < i; j++)
			spaces += " ";
		return spaces;
	}

	showTree(){
		if (this.right)
			this.right.showTree();
		console.log(this.spaces() + this.value);
		if (this.left)
			this.left.showTree();
	}
}

exports.Tree = Tree;
exports.Node = Node;
