//1. Source graph destroed;
//2. Class graph not adjustable;
//3. Copy not work;

YUI().use('node', function (Y) {

    var graph = Y.Node.create('<div>');
    Y.one('body').appendChild(graph);

    graph.insert('<p id="a" >A</p>');
    graph.insert('<p id="b" >B</p>');
    graph.insert('<p id="c" >C</p>');
    graph.insert('<p id="d" >D</p>');
    graph.insert('<p id="e" >E</p>');
    graph.insert('<p id="f" >F</p>');
    graph.insert('<p id="g" >G</p>');

});


Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

function createGraph() {

    var Graph = {
        V: [],
        E: [],
        copyE: [],

        addVertex: function (id) {
            this.V.push({
                'id': id
            });
        },

        addEdge: function (left, right) {
            this.E.push({
                'left': this.V[left],
                'right': this.V[right]
            })
        },

        copy: function (vertex) {
            var _self = this;
            var tree = {
                node: 'root',
                children: [
                    {
                        node: vertex,
                        children: []
                    }
                ]
            };

            search(tree);

            function copyingVertex(vertex) {
                var copyVertex = {
                    id: vertex.id
                };
                return copyVertex;
            }

            function makeEdgesCopy() {
                _self.copyE = [];

                for (var i = 0; i < _self.E.length; i++)
                    _self.copyE.push(copyingEdge(_self.E[i]));
            }

            function copyingEdge(edge) {
                copyEdge = {
                    right: edge.right,
                    left: edge.left
                }
                return copyEdge;
            }

            function search(treeNode) {
                var childrenVertices = [];
                treeNodeChildren = treeNode.children.slice();

                makeEdgesCopy();

                while (treeNodeChildren.length > 0) {
                    lookUp(treeNodeChildren, childrenVertices);
                    treeNodeChildren = childrenVertices.slice();
                    childrenVertices = [];
                }
            };

            function lookUp(treeNodeChildren, childrensVertices) {

                if (treeNodeChildren.length > 0) {
                    var treeNode = treeNodeChildren.pop();
                    for (i = 0; i < _self.copyE.length; i++) {
                        if (_self.copyE[i].left.id == treeNode.node.id) {
                            var el = { node: copyingVertex(_self.copyE[i].right), children: [] };
                            childrensVertices.push(el);
                            treeNode.children.push(el);

                            if (check(treeNode, treeNodeChildren)) {
                                _self.copyE.remove(i);
                                i--;
                            }
                            continue;
                        }
                        if (_self.copyE[i].right.id == treeNode.node.id) {
                            var el = { node: copyingVertex(_self.copyE[i].left), children: [] };
                            childrensVertices.push(el);
                            treeNode.children.push(el);

                            if (check(treeNode, treeNodeChildren)) {
                                _self.copyE.remove(i);
                                i--;
                            }
                            continue;
                        }
                    }
                    lookUp(treeNodeChildren, childrensVertices);
                    alert(treeNode.node.id);
                }
            };

            function check(treeNode, treeNodes) {
                for (var i = 0; i < treeNodes.length; i++) {
                    if (treeNode.node.id == treeNodes[i].node.id) return false;
                }
                return true;
            };

            return tree;

        },


        dump: function (treeNode, container) {
            var output = document.getElementById(container);

            print(treeNode, 0);

            function print(treenode, deep) {
                printTreeNode(treenode, deep);

                for (var i = 0; i < treenode.children.length; i++) {
                    print(treenode.children[i], deep + 1);
                }
            }
            function printTreeNode(treeNode, deep) {
                var symbol = '&#8627;';
                for (var i = 0; i < deep; i++)
                    symbol += '-';

                output.innerHTML += symbol + treeNode.node.id + '<br/>';
            }
        }
    }

    return Graph;
}