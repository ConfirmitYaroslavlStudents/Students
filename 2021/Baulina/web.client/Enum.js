function Enum(obj) {
    const newObj = {};

    for (const prop in obj) {
        if (obj.hasOwnProperty(prop)) {
            newObj[prop] = Symbol(obj[prop]);
        }
    }
    return Object.freeze(newObj);
}
