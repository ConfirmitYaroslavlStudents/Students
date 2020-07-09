class StackArray {
    constructor() {
        this._storage = [];
    }

    push = (...elements) => {     
        [...elements].forEach((el) => {
            this._storage.push(el);
        })
        return [...elements] 
    }

    pop = () => {     
        return this._storage.pop();   
    }     

    peek = () => {     
        return this._storage[this._storage.length - 1];   
    }

    isEmpty = () => {     
        return this._storage.length === 0;   
    }

    clear = () => {     
        this._storage = [];   
        return 'clear';
    }

    size = () => {     
        return this._storage.length;   
    }

    toArray = () => {
        return this._storage;
    }
}

module.exports = StackArray;