using MudBlazor;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace AcademiaNet.Frontend.Pages.Exam_management;

public partial class ExamManagementIndex
{
    private bool success;
    private string[] errors = { };
    private MudTextField<string>? pwField1;
    private MudForm? form;

    private IEnumerable<string> PasswordStrength(string pw)
    {
        if (string.IsNullOrWhiteSpace(pw))
        {
            yield return "Password is required!";
            yield break;
        }
        if (pw.Length < 8)
            yield return "Password must be at least of length 8";
        if (!Regex.IsMatch(pw, @"[A-Z]"))
            yield return "Password must contain at least one capital letter";
        if (!Regex.IsMatch(pw, @"[a-z]"))
            yield return "Password must contain at least one lowercase letter";
        if (!Regex.IsMatch(pw, @"[0-9]"))
            yield return "Password must contain at least one digit";
    }

    private string PasswordMatch(string arg)
    {
        if (pwField1.Value != arg)
            return "Passwords don't match";
        return null;
    }

    //private string? _value;
    //private Margin _margin;
    //private bool _dense;
    //private bool _disabled;
    //private bool _readonly;
    //private bool _placeholder;
    //private bool _helperText;
    //private bool _helperTextOnFocus;
    //private bool _clearable;

    //private string[] _states =
    //{
    //    "Alabama", "Alaska", "Arizona", "Arkansas", "California",
    //    "Colorado", "Connecticut", "Delaware", "Florida", "Georgia",
    //    "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas",
    //    "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts",
    //    "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana",
    //    "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
    //    "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma",
    //    "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota",
    //    "Tennessee", "Texas", "Utah", "Vermont", "Virginia",
    //    "Washington", "West Virginia", "Wisconsin", "Wyoming"
    //};

    //private async Task<IEnumerable<string>> Search(string value, CancellationToken token)
    //{
    //    // In real life use an asynchronous function for fetching data from an api.
    //    await Task.Delay(5, token);

    //    // if text is null or empty, show complete list
    //    if (string.IsNullOrEmpty(value))
    //        return _states;

    //    return _states.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    //}
}