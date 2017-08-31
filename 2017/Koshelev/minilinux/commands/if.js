function condition(operationsTrue, operationsFalse, condition, self){

	const result = condition.result;
	if (result){
		self.next = operationsTrue;
	} else {
		self.next = operationsFalse;
	}
}

module.exports = condition;