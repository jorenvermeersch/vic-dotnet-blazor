using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;

public class EntityNotFoundException : ApplicationException
{
    public EntityNotFoundException(string entityName, object id)
        : base($"'{entityName}' with 'Id':'{id}' was not found.") { }
}
