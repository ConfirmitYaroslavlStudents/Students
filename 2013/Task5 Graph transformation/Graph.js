var graph = {

    edges: [],
    vertexes: {},

    createVertex: function(name) {
        var vertex = {
            name: name,
        }
        return vertex;
    },

    pushEdge: function (leftVertex, rightVertex) {
        var l = this.vertexes[leftVertex],
            r = this.vertexes[rightVertex];

        if (!l) l = this.vertexes[leftVertex] = this.createVertex(leftVertex);
        if (!r) r = this.vertexes[rightVertex] = this.createVertex(rightVertex);

        this.edges.push({ leftV: l, rightV: r });
    },

    buildTree: function (layer) {
        var newLayer = [];

        while (layer.length > 0) {
            var vertex = layer.pop();
            for (var i = 0; i < this.edges.length; i++) {

                var e = this.edges[i];
                if (e.check == true) continue;

                var child = null;

                if (vertex.name == e.leftV.name) child = e.rightV;
                if (vertex.name == e.rightV.name) child = e.leftV;

                if (child) {
                    if (!vertex[child.name])
                        vertex[child.name] = child;

                    newLayer.push(child);
                    e.check = true;
                }
            }
        }
        if (newLayer.length > 0)
            this.buildTree(newLayer);
    },

    copy: function (name) {
        this.buildTree([this.vertexes[name]]);
        return this.vertexes[name];
    },

    dump: function (tree, indent) {

        indent += "_ ";

        document.getElementById('tree').innerHTML += indent + tree.name + "</br>";
        for (var p in tree) {
            if (p !== 'name') {
                this.dump(tree[p], indent);
            }
        }
    }

}