using _2024.src.Interfaces;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day9 : ISolution
    {
        public ushort DayNumber => 9;

        private class Block(int id, int size, int freeSpace) : IDeepCopyable<Block>
        {
            public readonly int id = id;
            public readonly int freeSpace = freeSpace;
            public int size = size;

            public Block DeepCopy()
            {
                return new Block(id, size, freeSpace);
            }
        }

        private class DefragBlock(int id, int count = 0)
        {
            public readonly int id = id;
            public int count = count;
        }

        private Block[] memory = [];


        private DefragBlock[] DiskCompactor()
        {
            Block[] memCopy = ArrayUtils.DeepCopyReferenceArray(memory);

            List<DefragBlock> compactedMemory = [];

            int memEndPtr = memory.Length - 1;

            for (int i = 0; i <= memEndPtr; ++i)
            {
                Block memBlock = memCopy[i];

                DefragBlock defragBlock = new(memBlock.id);
                for (int j = 0; j < memBlock.size; ++j) // Push current spaces in memory to defragmented memory
                {
                    ++defragBlock.count;
                }

                if (defragBlock.count > 0) compactedMemory.Add(defragBlock); // If any was added, push to new space in defragmented memory

                if (i == memEndPtr) continue; // If at end of where there is any used space, remaining free space is irrelevant as nothing can move there

                DefragBlock? movedDefragBlock = null; // When is null gets reset with new memEnd values
                for (int j = 0; j < memBlock.freeSpace; ++j) // If there is free space, move elements from end into it
                {
                    Block endMemBlock = memCopy[memEndPtr];
                    movedDefragBlock ??= new(memCopy[memEndPtr].id);

                    if (endMemBlock.size == 0) // Should only occur at the very start of iteration if end is by default full
                    {
                        --memEndPtr;
                        --j;
                        continue;
                    }

                    movedDefragBlock.count++;
                    --endMemBlock.size;

                    if (endMemBlock.size == 0) // If no more available to move to the front, decrement to next rear memory location with free space
                    {
                        --memEndPtr;
                        compactedMemory.Add(movedDefragBlock);
                        movedDefragBlock = null;
                    }
                }

                if (movedDefragBlock != null) compactedMemory.Add(movedDefragBlock); // If not null add whatever is left over at end of remaining free space
            }

            return [.. compactedMemory];
        }


        private long Checksum()
        {
            long total = 0;
            DefragBlock[] compactedMemory = DiskCompactor();

            int multiplier = 0;

            foreach (DefragBlock block in compactedMemory)
            {
                for (int i = 0; i < block.count; ++i)
                {
                    total += block.id * multiplier;
                    ++multiplier;
                }
            }

            return total;
        }


        public string? ExecPartA()
        {
            return Checksum().ToString();
        }

        public string? ExecPartB()
        {
            return null;
        }

        public void Setup(string[] input, string continuousInput)
        {
            memory = new Block[(continuousInput.Length + 1) / 2];
            for (int i = 0; i < continuousInput.Length; i += 2)
            {
                int size = continuousInput[i] - '0';

                if (i == continuousInput.Length - 1) memory[i / 2] = new(i / 2, size, 0);
                else memory[i / 2] = new(i / 2, size, continuousInput[i + 1] - '0');
            }
        }
    }
}