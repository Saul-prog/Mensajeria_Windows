using System.Globalization;

namespace Mensajeria_Windows.EntityFramework.Helpers
{
    public class RepositoryExceptions : Exception
    {
        public RepositoryExceptions() : base() { }

        public RepositoryExceptions(string message) : base(message) { }

        public RepositoryExceptions(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
