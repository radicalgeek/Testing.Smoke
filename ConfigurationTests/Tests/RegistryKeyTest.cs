using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Win32;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class RegistryKeyTest : Test
    {
        [DefaultProperty("ValueName")]
        public class RegistryEntry
        {
            public string ValueName { get; set; }
            public object ExpectedValue { get; set; }
            public override string ToString()
            {
                return ValueName;
            }
        }

        public enum RegistryBaseKey
        {
            // ReSharper disable InconsistentNaming
            HKEY_CLASSES_ROOT,
            HKEY_LOCAL_MACHINE,
            HKEY_CURRENT_CONFIG,
            HKEY_CURRENT_USER,
            HKEY_PERFORMANCE_DATA,
            HKEY_USERS
            // ReSharper restore InconsistentNaming
        }

        [MandatoryField]
        public RegistryBaseKey BaseKey { get; set; }
        [MandatoryField]
        public string Path { get; set; }
        [MandatoryField]
        public List<RegistryEntry> ExpectedEntries { get; set; }

        protected object GetValue(string entryName)
        {
            return Key.GetValue(entryName);
        }

        private RegistryKey key;

        private readonly Dictionary<RegistryBaseKey, RegistryKey> baseKeys =
            new Dictionary<RegistryBaseKey, RegistryKey>
                    {
                    {RegistryBaseKey.HKEY_CLASSES_ROOT,Registry.ClassesRoot},
                    {RegistryBaseKey.HKEY_LOCAL_MACHINE, Registry.LocalMachine},
                    {RegistryBaseKey.HKEY_CURRENT_CONFIG,Registry.CurrentConfig},
                    {RegistryBaseKey.HKEY_PERFORMANCE_DATA,Registry.PerformanceData},
                    {RegistryBaseKey.HKEY_USERS,Registry.Users},
                    {RegistryBaseKey.HKEY_CURRENT_USER,Registry.CurrentUser}
                    };

        private RegistryKey Key
        {
            get
            {
                if (key == null)
                {
                    key = baseKeys[BaseKey];
                    string path = Path;
                    if (!path.EndsWith("/")) path += "/";
                    while (path.Length > 0)
                    {
                        string keyName = path.Substring(0, path.IndexOf('/'));
                        key = key.OpenSubKey(keyName);
                        path = path.Substring(path.IndexOf('/') + 1);
                    }
                }
                return key;
            }
        }

        public override void Run()
        {
            foreach (RegistryEntry entry in ExpectedEntries)
            {
                object actual = GetValue(entry.ValueName);
                AssertState.Equal(entry.ExpectedValue, actual);
            }
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                           {
                               new RegistryKeyTest
                                   {
                                       BaseKey = RegistryBaseKey.HKEY_CURRENT_CONFIG,
                                       Path = "Software/Fonts",
                                       ExpectedEntries =
                                           new List<RegistryEntry>
                                               {new RegistryEntry {ValueName = "LogPixels", ExpectedValue = 60}}
                                   },
                               new RegistryKeyTest
                                   {
                                       BaseKey = RegistryBaseKey.HKEY_LOCAL_MACHINE,
                                       Path = "Software/Microsoft/Windows/CurrentVersion/BITS",
                                       ExpectedEntries =
                                           new List<RegistryEntry>
                                               {
                                                   new RegistryEntry
                                                       {ValueName = "IGDSercherDLL", ExpectedValue = @"bitsigd.dll"},
                                                   new RegistryEntry
                                                       {ValueName = "JobInactivityTimeout", ExpectedValue = 0x0076a700}
                                               }
                                   }

                           };
        }
    }
}

