using System;
using System.Collections.Generic;
using System.IO;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{

    public enum CpuArchitectures
    {
        Unknown,
        x86,
        x64,
        AnyCpu
    }

    public class PlatformTest : Test
    {
        [MandatoryField]
        public string AssemblyFilePath { get; set; }
        [MandatoryField]
        public CpuArchitectures ExpectedCpuArchitecture { get; set; }

        public override void Run()
        {
            try
            {
                CpuArchitectures ActualCpuArchitecture = GetPEArchitecture(AssemblyFilePath);
                if (ExpectedCpuArchitecture.ToString() != ActualCpuArchitecture.ToString()) 
                    throw new AssertionException(
                            string.Format("{0} was expected to be compiled for {1} but was actually compiled for {2}",
                                    AssemblyFilePath,
                                    ExpectedCpuArchitecture,
                                   ActualCpuArchitecture));
            }
            catch (BadImageFormatException)
            {
                throw new AssertionException(string.Format("Unable to load {0} because it is not a .Net assembly", AssemblyFilePath));
            }
        }

        public static CpuArchitectures GetPEArchitecture(string pFilePath)
        {
            ushort architecture = 0;

            ushort[] coffHeader = new ushort[10];

            using (FileStream fStream = new FileStream(pFilePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader bReader = new BinaryReader(fStream))
                {
                    if (bReader.ReadUInt16() == 23117) //check the MZ signature
                    {
                        fStream.Seek(0x3A, SeekOrigin.Current); // Go to the location of the location of the NT header
                        fStream.Seek(bReader.ReadUInt32(), SeekOrigin.Begin); // seek to the start of the NT header.
                        if (bReader.ReadUInt32() == 17744) //check the PE\0\0 signature.
                        {
                            for (int i = 0; i < 10; i++) // Read COFF Header
                                coffHeader[i] = bReader.ReadUInt16();
                            if (coffHeader[8] > 0) // Read Optional Header
                                architecture = bReader.ReadUInt16();
                        }
                    }
                }
            }

            switch (architecture)
            {
                case 0x20b:
                    return CpuArchitectures.x64;
                case 0x10b:
                    return ((coffHeader[9] & 0x100) == 0) ? CpuArchitectures.AnyCpu : CpuArchitectures.x86;
                default:
                    return CpuArchitectures.Unknown;
            }
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                        {
                            new PlatformTest
                                {
                                    ExpectedCpuArchitecture = CpuArchitectures.AnyCpu,
                                    TestName = "Any CPU Platform Check Example",
                                    AssemblyFilePath = @"C:\tfslocal\InteractiveApplications\Pawnbroking\Code\Main\PawnBrokingBackOffice\bin\Debug\PawnBrokingBackOffice.exe"
                                },
                                new PlatformTest
                                {
                                    ExpectedCpuArchitecture = CpuArchitectures.x86,
                                    TestName = "x86 Platform Check Example",
                                    AssemblyFilePath = @"C:\tfslocal\InteractiveApplications\Pawnbroking\Code\Main\PawnBrokingBackOffice\bin\Debug\PawnBrokingBackOffice.exe"
                                },
                                new PlatformTest
                                {
                                    ExpectedCpuArchitecture =  CpuArchitectures.x64,
                                    TestName = "x64 Platform Check Example",
                                    AssemblyFilePath = @"C:\tfslocal\InteractiveApplications\Pawnbroking\Code\Main\PawnBrokingBackOffice\bin\Debug\PawnBrokingBackOffice.exe"
                                }
                        };
        }
    }
}
