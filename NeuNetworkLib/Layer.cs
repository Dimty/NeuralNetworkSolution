namespace Neuron
{
    public class Layer
    {
        private Neuron[] _array;
        public int Count => _array.Length;

        public Layer(int size,int type)
        {
            _array = new Neuron[size];
            FillArray(type);
        }

        private void FillArray(int type)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = new Neuron(type);
            }
        }

        public Neuron this[int i] => _array[i];

        #region Useless

        // public Layer(ICollection<Neuron> collection)
        // {
        //     if (collection == null) throw new ArgumentNullException(nameof(collection));
        //     var obj = collection as Neuron[];
        //     if (obj != null)
        //     {
        //         _array = new Neuron[collection.Count];
        //         int index = 0;
        //         foreach (var item in collection)
        //         {
        //             _array[index++] = item;
        //         }
        //     }
        //     else
        //     {
        //         _array = new Neuron[collection.Count];
        //         collection.CopyTo(_array,0);
        //     }
        // }
        #endregion
    }
}