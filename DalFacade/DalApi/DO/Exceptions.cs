using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{

    [Serializable]

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

    public class DeleteProblemException : Exception
    {
        public DeleteProblemException() : base() { }
        public DeleteProblemException(string message) : base(message) { }
        public DeleteProblemException(string message, Exception inner) : base(message, inner) { }
        protected DeleteProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
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

    public class LoadingException : Exception
    {
        string filePath;
        public LoadingException() : base() { }
        public LoadingException(string message) : base(message) { }
        public LoadingException(string message, Exception inner) : base(message, inner) { }

        public LoadingException(string path, string messege, Exception inner) => filePath = path;
        protected LoadingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }


}
