const INIT_CAPACITY = 4
let _capacity = INIT_CAPACITY,
    _tail = 0,
    _head = 0,
    _storage; 

class CircleQueue {
    init() {
        _storage = new Array(_capacity); // [empty × _capacity]
        return new CircleQueue();
    }

    size() {
        // return quantity is not empty elements
        return _storage.map(el => Boolean(el)).filter(() => Boolean).length
    }

    show() {
        return _storage;
    }

    enqueue(value) {
        if (this.size() < _capacity) {
            _storage[_head] = value            
        } else {
            let currentCapacity = _capacity
            let currentStorage = _storage
            _capacity *= 2

            for (let i = 0; i < _tail; i++) {
                currentStorage.push(currentStorage[i])
            }

            _storage = new Array(_capacity)
            for (let i = 0, j = _tail; j < _tail + currentCapacity; i++, j++) {
                _storage[i] = currentStorage[j]
            }

            _tail = 0
            _head = currentCapacity

            _storage[_head] = value
            _head += 1

            return _storage
        }

        if (_head + 1 != _capacity) {
            _head += 1
        } else {
            _head = 0
        }
    }

    dequeue() {
        if (this.size() > 0) {
            let deletedData = _storage[_tail]
            delete _storage[_tail]
    
            if (_tail + 1 != _capacity) {
                _tail += 1
            } else {
                _tail = 0
            }
    
            return deletedData;
        } else {
            throw new Error('Queue is empty') // or create a custom error
        }
    }

    head() {
        return _head;
    }
    
    tail() {
        return _tail;
    }

    capacity() {
        return _capacity;
    }
}

module.exports = CircleQueue;