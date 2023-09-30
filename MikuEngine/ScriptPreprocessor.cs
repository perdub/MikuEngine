using System.Text.RegularExpressions;

namespace MikuEngine{
    internal static class ScriptPreprocess{
        internal static string ScriptProcess(this string script){
            string regexPattern = @"wait\(\);|sleep\(\d+\);";
            string replacment = "await $&";

            return Regex.Replace(script, regexPattern, replacment);
        }
    }
}