using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HashSet
{
    /*
     * HashSet
     * Dmitriy Karachev
     * 09.07.16
     * 
     * Основан на хэш таблице. 
     * Разрешение коллизий методом двойного хэширования.
     * H(key,attempts)=mainH(key)+attempts*offH(key)
     * 
     * mainH(key)= (key.GetHashCode() & 0x7FFFFFFF) % length_of_array
     * offH(key)=1+mainH(key) % (length_of_array-1)
     * 
     * Циклы вида:
     * While(True)
     * {
     *   Some code
     * }
     * 
     * в методах добавления\поиска не бесконечны. Гарантируется выход из них, так как своевременно производится
     * переназначение размера массива, и длина массива всегда равна простому числу.
     */

    #region Proxy

    internal class HashSetProxy<T>
    {
        private HashSet<T> _a;

        private HashSetProxy(HashSet<T> a)
        {
            _a = a;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public T[] Items
        {
            get
            {
                var items = (from item in _a select item).ToArray();
                return items;
            }
        }
    }

    #endregion

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(HashSetProxy<>))]
    public class HashSet<T> : ISet<T>
    {
        #region Consts and static fields

        private const double LoadFactor = 0.8;//[0.1,1] 
        private const int DefaultCapacity = 4;

        #endregion

        #region Fields

        private Entry[] _entries;
        private int _count;
        private int _countLimit;//Для Resize()
        private int _version;
        private IEqualityComparer<T> _eqComparer;

        #endregion

        #region Properties

        public int Count => _count;
        public IEqualityComparer<T> EqualityComparer => _eqComparer;
        public bool IsReadOnly => true;

        #endregion

        #region Constructors

        public HashSet() : this(DefaultCapacity, null) { }

        public HashSet(int capacity) : this(capacity, null) { }

        public HashSet(IEqualityComparer<T> equalityComparer) : this(DefaultCapacity, equalityComparer) { }

        public HashSet(int capacity, IEqualityComparer<T> equalityComparer)
        {
            Init(capacity, equalityComparer);
        }

        public HashSet(IEnumerable<T> collection) : this(collection, null) { }

        public HashSet(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer)
            : this(DefaultCapacity, equalityComparer)
        {
            UnionWith(collection);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Добавляет элемент в множество. 
        /// Если элемент уже есть в множестве, то он не добавляется.
        /// Возвращает true, если элемент был добавлен.
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        /// <returns>True, если элемент добавлен, иначе False.</returns>
        public bool Add(T item)
        {
            if (_count == _countLimit) Resize();

            int hash;
            var mainhash = GetMainHash(item, _entries.Length, out hash);
            var index = mainhash;
            if (AddInMainArrayIfEntryIsEmpty(item, hash, index)) return true;

            var offhash = GetOffHash(mainhash, _entries.Length);

            while (true)
            {
                if (_entries[index].HashCode == hash && _eqComparer.Equals(_entries[index].Value, item))
                    return false;

                index += offhash;
                index %= _entries.Length;

                if (AddInMainArrayIfEntryIsEmpty(item, hash, index)) return true;
            }
        }
         
        /// <summary>
        /// Удаляет все элементы из множества
        /// </summary>
        public void Clear()
        {
            Init(0, _eqComparer);
        }

        /// <summary>
        /// Проверяет, содержится ли указанный элемент в множестве.
        /// Возвращает True, если элемент содержится в множестве, иначе False.
        /// </summary>
        /// <param name="item">Элемент, подвергающийся проверке.</param>
        /// <returns>True, если элемент содержится в множестве, иначе False.</returns>
        public bool Contains(T item)
        {    
            return FindEntry(item) >= 0;
        }

        /// <summary>
        /// Копирует все элементы множества в указанный массив, начиная с нулевого индекса.
        /// </summary>
        /// <param name="array">Массив, в который будут скопированы элементы множества.</param>
        public void CopyTo(T[] array)
        {
            CopyTo(array, 0);
        }

        /// <summary>
        /// Копирует все элементы множества в указанный массив, начиная с указанного индекса.
        /// </summary>
        /// <param name="array">Массив, в который будут скопированы элементы множества.</param>
        /// <param name="arrayIndex">Индекс массива, с которого начнется копирование в массив.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("Индекс массива выходит за пределы длины массива");
            if (array.Length < _count + arrayIndex)
                throw new ArgumentException("Массив не имеет достаточно места");

            for (int i = 0; i < _entries.Length; i++)
            {
                if (IsEntryOfMainArrayEmpty(i)) continue;

                array[arrayIndex++] = _entries[i].Value;
            }
        }

        /// <summary>
        /// Удаляет указанный элемент из множества, если он содержится в нем.
        /// Возвращает True, если элемент был успешно удален, иначе False.
        /// </summary>
        /// <param name="item">Элемент, который требуется удалить</param>
        /// <returns>True, если элемент был успешно удален, иначе False.</returns>
        public bool Remove(T item)
        {
            var index = FindEntry(item);
            if (index < 0) return false;

            ClearEntryOfMainArray(index);

            return true;
        }

        /// <summary>
        /// Удаляет все элементы из множества, которые удовлетворяют указанному предикату.
        /// Возвращает количество удаленных элементов.
        /// </summary>
        /// <param name="match">Предикат, по которому будут определятся удаляемые элементы</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int RemoveWhere(Predicate<T> match)
        {
            int removeNum = 0;

            for (int i = 0; i < _entries.Length; i++)
            {
                if (IsEntryOfMainArrayEmpty(i)) continue;

                if (match(_entries[i].Value))
                {
                    ClearEntryOfMainArray(i);
                    removeNum++;
                }
            }

            return removeNum;
        }

        /// <summary>
        /// Задает емкость множества равной фактическому количеству находящихся в множестве элементов.
        /// </summary>
        public void TrimExcess()
        {
            if (_count == 0) return;

            var newsize = HashHelper.GetPrime(
                Convert.ToInt32((_count + 1)/LoadFactor)
                );

            _countLimit = Convert.ToInt32(LoadFactor*newsize);
            Rehash(newsize);
            _version++;
        }

        /// <summary>
        /// Добавляет элемент в множество. 
        /// Если элемент уже есть в множестве, то он не добавляется.
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        #endregion

        #region ISet methods

        /// <summary>
        /// Изменяет множество так, чтобы оно содержало все элементы, которые содержатся в нем
        /// или в указанной коллекции либо как в нем, так и в указанной коллекции.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            foreach (T item in other)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Изменяет множество так, чтобы в нем содержались только элементы, которые содержатся как в множестве,
        /// так и в указанной коллекции.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            if (_count == 0) return;
            var intersectEnriesIndexes = new HashSet<int>();
            foreach (T item in other)
            {
                var index = FindEntry(item);
                if (index >= 0)
                    intersectEnriesIndexes.Add(index);
            }

            for (int i = 0; i < _entries.Length; i++)
            {
                if (IsEntryOfMainArrayEmpty(i)) continue;

                if (intersectEnriesIndexes.Remove(i)) continue;

                ClearEntryOfMainArray(i);
            }
        }

        /// <summary>
        /// Удаляет из множества все элементы, которые содержатся в указанной коллекции.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            foreach (T item in other)
            {
                Remove(item);
            }
        }

        /// <summary>
        /// Изменяет множество так, чтобы в нем содержались элементы из этого множества и из указанной коллекции,
        /// но не одновременно в обоих.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            foreach (T item in other)
            {
                if (!Remove(item))
                    Add(item);
            }
        }

        /// <summary>
        /// Определяет, содержатся ли все элементы множества в указанной коллекции.
        /// Возвращает True, если содержатся, иначе False.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        /// <returns>True, если содержатся, иначе False.</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            var indexesOfOtherInSet = new HashSet<int>();
            foreach (T item in other)
            {
                var index = FindEntry(item);
                if (index >= 0) indexesOfOtherInSet.Add(index);
            }

            return indexesOfOtherInSet.Count == _count;
        }

        /// <summary>
        /// Определяет, содержатся ли все элементы из указанной коллекции в множестве.
        /// Возвращает True, если содержатся, иначе False.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        /// <returns>True, если содержатся, иначе False.</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            foreach (T item in other)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }

        /// <summary>
        /// Определяет, содержатся ли все элементы из указанной коллекции в множестве
        ///  и множество с указанной коллекцией не совпадают.
        /// Возвращает True, если содержатся и коллекции не совпадают, иначе False.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        /// <returns>True, если содержатся и множество с указанной коллекцией не совпадают, иначе False.</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            int count = 0;
            foreach (T item in other)
            {
                if (!Contains(item)) return false;
                count++;
            }
            return count != _count;
        }

        /// <summary>
        /// Определяет, содержатся ли все элементы из множества в указанной коллекции 
        /// и множество с указанной коллекцией не совпадают.
        /// Возвращает True, если содержатся и множество с указанной коллекцией не совпадают, иначе False.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        /// <returns>True, если содержатся и множество с указанной коллекцией не совпадают, иначе False.</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            var indexesOfOtherInSet = new HashSet<int>();
            foreach (T item in other)
            {
                var index = FindEntry(item);
                indexesOfOtherInSet.Add(index);
            }

            return indexesOfOtherInSet.Count - 1 == _count;
        }

        /// <summary>
        /// Определяет, содержится ли хоть один элемент из указанной коллекции в множестве.
        /// Возвращает True, если содержится, иначе False.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        /// <returns>True, если содержится, иначе False.</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            foreach (T item in other)
            {
                if (Contains(item)) return true;
            }
            return false;
        }

        /// <summary>
        /// Определяет, совпадает ли множество с указанной коллекцией.
        /// Возращает True, если совпадает, иначе False.
        /// </summary>
        /// <param name="other">Коллекция для сравнения с множеством.</param>
        /// <returns>True, если совпадает, иначе False.</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("Аргумент не должен принимать значение null");

            int count = 0;
            foreach (T item in other)
            {
                if (!Contains(item)) return false;
                count++;
            }
            return count == _count;
        }

        #endregion

        #region Enumerable methods

        public IEnumerator<T> GetEnumerator()
        {
            var version = _version;
            for (int i = 0; i < _entries.Length; i++)
            {
                if (version != _version) throw new InvalidOperationException("Коллекция была изменена");
                if (IsEntryOfMainArrayEmpty(i)) continue;
                yield return _entries[i].Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Internal and private methods

        /// <summary>
        /// Инициализирует множество.
        /// </summary>
        private void Init(int capacity, IEqualityComparer<T> comparer)
        {
            capacity = (capacity < DefaultCapacity) ? DefaultCapacity : capacity;

            var realsize = Convert.ToInt32(capacity / LoadFactor);
            realsize = HashHelper.GetPrime(realsize);
            _entries = new Entry[realsize];
            _count = 0;
            _countLimit = Convert.ToInt32(realsize * LoadFactor);
            _version = 0;
            _eqComparer = comparer ?? EqualityComparer<T>.Default;

            for (int i = 0; i < _entries.Length; i++)
            {
                InitDefaultEntry(_entries, i);
            }
        }

        /// <summary>
        /// Возвращает значение первой хэш-функции.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private int GetMainHash(T value, int length, out int hash)
        {
            hash = _eqComparer.GetHashCode(value) & 0x7FFFFFFF;
            var mainhash = hash % length;
            return mainhash;
        }

        /// <summary>
        /// Возвращает значение второй хэш-функции.
        /// </summary>
        /// <param name="mainhash"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int GetOffHash(int mainhash, int length)
        {
            return 1 + mainhash % (length - 1);
        }

        /// <summary>
        /// Производит изменение размера главного массива.
        /// </summary>
        private void Resize()
        {
            var newsize = HashHelper.GetPrime(2 * _entries.Length);

            Rehash(newsize);

            _version++;
            _countLimit = Convert.ToInt32(LoadFactor * newsize);
        }

        /// <summary>
        /// Пересчитывает хэши всех элементов главного массива и перераспределяет эти элементы
        /// в другой массив.
        /// </summary>
        /// <param name="newsize"></param>
        private void Rehash(int newsize)
        {
            var newentries = new Entry[newsize];
            for (int i = 0; i < newentries.Length; i++)
            {
                InitDefaultEntry(newentries, i);
            }

            for (int i = 0; i < _entries.Length; i++)
            {
                if (IsEntryOfMainArrayEmpty(i)) continue;

                PutEntry(newentries, _entries[i].Value);
            }
            _entries = newentries;
        }

        /// <summary>
        /// Вставляет указанный элемент в массив без проверки на повторы.
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="item"></param>
        private void PutEntry(Entry[] entries, T item)
        {
            int hash;
            var mainhash = GetMainHash(item, entries.Length, out hash);
            var index = mainhash;
            if (AddIfEntryIsEmpty(entries, item, hash, index)) return;

            var offhash = GetOffHash(mainhash, entries.Length);

            while (true)
            {
                if (AddIfEntryIsEmpty(entries, item, hash, index)) return;
                index += offhash;
                index %= entries.Length;
            }
        }

        /// <summary>
        /// Возвращает индекс главного массива, в котором находится указанный элемент.
        /// Если указанного элемента нет в главном массиве, то возращается -1.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int FindEntry(T item)
        {
            int hash;
            var mainhash = GetMainHash(item, _entries.Length, out hash);
            var index = mainhash;
            if (IsEntryOfMainArrayEmpty(index)) return -1;

            var offhash = GetOffHash(mainhash, _entries.Length);

            while (true)
            {
                if (_entries[index].HashCode == hash && _eqComparer.Equals(_entries[index].Value, item))
                {
                    return index;
                }

                index += offhash;
                index %= _entries.Length;

                if (IsEntryOfMainArrayEmpty(index)) return -1;
            }
        }

        /// <summary>
        /// Очищает запись под указанным индексом в указанном массиве.
        /// </summary>
        private void ClearEntry(Entry[] entries, int index)
        {
            InitDefaultEntry(entries, index);
        }

        /// <summary>
        /// Очищает запись под указанным индексом в главном массиве.
        /// </summary>
        private void ClearEntryOfMainArray(int index)
        {
            ClearEntry(_entries, index);
            DecreaseCount();
        }

        /// <summary>
        /// Инициализирует значение ячейки под указанным индексом в указанном массиве по умолчанию.
        /// </summary>
        private void InitDefaultEntry(Entry[] entries, int index)
        {
            entries[index].Value = default(T);
            entries[index].HashCode = -1;
        }

        /// <summary>
        /// Определяет, является ли запись в ячейке указанного массива под указанным идексом пустой.
        /// </summary>
        private bool IsEntryEmpty(Entry[] entries, int index)
        {
            return entries[index].HashCode == -1;
        }

        /// <summary>
        /// Определяет, является ли запись в ячейке главного массива под указанным идексом пустой.
        /// </summary>
        private bool IsEntryOfMainArrayEmpty(int index)
        {
            return IsEntryEmpty(_entries, index);
        }

        /// <summary>
        /// Добавляет элемент в указанный массив, если нет записи в указанной ячейке массива.
        /// </summary>
        private bool AddIfEntryIsEmpty(Entry[] entries, T item, int hash, int index)
        {
            if (!IsEntryEmpty(entries, index)) return false;

            entries[index].HashCode = hash;
            entries[index].Value = item;
            return true;
        }

        /// <summary>
        /// Добавляет элемент в главный массив, если нет записи в указанной ячейке массива.
        /// </summary>
        private bool AddInMainArrayIfEntryIsEmpty(T item, int hash, int index)
        {
            var isAdd = AddIfEntryIsEmpty(_entries, item, hash, index);
            if (isAdd) RaiseCount();
            return isAdd;
        }

        /// <summary>
        /// Увеличивает размер счетчика.
        /// </summary>
        private void RaiseCount()
        {
            _count++;
            _version++;
        }

        /// <summary>
        /// Уменьшает размер счетчика.
        /// </summary>
        private void DecreaseCount()
        {
            _count--;
            _version++;
        }
        #endregion

        #region Nested types
        internal struct Entry
        {
            public T Value;
            public int HashCode;//-1 if not used
        }
        #endregion
    }
}
