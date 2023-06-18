using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnitGenerator;

namespace dotnet_labo;

public interface IJson<TSelf>
{
    public static TSelf? FromJson(string s)
        => JsonSerializer.Deserialize<TSelf>(s);

    public static string ToJson(TSelf self)
        => JsonSerializer.Serialize<TSelf>(self);
}

internal static class UnitOfOptions
{
    public const UnitGenerateOptions Default = UnitGenerateOptions.JsonConverter | UnitGenerateOptions.ParseMethod | UnitGenerateOptions.Validate | UnitGenerateOptions.EntityFrameworkValueConverter;
}

[UnitOf(typeof(string), UnitOfOptions.Default)]
public readonly partial struct Name
{
    private partial void Validate()
    {
        if (value == "") throw new ArgumentException("");
    }

}

[UnitOf(typeof(int), UnitOfOptions.Default)]
public readonly partial struct Age
{
    private partial void Validate()
    {
        if (value < 0) throw new ArgumentException("");
    }
}

public record Person(Name Name, Age Age) : IJson<Person>;

public record Item(Name Name) : IJson<Item>;
public static class IJsonExtension
{
    public static T? To<T>(this string s) where T : IJson<T>
    {
        return IJson<T>.FromJson(s);
    }

    public static string ToJson<T>(this T self) where T : IJson<T>
    {
        return IJson<T>.ToJson(self);
    }

}

public record PersonDB(int Id, string Name, int Age) : IJson<PersonDB>;
