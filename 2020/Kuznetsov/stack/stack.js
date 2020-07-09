const Stack = (function () {
    let _size = 0;
    let _storage = {};

    return {
        push: function(...data) {
            [...data].forEach((el) => {
                let size = ++_size;
                _storage[size] = el;
            })
            return [...data]
        },

        pop: function() {
            let size = _size,
                deletedData;
         
            if (size) {
                deletedData = _storage[size];
         
                delete _storage[size];
                _size--;
         
                return deletedData;
            } else {
                return false; // Stack is Empty
            }
        },

        peek: function() {
            let size = _size;
         
            if (size) {
                return _storage[size];
            } else {
                return false; // Stack is Empty
            }
        },

        size: function() {
            return _size;
        },

        isEmpty: function() {
            return _size === 0;
        },

        clear: function() {
            _size = 0;
            _storage = {};
            return 'clear';
        },

        toArray: function() {
            return Object.values(_storage)
        }
    }
})();

module.exports = Stack