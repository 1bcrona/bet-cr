using System;
using System.Collections.Concurrent;
using System.Linq;
using BetCR.Library.Operation.Infrastructure;

namespace BetCR.Library.Operation.Helper
{
    public class OperationHelper
    {
        private static ConcurrentDictionary<string, IOperation> _operations;
        static OperationHelper()
        {
            if (_operations == null)
            {
                _operations = new ConcurrentDictionary<string, IOperation>();
                LoadDefaultOperations();
            }
        }

        public static void LoadDefaultOperations()
        {
            var @interface = typeof(IOperation);
            var operationsFound = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.DefinedTypes.Any(t => t.Namespace == "BetCR.Library.Operation.Impl"))
                .SelectMany(s => s.GetTypes())
                .Where(p => @interface.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .Select(t => (IOperation)Activator.CreateInstance(t));

            foreach (var operation in operationsFound)
            {
                _operations[operation.Name] = operation;
            }
        }


        public IOperation GetOperationByName(string operationName)
        {
            IOperation operation = null;
            _operations.TryGetValue(operationName, out operation);
            return operation;
        }



    }
}
