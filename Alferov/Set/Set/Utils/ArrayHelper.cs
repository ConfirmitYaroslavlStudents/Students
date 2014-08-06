using System;

namespace Set.Utils
{
    public class ArrayHelper<T>
    {
        public event Action<int> OnOperationExecute;

        public void Copy(T[] sourceArray, int sourceIndex, T[] destinationArray, int destinationIndex, int length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);

            if (OnOperationExecute != null)
            {
                OnOperationExecute(length);
            }
        }

        public void Clear(T[] array, int index, int length)
        {
            Array.Clear(array, index, length);

            if (OnOperationExecute != null)
            {
                OnOperationExecute(length);
            }
        }

        public void ChangeItem(T[] array, int index, T newValue)
        {
            array[index] = newValue;

            if (OnOperationExecute != null)
            {
                OnOperationExecute(1);
            }
        }
    }
}