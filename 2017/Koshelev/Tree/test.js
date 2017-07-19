var assert = require('chai').assert;
var Tree = require('./treeES5').Tree;
var Node = require('./treeES5').Node;

describe("Tree", function(){
	var tree;
	describe('findNode + addNode', function(){
		it('should return null if node not found', function(){
			var tree = new Tree();
			assert.equal(null, tree.findNode(2));
		});
		it('should add and find node', function(){
			var tree = new Tree();
			tree.addNode(5);
			tree.addNode(4);
			tree.addNode(7);
			assert.equal(tree.findNode(5).getValue(), 5);
			assert.equal(tree.findNode(4).getValue(), 4);
			assert.equal(tree.findNode(7).getValue(), 7);
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
		});

		afterEach('output tree', function(){
			tree = null;
		});

		it('delete root(20)', function(){
			tree.deleteNode(20);
			assert.equal(null, tree.findNode(20));
		});

		it('delete left son which have sons(16)', function(){
			tree.deleteNode(16);
			tree.showTree();
			assert.equal(tree.findNode(16), null);
		});

		it('delete right son which have sons(25)', function(){
			tree.deleteNode(25);
			assert.equal(tree.findNode(25), null);
		});

		it('delete left leaf(13)', function(){
			tree.deleteNode(13);
			assert.equal(tree.findNode(13), null);
		});

		it('delete right leaf(28)', function(){
			tree.deleteNode(28);
			assert.equal(tree.findNode(28), null);
		});

		it("don't delete anything if node doesn't exist", function(){
			tree.showTree();
			assert.equal(tree.findNode(15), null);
			tree.deleteNode(15);
			tree.showTree();
			let nodes = [20, 16, 25, 23, 28, 13, 18];
			for (let i = 0; i < nodes.length; i++)
				assert.equal(tree.findNode(nodes[i]).getValue(), nodes[i]);
		})
	})
})
