
//6. read articles


// 1. no div #list, use existing parents
// 2. declare private var - selected nodes list
// 3. 
YUI().use('node', function (Y) {

    function transformer(cssSelector) {
        var result = {};

        result.rotateLeft = function () {
            var nodelist = Y.all(cssSelector),
                firstNode = nodelist.item(0);

            for (i = 0; i < nodelist.size()-1 ; i++) {
                var previousNode = Y.all(cssSelector).item(i);
                previousNode.replace(Y.all(cssSelector).item(i + 1).cloneNode(true));
            }

            Y.all(cssSelector).item(nodelist.size() - 1).replace(firstNode);

            return this;
        };

        result.rotateRight = function () {
            var nodelist = Y.all(cssSelector),
                lastNode = nodelist.item(nodelist.size() - 1);

            for (i = nodelist.size()-1; i > 0; i--) {
                var previousNode = Y.all(cssSelector).item(i);

                previousNode.replace(Y.all(cssSelector).item(i-1).cloneNode(true));
            }

            Y.all(cssSelector).item(0).replace(lastNode);

            return result;
        };


        result.randomize = function () {
            var nodelist = Y.all(cssSelector);

            for (i = 0; i < nodelist.size() ; i++) {
                var random = Math.floor((Math.random() * (nodelist.size() - 1))),
                    node = Y.all(cssSelector).item(i),
                    randomNode = Y.all(cssSelector).item(random);

                if (i == random)
                    continue;
                
                node.replace(Y.all(cssSelector).item(random).cloneNode(true));
                randomNode.replace(node);
            }

            return result;
        };

        return result;
    };
    window.transformer=transformer;
});