using System;
using System.Linq.Expressions;

namespace Common.Domain;

public static class ErrorMessages
{
    public static string NotFound<TSource>() =>
        $"{typeof(TSource).Name} not found.";
    
    public static string NotFoundDetails<TSource, TMember>(
        Expression<Func<TSource, TMember>> memberAccess,
        TMember memberValue)
    {
        var memberName = ((MemberExpression)memberAccess.Body).Member.Name;
        var message = $"{typeof(TSource).Name} {{ {memberName}: {memberValue} }} does not exist.";

        return message;
    }
    
    public const string InternalServer = "Internal server error.";
}