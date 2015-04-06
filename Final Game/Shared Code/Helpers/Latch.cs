using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Helpers
{
    class Latch
    {
        public bool PreviousValue {get; protected set; }

        public Latch(bool InitialValue)
        {
            PreviousValue = InitialValue;
        }

        public Latch()
        {
            PreviousValue = false;
        }

        public EdgeType ProcessValue(bool NewValue)
        {
            bool PreviousValue = this.PreviousValue;
            this.PreviousValue = NewValue;

            if (!PreviousValue && NewValue)
                return EdgeType.RisingEdge;
            else if (PreviousValue && !NewValue)
                return EdgeType.FallingEdge;

            return EdgeType.None;
        }
    }

    enum EdgeType
    {
        RisingEdge,
        FallingEdge,
        None
    }
}
