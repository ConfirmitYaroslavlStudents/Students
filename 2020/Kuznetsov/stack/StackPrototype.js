// function StackFunction () {
//     this._size = 0;
//     this._storage = {};
// }
 
// StackFunction.prototype.push = function(...data) {
//     [...data].forEach((el) => {
//         let size = ++this._size;
//         this._storage[size] = el;
//     })
// };
 
// StackFunction.prototype.pop = function() {
//     let size = this._size,
//         deletedData;
 
//     if (size) {
//         deletedData = this._storage[size];
 
//         delete this._storage[size];
//         this._size--;
 
//         return deletedData;
//     } else {
//         return 'Stack is Empty';
//     }
// };

// StackFunction.prototype.peek = function() {
//     let size = this._size;
 
//     if (size) {
//         return this._storage[size];
//     } else {
//         return 'Stack is Empty';
//     }
// };

// StackFunction.prototype.size = function() {
//     return this._size;
// };

// StackFunction.prototype.isEmpty = function() {
//     return this._size === 0;
// };

// StackFunction.prototype.clear = function() {
//     this._size = 0;
//     this._storage = {};
// };

// StackFunction.prototype.toArray = function() {
//     return Object.values(this._storage)
// };