using System.Text.RegularExpressions;

namespace CleanBase.Generator.Domain.Rules;

public static partial class NamingRules
{
    [GeneratedRegex("^[A-Z][A-Za-z0-9.]{2,99}$")]
    private static partial Regex SolutionNameRegex();

    [GeneratedRegex("^[A-Z][A-Za-z0-9]{2,50}$")]
    private static partial Regex ProjectNameRegex();

    public static bool IsValidSolutionName(string value) => !string.IsNullOrWhiteSpace(value) && SolutionNameRegex().IsMatch(value);

    public static bool IsValidProjectName(string value) => !string.IsNullOrWhiteSpace(value) && ProjectNameRegex().IsMatch(value);
}
