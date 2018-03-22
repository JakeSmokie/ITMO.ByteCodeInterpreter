using System;
using LAPPI.Module;

namespace LAPPI
{
    public static class InterpreterUtils
    {
        internal static void RunDestSrc(InterpreterStorage storage, Func<int, int, int> func)
        {
            var val = ReadVar(storage);
            SaveValue(storage, val, func);
        }
        public static int ReadInt32(int cursor, byte[] blob)
        {
            return blob[cursor] + (blob[cursor + 1] << 8) + (blob[cursor + 2] << 16) + (blob[cursor + 3] << 24);
        }

        internal static void RunJumpCondition(InterpreterStorage storage, Func<int, int, bool> predicate)
        {
            var offset = BitConverter.ToInt32(storage.module.codeSection.Blob, storage.cursor);
            storage.cursor += 4;

            var a = ReadVar(storage);
            var b = ReadVar(storage);

            if (predicate(a, b))
            {
                storage.cursor = offset;
            }
        }

        internal static void SaveValue(InterpreterStorage storage, int input, Func<int, int, int> func = null)
        {
            var type = (VariableType) storage.module.codeSection.Blob[storage.cursor++];

            switch (type)
            {
                case VariableType.Register:
                    int reg = storage.module.codeSection.Blob[storage.cursor++];
                    storage.registers[reg] = func?.Invoke(storage.registers[reg], input) ?? input;
                    break;
                case VariableType.Offset:
                    int address = BitConverter.ToInt32(storage.module.codeSection.Blob, storage.cursor);
                    storage.cursor += 4;

                    if (func != null)
                    {
                        int old = BitConverter.ToInt32(storage.module.codeSection.Blob, address);
                        input = func(old, input);
                    }

                    var bytes = BitConverter.GetBytes(input);
                    storage.module.codeSection.Blob[address] = bytes[0];
                    storage.module.codeSection.Blob[address + 1] = bytes[1];
                    storage.module.codeSection.Blob[address + 2] = bytes[2];
                    storage.module.codeSection.Blob[address + 3] = bytes[3];

                    break;
                case VariableType.Array:
                    address = BitConverter.ToInt32(storage.module.codeSection.Blob, storage.cursor);
                    storage.cursor += 4;
                    address += ReadVar(storage) * 4;

                    if (func != null)
                    {
                        int old = BitConverter.ToInt32(storage.module.codeSection.Blob, address);
                        input = func(old, input);
                    }

                    bytes = BitConverter.GetBytes(input);
                    storage.module.codeSection.Blob[address] = bytes[0];
                    storage.module.codeSection.Blob[address + 1] = bytes[1];
                    storage.module.codeSection.Blob[address + 2] = bytes[2];
                    storage.module.codeSection.Blob[address + 3] = bytes[3];

                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        internal static int ReadVar(InterpreterStorage storage)
        {
            var blob = storage.module.codeSection.Blob;
            var type = (VariableType)blob[storage.cursor++];
            var result = 0;

            switch (type)
            {
                case VariableType.Register:
                    result = storage.registers[blob[storage.cursor++]];
                    break;
                case VariableType.Offset:
                    var address = BitConverter.ToInt32(blob, storage.cursor);
                    storage.cursor += 4;
                    result = BitConverter.ToInt32(blob, address);
                    break;
                case VariableType.Const:
                    result = BitConverter.ToInt32(blob, storage.cursor);
                    storage.cursor += 4;
                    break;
                case VariableType.Array:
                    address = BitConverter.ToInt32(blob, storage.cursor);
                    storage.cursor += 4;
                    result = BitConverter.ToInt32(blob, address + ReadVar(storage) * 4);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }



        public static void SaveInt32(BinaryModule module, int offset, int val)
        {
            //module.DataSection.Blob[offset] = (byte) ((val) & 0xFF);
            //module.DataSection.Blob[offset + 1] = (byte) ((val >> 8) & 0xFF);
            //module.DataSection.Blob[offset + 2] = (byte) ((val >> 16) & 0xFF);
            //module.DataSection.Blob[offset + 3] = (byte) ((val >> 24) & 0xFF);
        }

        public enum VariableType : byte
        {
            Register = 0x00,
            Offset = 0x01,
            Const = 0x02,
            Array = 0x03
        }
    }
}
