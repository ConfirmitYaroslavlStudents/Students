//6. read articles

// 
// 1. transformer undefind in page;
// 2. Recalculation nodelist;
// 3. do not use cloneNode;
// 4. read about xml;

    function transformer(cssSelector) {
        var result = {};

        YUI().use('node', function (Y) {

            var nodelist = Y.all(cssSelector);

            function replaceNodes(node1, node2) {
                if (node1 == node2)
                    return;

                var temp = Y.Node.create('temp');

                node1.replace(temp);
                node2.replace(node1);
                temp.replace(node2);
            }

            result.rotateLeft = function () {
                for (var i = 0; i < nodelist.size() - 1 ; i++)
                    replaceNodes(Y.all(cssSelector).item(i), Y.all(cssSelector).item(i + 1));
                
                return result;
            };

            result.rotateRight = function () {
                for (var i = nodelist.size() - 1; i > 0; i--)
                    replaceNodes(Y.all(cssSelector).item(i), Y.all(cssSelector).item(i-1));

                return result;
            };


            result.randomize = function () {
                for (var i = 0; i < nodelist.size() - 1; i++) {
                    var random = Math.floor((Math.random() * (nodelist.size() - 1)));

                    replaceNodes(Y.all(cssSelector).item(i), Y.all(cssSelector).item(random));
                }

                return result;
            };
        });

        return result;
        
    };