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

var Graph = {

    V: [
        {
            id: 'A',
        },
        {
            id: 'B',
        },
        {
            id: 'C',
        },
        {
            id: 'D',
        },
        {
            id: 'E',
        },
        {
            id: 'F',
        },
        {
            id: 'G',
        }
    ],

    E: [],

    
    copy: function (vertex) {
        var _self = this;
        var tree = {
            node: 'root',
            children: [
                {
                    node: vertex,
                    children: []
                }]
        };

        search(tree);
        
        function search(treeNode) {
            var childrenVertices = [];
            treeNodeChildren = treeNode.children.slice();
            while (treeNodeChildren.length > 0) {
                lookUp(treeNodeChildren, childrenVertices);
                treeNodeChildren = childrenVertices.slice();
                childrenVertices = [];
            }
        };

        function lookUp(treeNodeChildren, childrensVertices) {
            if (treeNodeChildren.length > 0) {
                var treeNode = treeNodeChildren.pop();
                for (i = 0; i < _self.E.length; i++) {
                    if (_self.E[i].left == treeNode.node) {
                        var el = { node: _self.E[i].right, children: [] };
                        childrensVertices.push(el);
                        treeNode.children.push(el);

                        if (check(treeNode, treeNodeChildren)) {
                            _self.E.remove(i);
                            i--;
                        }
                        continue;
                    }
                    if (_self.E[i].right == treeNode.node) {
                        var el = { node: _self.E[i].left, children: [] };
                        childrensVertices.push(el);
                        treeNode.children.push(el);

                        if (check(treeNode, treeNodeChildren)) {
                            _self.E.remove(i);
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
                if (treeNode.node == treeNodes[i].node) return false;
            }
            return true;
        };

        return tree;

    },

    addEdge: function (left, right) {
        this.E.push({
            'left': left,
            'right': right
        })
    },
    addVertex: function (id) {
        this.V.push({
            'id': id
        });
    },

    init: function () {
        this.addEdge(this.V[0], this.V[1]); // A--B
        this.addEdge(this.V[0], this.V[2]); // A--C
        this.addEdge(this.V[1], this.V[3]); // B--D
        this.addEdge(this.V[2], this.V[3]); // C--D
        this.addEdge(this.V[3], this.V[4]); // D--E
        this.addEdge(this.V[3], this.V[5]); // D--F
        this.addEdge(this.V[4], this.V[6]); // E--G
        this.addEdge(this.V[5], this.V[6]); // F--G
    },

}

Graph.init();