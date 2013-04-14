//0. remove random start position from rotate
//   rotate in both diretions
//1. create randomize function
//2. create object with rotate and randomize funcs ( use closure)
//3. chain calls myObj('css selector').randomize().rotate();
//4. rename file
//5. global newIndex
//6. read articles


YUI().use('node', function (Y) {

    function transformer(cssSelector) {

        this.rotate = function (direction) {
            var nodelist = Y.all(cssSelector);

            if (direction == undefined)
                direction = 1;

            Y.all(cssSelector).remove();

            for (var i = 0; i < nodelist.size() ; i++) {
                var newIndex = ((i + nodelist.size() + direction) % nodelist.size());
                Y.one('#list').appendChild(nodelist.item(newIndex));
            };

            return this;
        };

        this.randomize = function () {
            var nodelist = Y.all(cssSelector),
                nodeArray = [];

            for (var i = 0; i < nodelist.size() ; i++) {
                var currentList = Y.all(cssSelector);
                var random = Math.floor((Math.random() * (currentList.size() - 1)));
                var node = currentList.item(random);
                nodeArray.push(node);
                node.remove();
            }
            for (var i = 0; i < nodeArray.length; i++)
                Y.one('#list').appendChild(nodeArray[i]);
        }

    }

    Y.one('#btRotateRight').on('click', function () {
        new transformer('a').rotate(-1);
    });

    Y.one('#btRotateLeft').on('click', function () {
        new transformer('a').rotate(1);
    });


    Y.one('#btRandomize').on('click', function () {
        new transformer('a').rotate().randomize();
    });

});