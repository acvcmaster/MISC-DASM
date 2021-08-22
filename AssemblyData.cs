using System.Collections.Generic;

namespace MISC_DASM
{
    public class OperandCombination
    {
        public string Left { get; set; }
        public string Right { get; set; }
    }

    public static class AssemblyData
    {
        public static IDictionary<byte, string> Mnemonics = new Dictionary<byte, string>()
        {
            { 0, "HALT" },
            { 1, "ADD" },
            { 2, "SUB" },
            { 3, "MUL" },
            { 4, "DIV" },
            { 5, "MOV" },
            { 6, "LD" },
            { 7, "ULD" },
            { 8, "BZ" },
            { 9, "SWI" },
            { 10, "CALL" },
            { 11, "RET" },
            { 12, "NOP" },
        };

        private static string[] _Operands = new string[]
        {
            "R1", "R2", "R3", "R4",
            "IP", "CT", "MA"
        };

        public static IDictionary<byte, OperandCombination> Operands
        {
            get
            {
                var result = new Dictionary<byte, OperandCombination>();
                byte count = 0;

                for (int i = 0; i < _Operands.Length; i++)
                    for (int j = 0; j < _Operands.Length; j++)
                    {
                        result.Add(count, new OperandCombination()
                        {
                            Left = _Operands[i],
                            Right = _Operands[j]
                        });
                        count++;
                    }

                return result;
            }
        }

        public static string[] MnemonicStatement = new string[] { "HALT", "RET", "NOP" };
        public static string[] MnemonicMonoStatement = new string[] { "LD", "ULD", "SWI", "CALL" };
        public static string[] MnemonicDualStatement = new string[] { "ADD", "SUB", "MUL", "DIV", "MOV", "BZ" };
    }
}