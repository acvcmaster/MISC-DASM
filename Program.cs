using System;
using System.IO;
using CommandLine;

namespace MISC_DASM
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "The input file.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false, HelpText = "Set the output file.")]
        public string OutputFile { get; set; }
    }

    class Program
    {
        static unsafe void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(options =>
                {
                    if (!File.Exists(options.InputFile))
                    {
                        Console.WriteLine($"File '{options.InputFile}' not found.");
                        return;
                    }

                    byte[] buffer = null;
                    try { buffer = File.ReadAllBytes(options.InputFile); }
                    catch { Console.WriteLine($"Could not open '{options.InputFile}'. An error has occurred."); return; }

                    if (buffer == null || buffer.Length % 4 != 0)
                    {
                        Console.WriteLine("Invalid input file. Could not disassemble.");
                        return;
                    }

                    fixed (byte* start = buffer)
                    {
                        for (int i = 0; i < buffer.Length; i += sizeof(Instruction))
                        {
                            var instruction = *((Instruction*)(start + i));
                        }
                    }
                });
        }
    }
}
