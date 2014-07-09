using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListRealization
{
    class List<T>
    {
        //Реализовать:
        //1. Добавление по одному элементу.
        //2. Удаление.
        //3. Количество элементов.

        T[] _elements;
        int _size;
        T[] _initialArray = new T[5];

        public void PRINT()
        {
            Console.WriteLine("---------");
            for (int i = 0; i < _size; i++)
                Console.WriteLine("Element = '{0}'", _elements[i]);
            Console.WriteLine("---------");
        }

        public List()
        {
            _elements = _initialArray;
        }

        public List(int size)
        {
            if (size < 0)
            {
                throw new Exception();
            }
            else
            {
                _elements = new T[size];
            }
        }

        /// <summary>
        /// Фактическая размерность массива
        /// </summary>
        public int Capacity
        {
            get { return _elements.Length; }

            set
            {
                if (value < _size)
                {
                    // Exception, если пытаемся изменить размерность массива до числа, меньшего количества элементов
                    throw new Exception();
                }
                if (_elements.Length < value)
                {
                    if (value > 0)
                    {
                        T[] tempArray = new T[value];
                        Array.Copy(_elements, 0, tempArray, 0, _size);
                        _elements = tempArray;
                    }
                }
            }
        }

        /// <summary>
        /// Фактическое количество элементво в списке
        /// </summary>
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public void Add(T element)
        {
            // удваиваем размерность массива, чтобы препятствовать выходу за пределы массива, если достигнута 
            // верхняя граница
            if (_size == Capacity)
            {
                // метод set свойства Capacity расширяет массив и копирует в него значения из исходного массива
                Capacity = Capacity * 2;
            }
            _elements[_size] = element;
            _size++;
        }

        public void RemoveAt(int index)
        {
            if (index >= _size)
            {
                throw new Exception();
            }
            if (index < _size)
            {
                // смещаем вниз массив
                // .Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length)
                Array.Copy(_elements, index + 1, _elements, index, _size - index);
            }
            // default(T): значение null для ссылочных типов и "0" — для числовых типов значений
            // фактически удаляем элемент на вершине списка после смещения
            _elements[_size] = default(T);
            _size--;
        }

        public Boolean Remove(T element)
        {
            //.IndexOf<T>(array, value, startIndex, count)
            int index = Array.IndexOf<T>(_elements, element, 0, _size);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}