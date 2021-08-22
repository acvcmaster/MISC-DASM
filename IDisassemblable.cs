namespace MISC_DASM
{
    public interface IDisassemblable
    {
        /// <summary>Get the disassembled instruction.</summary>
        /// <returns>The disassembled instruction.</returns>
        string Disassemble();
    }
}