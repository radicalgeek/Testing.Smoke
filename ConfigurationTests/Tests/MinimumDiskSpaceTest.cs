using System.Collections.Generic;
using System.IO;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class MinimumDiskSpaceTest : Test
    {
        [MandatoryField]
        public string Drive { get; set; }

        [MandatoryField]
        public long RequiredMegabytes { get; set; }

        public override void Run()
        {
            DriveInfo d = new DriveInfo(Drive);
            long actual = d.AvailableFreeSpace / 1024;
            if (actual < RequiredMegabytes)
                throw new AssertionException<long>(RequiredMegabytes, actual, string.Format("Insufficient space on drive {0}",Drive));
        }

        public override List<Test> CreateExamples()
        {
                return new List<Test>
                             {
                                 new MinimumDiskSpaceTest{Drive="C",RequiredMegabytes = 123456,TestName = "Check space on C:"},
                                 new MinimumDiskSpaceTest{Drive="D",RequiredMegabytes = 12,TestName = "Check space on D:"}
                             };
        }
    }
}