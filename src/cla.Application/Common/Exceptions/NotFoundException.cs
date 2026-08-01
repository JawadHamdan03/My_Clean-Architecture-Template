using System;
using System.Collections.Generic;
using System.Text;

namespace cla.Application.Common.Exceptions;

public class NotFoundException(string entity, Guid id) : Exception($"the {entity} with id: '{id}' not found")
{
}
