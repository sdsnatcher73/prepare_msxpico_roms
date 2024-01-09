using System.Runtime.InteropServices;

namespace prepare_msxpico_roms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string romFilesDirName = ".";
            string concatOutputFileName = "msxpico.rom";
            string codeOutputFileName = "msxpico.c";
            Int64 romIndex = 0;
            Int64 concatOutputIndex = 0;

            const Int64 mapperGeneric8       = 0;  /* Generic switch, 8kB pages     */
            const Int64 mapperGeneric16      = 1;  /* Generic switch, 16kB pages    */
            const Int64 mapperKonami5        = 2;  /* Konami 5000/7000/9000/B000h   */
            const Int64 mapperKonami4        = 3;  /* Konami 4000/6000/8000/A000h   */
            const Int64 mapperASCII8         = 4;  /* ASCII 6000/6800/7000/7800h    */
            const Int64 mapperASCII16        = 5;  /* ASCII 6000/7000h              */
            const Int64 mapperGameMaster2    = 6;  /* Konami GameMaster2 cartridge  */
            const Int64 mapperFMPAC          = 7;  /* Panasonic FMPAC cartridge     */
            const Int64 mapperASCII16Ex      = 8;  /* ASCII16 with 16 bits mapper   */
            const Int64 mapperRType          = 9;  /* R-Type cartridge              */
            const Int64 mapperNextorRAM      = 10; /* Nextor                        */
            const Int64 mapperDSK2ROM        = 11; /* Modified dsk2rom              */
            const Int64 mapperPlain0000      = 12; /* Plain ROM starting at 0000h   */
            const Int64 mapperPlain4000      = 13; /* Plain ROM starting at 4000h   */
            const Int64 mapperPlain8000      = 14; /* Plain ROM starting at 8000h   */
            const Int64 mapperNeo16          = 15; /* Neo (Aoineko) 16kB pages      */
            const Int64 mapperNeo8           = 16; /* Neo (Aoineko) 8kB pages       */
            const Int64 mapperKonamiUltimate = 17; /* Neo (Aoineko) 8kB pages       */
            const Int64 mapperNextorNoRAM    = 18; /* Nextor without extra RAM      */
            const Int64 mapperSCCIOnly       = 19; /* SCC-I in mainslot             */

            const Int64 generationMSX1 = 0;
            const Int64 generationMSX2 = 1;

            if (args.Length == 1) 
            {
                romFilesDirName = args[0];
            }
            else
            {
                Console.WriteLine("Please only specify the directory containing the ROM files");
            }

            DirectoryInfo romFilesDirInfo = new DirectoryInfo(romFilesDirName);
            FileInfo[] romFilesInfo = romFilesDirInfo.GetFiles("*.rom");

            FileStream concatOutputFileStream = File.Open(Path.Combine(romFilesDirName, concatOutputFileName), FileMode.OpenOrCreate & FileMode.Truncate);
            StreamWriter codeOutputFileStreamWriter = new StreamWriter(Path.Combine(romFilesDirName, codeOutputFileName));

            foreach (FileInfo romFileInfo in romFilesInfo) 
            {
                string romFileName = romFileInfo.Name;
                Int64 romFileSize = romFileInfo.Length;

                string romName = romFileInfo.Name.Split('.')[0].Split('_')[0];
                string romMapperString = romFileInfo.Name.Split('.')[0].Split('_')[1];
                Int64 romMapper = -1;
                string romGenerationString = romFileInfo.Name.Split('.')[0].Split('_')[2];
                Int64 romGeneration = -1;

                switch (romMapperString)
                {
                    case "generic8":
                        {
                            romMapper = mapperGeneric8;
                            break;
                        }

                    case "generic16":
                        {
                            romMapper = mapperGeneric16;
                            break;
                        }

                    case "konami5":
                        {
                            romMapper = mapperKonami5;
                            break;
                        }

                    case "konami4":
                        {
                            romMapper = mapperKonami4;
                            break;
                        }

                    case "ascii8":
                        {
                            romMapper = mapperASCII8;
                            break;
                        }

                    case "ascii16":
                        {
                            romMapper = mapperASCII16;
                            break;
                        }

                    case "gamemaster2":
                        {
                            romMapper = mapperGameMaster2;
                            break;
                        }

                    case "ascii16ex":
                        {
                            romMapper = mapperASCII16Ex;
                            break;
                        }

                    case "rtype":
                        {
                            romMapper = mapperRType;
                            break;
                        }

                    case "dsk2rom":
                        {
                            romMapper = mapperDSK2ROM;
                            break;
                        }

                    case "plain0000":
                        {
                            romMapper = mapperPlain0000;
                            break;
                        }

                    case "plain4000":
                        {
                            romMapper = mapperPlain4000;
                            break;
                        }

                    case "plain8000":
                        {
                            romMapper = mapperPlain8000;
                            break;
                        }

                    case "neo16":
                        {
                            romMapper = mapperNeo16;
                            break;
                        }

                    case "neo8":
                        {
                            romMapper = mapperNeo8;
                            break;
                        }

                    case "konamiultimate":
                        {
                            romMapper = mapperKonamiUltimate;
                            break;
                        }

                }

                switch (romGenerationString)
                {
                    case "msx1":
                        {
                            romGeneration = generationMSX1;
                            break;
                        }

                    case "msx2":
                        {
                            romGeneration = generationMSX2;
                            break;
                        }
                }

                codeOutputFileStreamWriter.WriteLine("ROM Index:\t" + romIndex);
                codeOutputFileStreamWriter.WriteLine("ROM Name:\t" + romName);
                codeOutputFileStreamWriter.WriteLine("ROM Mapper:\t" + romMapper);
                codeOutputFileStreamWriter.WriteLine("ROM Generation:\t" + romGeneration);
                codeOutputFileStreamWriter.WriteLine("ROM Start Position:\t" + concatOutputIndex);
                codeOutputFileStreamWriter.WriteLine("ROM Size:\t" + romFileSize);

                FileStream romFileStream = File.Open(romFilesDirName + romFileName, FileMode.Open);

                romFileStream.CopyTo(concatOutputFileStream);
                romFileStream.Close();

                romIndex++;
                concatOutputIndex += romFileSize;
                
            }

            concatOutputFileStream.Close();
            codeOutputFileStreamWriter.Close();
        }
    }
}
