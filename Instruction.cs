using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace MISC_DASM
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Instruction : IDisassemblable
    {
        public int Data { get; set; }

        public unsafe string Disassemble()
        {
            var dataBytes = BitConverter.GetBytes(Data);

            string iMnemonic = string.Empty;
            string iLeft = string.Empty;
            string iRight = string.Empty;
            string iData = string.Empty;

            fixed (byte* data = dataBytes)
            {
                if (!AssemblyData.Mnemonics.ContainsKey(*data))
                    return "?";

                iMnemonic = AssemblyData.Mnemonics[*data];

                if (!AssemblyData.Operands.ContainsKey(*(data + 1)))
                    return "?";

                var operandCombination = AssemblyData.Operands[*(data + 1)];
                iLeft = operandCombination.Left;
                iRight = operandCombination.Right;

                var dataString = (*((ushort*)(data + 2))).ToString("X");
                iData = iRight == "CT" ? $"#{dataString}" : dataString;
            }

            if (AssemblyData.MnemonicDualStatement.Contains(iMnemonic))
            {
                return iRight == "CT" || iRight == "MA"
                    ? $"{iMnemonic} {iLeft} {iData}"
                    : $"{iMnemonic} {iLeft} {iRight}";
            }
            else if (AssemblyData.MnemonicMonoStatement.Contains(iMnemonic))
            {
                return iRight == "CT" || iRight == "MA"
                    ? $"{iMnemonic} {iData}"
                    : $"{iMnemonic} {iRight}";
            }
            else return iMnemonic;
        }
    }
}