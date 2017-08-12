function core(commands, args){
	const log = console.log;
	const helloWorld = function(){
		const { spawnSync } = require('child_process');
		let hello = spawnSync('./helloWorld.sh');
		console.log(hello.stdout.toString());		
	}
	const createFolder = function(path){
		const {mkdirSync} = require('fs');
		mkdirSync(path);
	}
	let error = false;
	let results = [];
	let program = match(commands, args);
	commands = program.commands;
	args = program.args;
	let branches = program.branches;
	
	branches.__proto__.contains = function(command, k){
		for (let i = 0; i < this.length; i++){
			if (this[i].error == command && k < this[i].index)
				return this[i];
		}
		return false;
	}

	for(let i = 0; i < commands.length; i++){
		try{
			let command = commands[i];
			if (command[0] == "!"){
				call(command, args[i]);
			} else if (!error){
				call(command, args[i]);
			} else {
				results.push(false);
			}
		}catch(e){
			console.log("Error in", commands[i], "with args:", args[i]);
			error = true;
			results.push(false);
			let chain = branches.contains(commands[i], i);

			if (chain){
				error = false;
				commands = commands.concat(chain.commands);
				args = args.concat(chain.args);
			}
		}
	}

	return results;

	function call(command, args){
		if (command[0] == '!')
			command = command.slice(1);
		switch(command){
			case 'log': log(args);
				break;
			case 'hello': helloWorld(args);
				break;
			case 'createFolder': createFolder(args);
				break;
			default: throw new Error("Such function doesn't exist");
		}
		results.push(true);
	}

	function match(commands, args){
		let _if = [];
		let main = [];
		let mainArgs = [];
		let index;
		
		for (let i = 0; i < commands.length; i++)
		{
			if (!commands[i].indexOf('!if')){
				if (!isFinite(index)){
					index = 0;
				} else {
					index++;
				}
				_if[index] = {error: commands[i].slice(3), index: i-index, commands: [], args:[]};
			} else if(!isFinite(index)) {
				main.push(commands[i]);
				mainArgs.push(args[i]);
			} else {
				_if[index].commands.push(commands[i]);
				_if[index].args.push(args[i]);
			}
		}

		return {
			commands: main,
			args: mainArgs,
			branches: _if
		};
	}
}

module.exports = {
	core
};