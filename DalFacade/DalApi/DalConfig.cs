namespace DalApi;
using System.Xml.Linq;

static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, string> s_dalPackages;
    static string? s_dir = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.FullName + @"\xml\";
    static DalConfig()
    {

        XElement dalConfig = XElement.Load(s_dir + @"dal_config.xml")
            ?? throw new DalConfigException("dal_config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
