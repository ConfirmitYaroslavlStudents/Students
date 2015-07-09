using System;

namespace Set.Utils
{
    public class ArrayHelper<T>
    {
        public event Action<int> OnOperationExecute;

        public void Copy(T[] sourceArray, int sourceIndex, T[] destinationArray, int destinationIndex, int length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
            InvokeOnOperationExecute(length);
        }

        public void Clear(T[] array, int index, int length)
        {
            Array.Clear(array, index, length);
            InvokeOnOperationExecute(length);
        }

        public void ChangeItem(T[] array, int index, T newValue)
        {
            array[index] = newValue;
            InvokeOnOperationExecute(1);
        }

        private void InvokeOnOperationExecute(int length)
        {
            if (OnOperationExecute != null)
            {
                OnOperationExecute(length);
            }
        }
    }
}