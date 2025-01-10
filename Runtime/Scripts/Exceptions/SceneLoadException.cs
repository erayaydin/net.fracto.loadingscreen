using System;

namespace Fracto.LoadingScreen.Exceptions
{
    [Serializable]
    public class SceneLoadException : Exception
    {
        public SceneLoadException() { }
        public SceneLoadException(string message) : base(message) { }
        public SceneLoadException(string message, Exception inner) : base(message, inner) { }
    }
}