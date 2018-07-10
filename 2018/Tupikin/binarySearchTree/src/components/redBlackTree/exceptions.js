class Exceptions{

    static IllegalArgumentException(message){
        this.name = 'IllegalArgumentException';
        this.message = message;
    }

}

module.exports = {Exceptions};