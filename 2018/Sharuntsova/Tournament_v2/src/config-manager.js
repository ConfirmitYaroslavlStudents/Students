const fs = require('fs');

class ConfigurationManager {
    constructor() {
        this.fileName = "configuration.json";
    }

    exists() {
        return fs.existsSync(this.fileName);
    }

    load() {
        return JSON.parse(fs.readFileSync(this.fileName));
    }

    save(config) {
        fs.writeFileSync(this.fileName, JSON.stringify(config))
    }
}

module.exports = {
    ConfigurationManager
}