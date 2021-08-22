using System;
using System.IO;
using System.Text;
using CommandLine;

namespace MISC_DASM
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "The input file.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false, HelpText = "Set the output file.")]
        public string OutputFile { get; set; }

        [Option('h', "hide-addresses", Required = false, HelpText = "Hides instruction addresses when disassembling.")]
        public bool HideAddresses { get; set; }

        [Option('s', "show-output", Required = false, HelpText = "When writing to a file, prints the output to the console.")]
        public bool ShowOutput { get; set; }
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
                        var output = new StringBuilder();

                        for (int i = 0; i < buffer.Length; i += sizeof(Instruction))
                        {
                            var instruction = *((Instruction*)(start + i));

                            if (!options.HideAddresses)
                                output.Append($"[0x{i.ToString("X").PadLeft(4, '0')}] {instruction.Disassemble()}\n");
                            else
                                output.Append($"{instruction.Disassemble()}\n");

                            if ((i / 4 + 1) % 4 == 0)
                                output.Append('\n');
                        }

                        if (options.OutputFile != null)
                        {
                            try
                            {
                                using (var writer = new StreamWriter(options.OutputFile))
                                    writer.Write(output);
                                
                                if (options.ShowOutput)
                                    Console.Write(output);
                            }
                            catch { Console.WriteLine("Could not write data to file. An error has occurred."); }
                        }
                        else
                            Console.Write(output);
                    }
                });
        }
    }
}
