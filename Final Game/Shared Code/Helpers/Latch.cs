
namespace HuntTheWumpus.SharedCode.Helpers
{
    /// <summary>
    /// A latch holds boolean state. When it recieves a new value, it sends an EdgeType event.
    /// </summary>
    class Latch
    {
        public Latch(bool InitialValue)
        {
            PreviousValue = InitialValue;
        }

        /// <summary>
        /// Constructs a latch with the value false.
        /// </summary>
        public Latch() : this(false)
        {
        }

        public bool PreviousValue {get; protected set; }

        public EdgeType PreviousEdgeResult { get; protected set; }

        /// <summary>
        /// Takes a boolean value and updates this latch's state. Returns the type of change that occured.
        /// </summary>
        /// <param name="NewValue"></param>
        /// <returns></returns>
        public EdgeType ProcessValue(bool NewValue)
        {
            bool PreviousValue = this.PreviousValue;
            this.PreviousValue = NewValue;

            if (!PreviousValue && NewValue)
                return PreviousEdgeResult = EdgeType.RisingEdge;
            if (PreviousValue && !NewValue)
                return PreviousEdgeResult = EdgeType.FallingEdge;

            return PreviousEdgeResult = EdgeType.None;
        }
    }
    /// <summary>
    /// The type of change in the latch's state that occured.
    /// </summary>
    enum EdgeType
    {
        /// <summary>
        /// This means that this latch's state is going from false to true.
        /// </summary>
        RisingEdge,
        /// <summary>
        /// This means that this latch's state is going from true to false.
        /// </summary>
        FallingEdge,
        /// <summary>
        /// There was no change in the latch's state.
        /// </summary>
        None
    }
}
