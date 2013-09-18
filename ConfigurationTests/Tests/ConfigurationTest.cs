using System.Configuration;
using System.IO;
using RadicalGeek.Common.Collections;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public abstract class ConfigurationTest : FileTest
    {
        private bool isWebConfig;
        private string exePath;

        private readonly CacheList<string, Configuration> configurationCache = new CacheList<string, Configuration>(ConfigurationManager.OpenExeConfiguration);

        protected Configuration GetConfig()
        {
            if (!File.Exists(FullFilePath))
                throw new AssertionException(string.Format("File Not Found, {0}", FullFilePath));

            exePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(FullFilePath), System.IO.Path.GetFileNameWithoutExtension(FullFilePath));

            isWebConfig = System.IO.Path.GetFileName(exePath).ToLower() == "web" && !File.Exists(exePath);

            if (!isWebConfig && !File.Exists(exePath))
                throw new AssertionException(string.Format("Assembly Not Found, {0}", exePath));

            if (isWebConfig)
                File.Create(exePath).Close();

            Configuration configuration = configurationCache[exePath];

            if (isWebConfig)
                File.Delete(exePath);

            return configuration;
        }
    }
}
