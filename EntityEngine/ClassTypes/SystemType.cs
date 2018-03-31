using System;

namespace EntityEngine.ClassTypes
{
    public class SystemType
    {
        private static int _id = 0;
        private static int _bit = 1;

        public int Id
        {
            get;
            private set;
        }

        public int Bit
        {
            get;
            private set;
        }

        public SystemType()
        {
            if (_id == 32) throw new Exception("Max (32) system types reached!");
            Id = _id;
            Bit = _bit;

            _id++;
            _bit = 1 << _id;
        }
    }
}
