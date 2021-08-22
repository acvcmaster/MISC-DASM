using System.Runtime.InteropServices;

namespace MISC_DASM
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Instruction : IDisassemblable
    {
        public int Data { get; set; }

        public string Disassemble()
        {
            throw new System.NotImplementedException();
        }
    }
}