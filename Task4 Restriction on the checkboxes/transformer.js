//6. read articles

// 0. Onclick going to call the constructor and creation new object.
// 1. Call function check and checkOfSend with the same object.
// 2. Operation "check" one, but it use two methods;
// 3. split check function  - use 2 private functions in it

// preventDefault in native js.
// form.onsubmit = function () { return false } ???

// fiddler: construct post request to ya.ru;

function transformer(cssSelector) {
    var result = {};

    YUI().use('node', function (Y) {
        var nodelist = Y.all(cssSelector),
            nodelistSize = nodelist.size(),
            temp = Y.Node.create('temp'),
            currentCheckedListCount = 0;

        function replaceNodes(node1, node2) {
            if (node1 == node2)
                return;

            node1.replace(temp);
            node2.replace(node1);
            temp.replace(node2);
        }

        result.rotateRight = function () {
            for (var i = 0; i < nodelistSize - 1 ; i++)
                replaceNodes(nodelist.item(i), nodelist.item(i + 1));

            return result;
        };

        result.rotateLeft = function () {
            for (var i = nodelistSize - 1; i > 0; i--)
                replaceNodes(nodelist.item(i), nodelist.item(i - 1));

            return result;
        };

        result.randomize = function () {
            for (var i = 0; i < nodelistSize - 1; i++) {
                var random = Math.floor((Math.random() * (nodelistSize - 1)));

                replaceNodes(nodelist.item(i), nodelist.item(random));
            }

            return result;
        };

        result.check = function (config) {

            Y.all(cssSelector).on('click', check);
            Y.one('input[type=submit]').on('click', checkOfSend);

            function checkOnTheMarked(e) {
                var value;

                value = config.type == 'equal' ? config.count : config.maxCount;

                if (e.target.get('checked') !== true)
                    currentCheckedListCount--;
                else
                    currentCheckedListCount++;

                if (currentCheckedListCount == value) {
                    for (var i = 0; i < nodelistSize ; i++) {
                        if (nodelist.item(i).get('checked') !== true)
                            nodelist.item(i).set('disabled', 'true');
                    }
                }
                else
                    for (var i = 0; i < nodelistSize ; i++)
                        nodelist.item(i).set('disabled', false);
            };

            function checkOfSend(e) {
                if (config.type == 'equal') {
                    if (config.count == currentCheckedListCount) {
                        alert('form is send!');
                        return true;
                    }
                    alert('You must check ' + config.count + ' fields');
                }
                else {
                    if (config.minCount <= currentCheckedListCount && currentCheckedListCount <= config.maxCount) {
                        alert('form is send!');
                        return true;
                    }
                    alert('You must check ' + config.minCount + ' to ' + config.maxCount + ' fields');
                }
                e.preventDefault();

            };

        }

    });

    return result;

};