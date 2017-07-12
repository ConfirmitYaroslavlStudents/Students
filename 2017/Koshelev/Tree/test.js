var assert = require('chai').assert;
var Tree = require('./tree');

describe("Tree", function(){
	var tree;
	describe('addNode', function(){
		it('should add new node if it does not exist', function(){
			var tree = new Tree();
			tree.addNode(5);
			tree.addNode(5);
			tree.addNode(2);
			tree.addNode(6);
			assert.equal(tree.getValue(), 5);
			assert.equal(tree.getRight().getValue(), 6);
			assert.equal(tree.getLeft().getValue(), 2);
			tree.showTree();
		});
	});
	describe('deleteNode - should delete node if it exists', function(){
		var tree;

		beforeEach("create a tree", function(){
			tree = new Tree();
			tree.addNode(20);
			tree.addNode(16);
			tree.addNode(25);
			tree.addNode(23);
			tree.addNode(28);
			tree.addNode(13);
			tree.addNode(18);
			tree.showTree();
		});

		afterEach('output tree', function(){
			tree.showTree();
			console.log("--------------------");
			tree = null;
		});

		it('delete root(20)', function(){
			tree = tree.deleteNode(20);
			assert.equal(tree.getValue(), 16);
			assert.equal(tree.getRight().getRight().getValue(), 25);
		});

		it('delete left son which have sons(16)', function(){
			tree = tree.deleteNode(16);
			assert.equal(tree.getLeft().getValue(), 18);
			assert.equal(tree.getLeft().getLeft().getValue(), 13);
		});

		it('delete right son which have sons(25)', function(){
			tree = tree.deleteNode(25);
			assert.equal(tree.getRight().getValue(), 23);
			assert.equal(tree.getRight().getRight().getValue(), 28);
		});

		it('delete left leaf(13)', function(){
			tree = tree.deleteNode(13);
			assert.equal(tree.getLeft().getLeft(), null);
		});

		it('delete right leaf(28)', function(){
			tree = tree.deleteNode(28);
			assert.equal(tree.getRight().getRight(), null);
		});

		it("don't delete anything if node doesn't exist", function(){
			tree = tree.deleteNode(15);
			assert.equal(tree.getLeft().getValue(), 16);
			assert.equal(tree.getLeft().getLeft().getValue(), 13);
			assert.equal(tree.getLeft().getRight().getValue(), 18);
		})
	})
})