using System.IO;

namespace RadicalGeek.Testing.Smoke.TestConfiguration
{
    public static class Extensions
    {
        public static bool IsValidDirectory(this string path)
        {
            return Directory.Exists(path);
        }
    }
}
