using System.Runtime.InteropServices;
using System.Text;
using ByteSizeLib;

namespace prepare_msxpico_roms
{
    struct romEntryHeader
    {
        public string name;
        public UInt32 offset;
        public UInt32 size;
        public UInt16 mapper;
        public UInt16 generation;

        const UInt16 nameIndex = 0;
        const UInt16 sizeIndex = nameIndex + 80;
        const UInt16 mapperIndex = sizeIndex + 4;
        const UInt16 generationIndex = mapperIndex + 2;

        public romEntryHeader(string name, Int64 offset, Int64 size, Int64 mapper, Int64 generation)
        {
            this.name = name[..80];
            this.size = (UInt32) size;
            this.mapper = (UInt16) mapper;
            this.generation = (UInt16) generation;
        }

        public readonly byte[] GetBytes()
        {
            byte[] header = new byte[96];

            Encoding.ASCII.GetBytes(name).CopyTo(header, nameIndex);
            BitConverter.GetBytes(size).CopyTo(header,sizeIndex);
            BitConverter.GetBytes(mapper).CopyTo(header, mapperIndex);
            BitConverter.GetBytes(generation).CopyTo(header, generationIndex);

            return header;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string romFilesDirName = ".";
            string concatOutputFileName = "msxpico.bin";
            UInt32 concatOutputSize = 0;

            const UInt16 mapperGeneric8 = 0;        /* Generic switch, 8kB pages     */
            const UInt16 mapperGeneric16 = 1;       /* Generic switch, 16kB pages    */
            const UInt16 mapperKonami5 = 2;         /* Konami 5000/7000/9000/B000h   */
            const UInt16 mapperKonami4 = 3;         /* Konami 4000/6000/8000/A000h   */
            const UInt16 mapperASCII8 = 4;          /* ASCII 6000/6800/7000/7800h    */
            const UInt16 mapperASCII16 = 5;         /* ASCII 6000/7000h              */
            const UInt16 mapperGameMaster2 = 6;     /* Konami GameMaster2 cartridge  */
            const UInt16 mapperFMPAC = 7;           /* Panasonic FMPAC cartridge     */
            const UInt16 mapperASCII16Ex = 8;       /* ASCII16 with 16 bits mapper   */
            const UInt16 mapperRType = 9;           /* R-Type cartridge              */
            const UInt16 mapperNextorRAM = 10;      /* Nextor                        */
            const UInt16 mapperDSK2ROM = 11;        /* Modified dsk2rom              */
            const UInt16 mapperPlain0000 = 12;      /* Plain ROM starting at 0000h   */
            const UInt16 mapperPlain4000 = 13;      /* Plain ROM starting at 4000h   */
            const UInt16 mapperPlain8000 = 14;      /* Plain ROM starting at 8000h   */
            const UInt16 mapperNeo16 = 15;          /* Neo (Aoineko) 16kB pages      */
            const UInt16 mapperNeo8 = 16;           /* Neo (Aoineko) 8kB pages       */
            const UInt16 mapperKonamiUltimate = 17; /* Neo (Aoineko) 8kB pages       */
            const UInt16 mapperNextorNoRAM = 18;    /* Nextor without extra RAM      */
            const UInt16 mapperSCCIOnly = 19;       /* SCC-I in mainslot             */

            const UInt16 generationMSX1 = 0;
            const UInt16 generationMSX2 = 1;

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
            concatOutputFileStream.SetLength(0);

            byte[] initConcatOutputFile;

            initConcatOutputFile = Encoding.ASCII.GetBytes("MSXPICO_ROM_CAT ");
            initConcatOutputFile[15] = 0x00;
            concatOutputFileStream.Write(initConcatOutputFile, 0, 16);
            
            foreach (FileInfo romFileInfo in romFilesInfo) 
            {
                romEntryHeader romHeader = new();

                romHeader.name = romFileInfo.Name.Split('.')[0].Split('_')[0];
                string romMapperString = romFileInfo.Name.Split('.')[0].Split('_')[1];
                string romGenerationString = romFileInfo.Name.Split('.')[0].Split('_')[2];
                romHeader.size = (UInt32) romFileInfo.Length;

                if (concatOutputSize + romHeader.size + (2 * 96) < (7 * 1024 * 1024))
                {
                    switch (romMapperString)
                    {
                        case "generic8":
                            {
                                romHeader.mapper = mapperGeneric8;
                                break;
                            }
    
                        case "generic16":
                            {
                                romHeader.mapper = mapperGeneric16;
                                break;
                            }
    
                        case "konami5":
                            {
                                romHeader.mapper = mapperKonami5;
                                break;
                            }
    
                        case "konami4":
                            {
                                romHeader.mapper = mapperKonami4;
                                break;
                            }
    
                        case "ascii8":
                            {
                                romHeader.mapper = mapperASCII8;
                                break;
                            }
    
                        case "ascii16":
                            {
                                romHeader.mapper = mapperASCII16;
                                break;
                            }
    
                        case "gamemaster2":
                            {
                                romHeader.mapper = mapperGameMaster2;
                                break;
                            }
    
                        case "ascii16ex":
                            {
                                romHeader.mapper = mapperASCII16Ex;
                                break;
                            }
    
                        case "rtype":
                            {
                                romHeader.mapper = mapperRType;
                                break;
                            }
    
                        case "dsk2rom":
                            {
                                romHeader.mapper = mapperDSK2ROM;
                                break;
                            }
    
                        case "plain0000":
                            {
                                romHeader.mapper = mapperPlain0000;
                                break;
                            }
    
                        case "plain4000":
                            {
                                romHeader.mapper = mapperPlain4000;
                                break;
                            }
    
                        case "plain8000":
                            {
                                romHeader.mapper = mapperPlain8000;
                                break;
                            }
    
                        case "neo16":
                            {
                                romHeader.mapper = mapperNeo16;
                                break;
                            }
    
                        case "neo8":
                            {
                                romHeader.mapper = mapperNeo8;
                                break;
                            }
    
                        case "konamiultimate":
                            {
                                romHeader.mapper = mapperKonamiUltimate;
                                break;
                            }
    
                    }
    
                    switch (romGenerationString)
                    {
                        case "msx1":
                            {
                                romHeader.generation = generationMSX1;
                                break;
                            }
    
                        case "msx2":
                            {
                                romHeader.generation = generationMSX2;
                                break;
                            }
                    }
    
                    
                    FileStream romFileStream = File.Open(Path.Combine(romFilesDirName, romFileInfo.Name), FileMode.Open);
    
                    concatOutputFileStream.Write(romHeader.GetBytes(),0 ,96);
                    romFileStream.CopyTo(concatOutputFileStream);
                    romFileStream.Close();

                    concatOutputSize += (romHeader.size + 96);
                }
                else
                {
                    Console.WriteLine("ROM file " + romHeader.name + " skipped because resulting outputfile would exceed 7MB.");
                }
            }

            Console.WriteLine("Size of msxpico.bin:\t" + ByteSize.FromBytes(concatOutputSize + 96).ToString("#.# KiB"));
            Console.WriteLine("Remaining flash space:\t" + ByteSize.FromBytes((7 * 1024 * 1024) - (concatOutputSize + 96)).ToString("#.# KiB"));
            // Write a terminator header so pico knows he is finished;
            byte[] romTerminator = new byte[96];
            concatOutputFileStream.Write(romTerminator, 0, 96);

            concatOutputFileStream.Close();
        }
    }
}
