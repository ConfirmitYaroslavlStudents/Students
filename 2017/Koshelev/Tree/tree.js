function Tree() {
	this._value = this._left = this._right = this._par = null;
}

Tree.prototype.getValue = function() {
	return this._value;
}

Tree.prototype.setValue = function(val) {
	this._value = val;
}

Tree.prototype.getLeft = function() {
	return this._left;
}

Tree.prototype.setLeft = function(node) {
	this._left = node;
}

Tree.prototype.getRight = function() {
	return this._right;
}

Tree.prototype.setRight = function(node) {
	this._right = node;
}

Tree.prototype.getParent = function() {
	return this._parent;
}

Tree.prototype.setParent = function(node) {
	this._parent = node;
}

Tree.prototype.showTree = function() {
	function spaces(self) {
		var i = 0;
		while (self.getParent()) {
			i++;
			self = self.getParent();
		}
		var spaces = "";
		for (var j = 0; j < i; j++)
			spaces += "  ";
		return spaces;
	}

	if (this.getRight())
		this.getRight().showTree();

	console.log(spaces(this) + this.getValue());

	if (this.getLeft())
		this.getLeft().showTree();
}

Tree.prototype.findNode = function(val) { //return node closest to val
	var self = this;

	while (self != null) {
		var value = self.getValue();
		var left = self.getLeft();
		var right = self.getRight();
		if (value == val)
			return self;
		if (value < val)
			if (right)
				self = right;
			else
				return self;
		if (value > val)
			if (left)
				self = left;
			else
				return self;
	}
	return self;
}

Tree.prototype.addNode = function(val) { // add new node to tree
	var self = this.findNode(val);
	var value = self.getValue();

	if (value == val)
		return;

	if (value && value > val) {
		self.setLeft(new Tree());
		self.getLeft().setValue(val);
		self.getLeft().setParent(self);
		return;
	}
	if (value && value < val) {
		self.setRight(new Tree());
		self.getRight().setValue(val);
		self.getRight().setParent(self);
		return;
	}
	self.setValue(val); // for root
}

Tree.prototype.deleteNode = function(val) {
	var self = this.findNode(val);

	if (self.getValue() != val)
		return this;

	if (self.getParent() && self.getParent().getValue() > self.getValue()) { // not root and left son
		if (self.getRight()) {
			var replace = self.getRight();
			while (replace.getLeft())
				replace = replace.getLeft();

			replace.setLeft(self.getLeft()); // replace left son deleting node to the lowest son from right
			self.getLeft().setParent(replace);
			self.getParent().setLeft(self.getRight()); // replace deleted node by right son
			self.getRight().setParent(self.getParent()); // link right son to deleted node parent
		} else {
			if (self.getLeft()) {
				self.getParent().setLeft(self.getLeft());
				self.getLeft().setParent(self.getParent());
			} else {
				self.getParent().setLeft(null); // a leaf
			}
		}
	} else {
		if (self.getLeft()) { // root or right son
			var replace = self.getLeft();
			while (replace.getRight())
				replace = replace.getRight(); // find max on branch

			replace.setRight(self.getRight()); // link deleted node right son to max
			self.getRight().setParent(replace);
			if (self.getParent()) {
				self.getParent().setRight(self.getLeft()); // link with parent if it is
				self.getLeft().setParent(self.getParent());
			} else {
				self.getLeft().setParent(null);
			}
		} else {
			if (self.getRight()) { // root without left son
				if (self.getParent())
					self.getParent().setRight(self.getRight());
				self.getRight().setParent(self.getParent());
			} else {
				if (self.getParent()) // a leaf
					self.getParent().setRight(null);
			}
		}
	}

	var root = self.getParent() || self.getRight() || self.getLeft(); // return root
	while (root.getParent())
		root = root.getParent();
	self = null;
	return root;
}

module.exports = Tree;