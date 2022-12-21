﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Data;

public static class DbSetExtensions
{
    public static void RemoveIf<T>(this DbSet<T> theDbSet, Expression<Func<T, bool>> thePredicate) where T : class
    {
        var entities = theDbSet.Where(thePredicate);
        theDbSet.RemoveRange(entities);
    }
}
