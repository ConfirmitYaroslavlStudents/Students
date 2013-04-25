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
                replaceNodes(Y.all(cssSelector).item(i), Y.all(cssSelector).item(i - 1));

            return result;
        };


        result.randomize = function () {
            for (var i = 0; i < nodelist.size() - 1; i++) {
                var random = Math.floor((Math.random() * (nodelist.size() - 1)));

                replaceNodes(Y.all(cssSelector).item(i), Y.all(cssSelector).item(random));
            }

            return result;
        };

        result.check = function (config) {
            var counter = 0,
                value;

            if (config.type == 'equal')
                value = config.count;
            else
                value = config.maxCount;

            for (var i = 0; i < nodelist.size() ; i++)
                if (nodelist.item(i).get('checked') == true) {
                    counter++;

                    if (counter == value) {
                        for (var j = 0; j < nodelist.size() ; j++) {
                            if (nodelist.item(j).get('checked') !== true)
                                nodelist.item(j).set('disabled', 'true');
                        }
                    }
                    else
                        for (var p = 0; p < nodelist.size() ; p++)
                            nodelist.item(p).set('disabled', false);
                }
        }

        result.checkOfSend = function (e,config) {
            var counter = 0;

            for (var i = 0; i < nodelist.size() ; i++) {
                if (nodelist.item(i).get('checked') == true)
                    counter++;
            }
            if (config.type == 'equal') {
                if (config.count == counter) {
                    alert('form is send!');
                    return true;
                }
                alert('You must check ' + config.count + ' fields');
                e.preventDefault();
            }
            else {
                if (config.minCount <= counter && counter <= config.maxCount) {
                    alert('form is send!');
                    return true;
                }
                alert('You must check ' + config.minCount + ' to ' + config.maxCount + ' fields');
                e.preventDefault();
            }

        }


    });

    return result;

};