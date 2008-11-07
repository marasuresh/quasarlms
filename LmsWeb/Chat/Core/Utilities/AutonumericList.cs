namespace System.Collections.Generic
{
    /// <summary>
    /// Summary description for AutonumericList
    /// </summary>
    public class AutonumericList<TValue> : Dictionary<int, TValue>
    {
        #region Propiedades

        // El orden de la Lista
        public int autonumeric = 1;

        private int _step = 1;

        /// <summary>
        /// Define el incremento del autonumérico.
        /// Como mínimo valdrá 1
        /// </summary> 
        private int step
        {
            get { return _step; }
            set { _step = value != 0 ? value : 1; }
        }

        #endregion

        #region Inicialización

        public AutonumericList()
            : this(1, 1)
        {
        }

        public AutonumericList(int seed, int step)
        {
            autonumeric = seed;
            this.step = step;
        }

        #endregion

        #region 'Nuevas propiedades'

        public void Add(TValue value)
        {
            base.Add(autonumeric, value);
            autonumeric += step;
        }

        /// <summary>
        /// The first argument is ignored.
        /// El primer argumento es ignorado.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add(int key, TValue value)
        {
            Add(value);
        }

        #endregion
    }
}