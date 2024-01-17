using System;
using System.Linq.Expressions;

namespace ModularHouse.Server.Common.Domain;

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
    
    public static string AlreadyExist<TSource>() =>
        $"{typeof(TSource).Name} already exist.";

    public static string AlreadyExistDetails<TSource, TMember>(
        Expression<Func<TSource, TMember>> memberAccess,
        TMember memberValue)
    {
        var memberName = ((MemberExpression)memberAccess.Body).Member.Name;
        var message = $"{typeof(TSource).Name} {{ {memberName}: {memberValue} }} already exist.";

        return message;
    }
    
    public static string AlreadyLinked<TSource>() =>
        $"{typeof(TSource).Name} already linked.";
    
    public static string AlreadyLinkedDetails<TSource, TMember>(
        Expression<Func<TSource, TMember>> memberAccess,
        TMember memberValue)
    {
        var memberName = ((MemberExpression)memberAccess.Body).Member.Name;
        var message = $"{typeof(TSource).Name} {{ {memberName}: {memberValue} }} already linked.";

        return message;
    }

    public const string InternalServer = "Internal server error.";
}