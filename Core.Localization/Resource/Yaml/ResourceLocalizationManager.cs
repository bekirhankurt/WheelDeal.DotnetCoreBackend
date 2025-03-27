using System.Data;
using Core.Localization.Abstraction;
using YamlDotNet.RepresentationModel;

namespace Core.Localization.Resource.Yaml;

public class ResourceLocalizationManager : ILocalizationService
{
    private const string _defaultLocale = "en";
    private const string _defaultKeySection = "index";

    private readonly Dictionary<string, Dictionary<string, (string path, YamlMappingNode? content)>> _resourceData =
        new Dictionary<string, Dictionary<string, (string, YamlMappingNode)>>();

    public ICollection<string>? AcceptLocales { get; set; }

    public ResourceLocalizationManager(
        Dictionary<string, Dictionary<string, string>> resources)
    {
        foreach ((var key3, Dictionary<string, string> dictionary1) in resources)
        {
            if (!this._resourceData.ContainsKey(key3))
                this._resourceData.Add(key3, new Dictionary<string, (string, YamlMappingNode)>());
            string str2;
            // foreach ((key3, str2) in dictionary1)
            // {
            //     var str3 = str2;
            //     this._resourceData[key3].Add(key3, (str3, (YamlMappingNode)null));
            // }
        }
    }


    public Task<string> GetLocalizedAsync(string key, string? keySection = null)
    {
        var acceptLocales = this.AcceptLocales;
        if (acceptLocales == null)
            throw new NoNullAllowedException("AcceptLocales");
        return this.GetLocalizedAsync(key, acceptLocales, keySection);
    }

    public Task<string> GetLocalizedAsync(
        string key,
        ICollection<string> acceptLocales,
        string? keySection = null)
    {
        if (acceptLocales != null)
        {
            foreach (string acceptLocale in (IEnumerable<string>)acceptLocales)
            {
                var localizationFromResource = this.getLocalizationFromResource(key, acceptLocale, keySection);
                if (localizationFromResource != null)
                    return Task.FromResult<string>(localizationFromResource);
            }
        }

        var localizationFromResource1 = this.getLocalizationFromResource(key, "en", keySection);
        return localizationFromResource1 != null
            ? Task.FromResult<string>(localizationFromResource1)
            : Task.FromResult<string>(key);
    }

    private string? getLocalizationFromResource(string key, string locale, string? keySection = "index")
    {
        if (string.IsNullOrWhiteSpace(keySection))
            keySection = "index";
        Dictionary<string, (string path, YamlMappingNode content)> dictionary;
        (string path, YamlMappingNode content) tuple;
        if (!this._resourceData.TryGetValue(locale, out dictionary) || !dictionary.TryGetValue(keySection, out tuple))
            return (string)null;
        if (tuple.content == null)
            this.lazyLoadResource(tuple.path, out tuple.content);
        YamlNode yamlNode;
        if (tuple.content.Children.TryGetValue((YamlNode)new YamlScalarNode(key), out yamlNode))
            return yamlNode.ToString();

        return (string)null;
    }

    private void lazyLoadResource(string path, out YamlMappingNode? content)
    {
        using var input = new StreamReader(path);
        var yamlStream = new YamlStream();
        yamlStream.Load((TextReader)input);
        content = (YamlMappingNode)yamlStream.Documents[0].RootNode;
    }
}

