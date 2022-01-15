using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Exceptions : Exception
    {

        public class IDexists : Exception
        {
            public IDexists() : base() { }
            public IDexists(string message) : base(message) { }
            public IDexists(string message, Exception inner) : base(message, inner) { }
            protected IDexists(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class IDdoesntExists : Exception
        {
            public IDdoesntExists() : base() { }
            public IDdoesntExists(string message) : base(message) { }
            public IDdoesntExists(string message, Exception inner) : base(message, inner) { }
            protected IDdoesntExists(SerializationInfo info, StreamingContext context) : base(info, context) { }

        }

        public class numNotInRange : Exception
        {
            public numNotInRange() : base() { }
            public numNotInRange(string message) : base(message) { }
            public numNotInRange(string message, Exception inner) : base(message, inner) { }
            protected numNotInRange(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class noAvailableDrone : Exception   //אין רחפן פנוי בכלל או אין רחפן פנוי שמתאים למשקל
        {
            public noAvailableDrone() : base() { }
            public noAvailableDrone(string message) : base(message) { }
            public noAvailableDrone(string message, Exception inner) : base(message, inner) { }
            protected noAvailableDrone(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class AddingProblemException : Exception
        {
            public AddingProblemException() : base() { }
            public AddingProblemException(string message) : base(message) { }
            public AddingProblemException(string message, Exception inner) : base(message, inner) { }
            protected AddingProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class UpdateProblemException : Exception
        {
            public UpdateProblemException() : base() { }
            public UpdateProblemException(string message) : base(message) { }
            public UpdateProblemException(string message, Exception inner) : base(message, inner) { }
            protected UpdateProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class DeleteProblemException : Exception
        {
            public DeleteProblemException() : base() { }
            public DeleteProblemException(string message) : base(message) { }
            public DeleteProblemException(string message, Exception inner) : base(message, inner) { }
            protected DeleteProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class RangeException : Exception
        {
            public RangeException() : base() { }
            public RangeException(string message) : base(message) { }
            public RangeException(string message, Exception inner) : base(message, inner) { }
            protected RangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class noAvailableStation : Exception
        {
            public noAvailableStation() : base() { }
            public noAvailableStation(string message) : base(message) { }
            public noAvailableStation(string message, Exception inner) : base(message, inner) { }
            protected noAvailableStation(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        public class notEnoughBattary : Exception
        {
            public notEnoughBattary() : base() { }
            public notEnoughBattary(string message) : base(message) { }
            public notEnoughBattary(string message, Exception inner) : base(message, inner) { }
            protected notEnoughBattary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }


    }
}
