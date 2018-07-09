#!/usr/bin/env node
'use strict';

const cmd = require('node-cmd');
const fs = require('fs');

async function test(){
    console.log('Run the tests...\n');

    if (!fs.existsSync('package-lock.json'))
        await cmd.run('npm install');

    await cmd.get('npm test', (err, data) => {
        console.log(data);
    });
}

console.log(`
|------------------------------------------------------|
| Created by Vladislav Tupikin, 2018                   |
| Licence: MIT                                         |
|                                                      |
| Algorithms code:                                     |
|                                                      |
|              'src/BinarySearchTree.js'               |
|                    'src/Node.js'                     |
|                                                      |
| Tests code by qunit module:                          |
|                                                      |
|           'test/BinarySearchTree.test.js'            |
|                                                      |
| Github:                                              |
|                                                      |
| https://github.com/MrRefactoring/red-black-tree-node |
|                                                      |
|******************************************************|
`);

test();