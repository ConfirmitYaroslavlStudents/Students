const Stack = require('./Stack');

test('Проверка длины стека. stack.size() = 0', () => {
    const stack = new Stack();
    expect(stack.size()).toBe(0);
});

test('Проверка функции push(). stack.size() = 1', () => {
    const stack = new Stack();
    stack.push([4, 8, 15, 16, 23, 42])
    expect(stack.size()).toBe(1);
});

test('Проверка функциЙ push() и pop(). stack.size() = 2', () => {
    const stack = new Stack();
    stack.push(4)
    stack.push(8)
    stack.push(15)
    stack.pop()
    expect(stack.size()).toBe(2);
});

test('Проверка функции clear(). stack.size() = 0', () => {
    const stack = new Stack();
    stack.push(4)
    stack.push(8)
    stack.push(15)
    stack.push(16)
    stack.push(23)
    stack.push(42)
    stack.clear()
    expect(stack.size()).toBe(0);
});

test('Проверка функции isEmpty(). isEmpty() = true', () => {
    const stack = new Stack();
    expect(stack.isEmpty()).toBe(true);
});

test('Проверка функции isEmpty(). isEmpty() = false', () => {
    const stack = new Stack();
    stack.push([4, 8, 15, 16, 23, 42])
    expect(stack.isEmpty()).toBe(false);
});

test('Проверка функции peek(). Верхний элемент = 8', () => {
    const stack = new Stack();
    stack.push(4)
    stack.push(8)
    stack.push(15)
    stack.pop(15)
    expect(stack.peek()).toBe(8);
});