using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Trivia
{
    /// <summary>
    /// Stores the current type of question beihg asked.
    /// </summary>
    public enum TriviaQuestionState
    {
        None,
        TrappedInPit,
        HitWumpus
    }
}
