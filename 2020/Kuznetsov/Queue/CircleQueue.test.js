const CircleQueue = require('./CircleQueue');
const circleQueue = new CircleQueue().init()

describe('CircleQueue Tests', () => {
    test('Initial_SizeEqualZero', () => {
        expect(circleQueue.size()).toBe(0);
    });

    test('Initial_HeadEqualZero', () => {
        expect(circleQueue.head()).toBe(0);
    });

    test('Initial_TailEqualZero', () => {
        expect(circleQueue.tail()).toBe(0);
    });

    test('Initial_ShowStorage', () => {
        expect(circleQueue.show()).toEqual([ , , , ,]);
    });

    test('Enqueue_SizeEqualOne', () => {
        circleQueue.enqueue(0)
        expect(circleQueue.size()).toBe(1);
    });

    test('Enqueue_HeadEqualOne', () => {
        expect(circleQueue.head()).toBe(1);
    });

    test('Enqueue_TailEqualZero', () => {
        expect(circleQueue.tail()).toBe(0);
    });

    test('Enqueue_ShowStorage', () => {
        expect(circleQueue.show()).toEqual([0, , , ,]);
    });

    test('Dequeue_ReturnZero', () => {
        expect(circleQueue.dequeue()).toBe(0);
    });

    test('Dequeue_HeadEqualOne', () => {
        expect(circleQueue.head()).toBe(1);
    });

    test('Dequeue_TailEqualOne', () => {
        expect(circleQueue.tail()).toBe(1);
    });

    test('Dequeue_ShowStorage', () => {
        expect(circleQueue.show()).toEqual([ , , , ,]);
    });

    test('EnqueueAfterDequeue_SizeEqualOne', () => {
        circleQueue.enqueue(1)
        expect(circleQueue.size()).toBe(1);
    });

    test('EnqueueAfterDequeue_HeadEqualTwo', () => {
        expect(circleQueue.head()).toBe(2);
    });

    test('EnqueueAfterDequeue_TailEqualOne', () => {
        expect(circleQueue.tail()).toBe(1);
    });

    test('EnqueueAfterDequeue_ShowStorage', () => {
        expect(circleQueue.show()).toEqual([ , 1, , ,]);
    });

    test('EnqueueToMaxHeadNumber_SizeEqualThree', () => {
        circleQueue.enqueue(2)
        circleQueue.enqueue(3)
        expect(circleQueue.size()).toBe(3);
    });

    test('EnqueueToMaxHeadNumber_ShowStorage', () => {
        expect(circleQueue.show()).toEqual([ , 1, 2, 3, ]);
    });

    test('EnqueueToMaxHeadNumber_HeadEqualZero', () => {
        expect(circleQueue.head()).toBe(0);
    });

    test('EnqueueToMaxCapacityNumber_ShowStorage', () => {
        circleQueue.enqueue(4)
        expect(circleQueue.show()).toEqual([4, 1, 2, 3, ]);
    });

    test('EnqueueToRisizeStorageCapacity_ShowStorage_CapacityEqualEight', () => {
        circleQueue.enqueue(5)
        circleQueue.enqueue(6)
        expect(circleQueue.show()).toEqual([1, 2, 3, 4, 5, 6, , ,]);
        expect(circleQueue.capacity()).toEqual(8);
    });
    
    test('DequeueAfterRisizeStorageCapacityEqualEight', () => {
        expect(circleQueue.dequeue()).toBe(1);
        expect(circleQueue.dequeue()).toBe(2);
        expect(circleQueue.dequeue()).toBe(3);
        expect(circleQueue.dequeue()).toBe(4);
        expect(circleQueue.dequeue()).toBe(5);
        expect(circleQueue.show()).toEqual([, , , , , 6, , ,]);
    });
    
    test('EnqueueToRisizeStorage_ShowStorage_CapacityEqualSixTeen', () => {
        circleQueue.enqueue('a')
        circleQueue.enqueue('b')
        circleQueue.enqueue('c')
        circleQueue.enqueue('d')
        circleQueue.enqueue('e')
        circleQueue.enqueue('f')
        circleQueue.enqueue('g')
        expect(circleQueue.show()).toEqual(['c', 'd', 'e', 'f', 'g', 6, 'a', 'b',]);
        expect(circleQueue.capacity()).toEqual(8);
        circleQueue.enqueue('h')
        expect(circleQueue.show()).toEqual([6, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', , , , , , , ,]);
        expect(circleQueue.capacity()).toEqual(16);
    });
    
    test('EnqueueToRisizeStorageCapacityEqualSixTeen_HeadEqualNine', () => {
        expect(circleQueue.head()).toBe(9);
    })

    test('EnqueueToRisizeStorageCapacityEqualSixTeen_TailEqualZero', () => {
        expect(circleQueue.tail()).toBe(0);
    })

    test('DequeueAfterRisizeStorageCapacityEqualSixTeen', () => {
        expect(circleQueue.dequeue()).toBe(6);
        expect(circleQueue.dequeue()).toBe('a');
        expect(circleQueue.dequeue()).toBe('b');
        expect(circleQueue.dequeue()).toBe('c');
        expect(circleQueue.dequeue()).toBe('d');
        expect(circleQueue.dequeue()).toBe('e');
        expect(circleQueue.dequeue()).toBe('f');
        expect(circleQueue.dequeue()).toBe('g');
        expect(circleQueue.show()).toEqual([, , , , , , , , 'h', , , , , , , ,]);
        expect(circleQueue.size()).toBe(1);
        expect(circleQueue.dequeue()).toBe('h');
        expect(circleQueue.show()).toEqual([, , , , , , , , , , , , , , , ,]);
        expect(circleQueue.size()).toBe(0);
    });

    test('Dequeue_QueueIsEmpty', () => { 
        function dequeueEmptyQueue() {
            circleQueue.dequeue()
        }
        expect(dequeueEmptyQueue).toThrowError(new Error('Queue is empty'));
        expect(circleQueue.show()).toEqual([, , , , , , , , , , , , , , , ,]);
        expect(circleQueue.size()).toBe(0);
    });
});