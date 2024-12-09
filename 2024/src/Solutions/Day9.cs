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

        private class DefragBlock(int id, int space)
        {
            public readonly int id = id;
            public int space = space;
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

                DefragBlock defragBlock = new(memBlock.id, 0);
                for (int j = 0; j < memBlock.size; ++j) // Push current spaces in memory to defragmented memory
                {
                    ++defragBlock.space;
                }

                if (defragBlock.space > 0) compactedMemory.Add(defragBlock); // If any was added, push to new space in defragmented memory


                DefragBlock? movedDefragBlock = null; // When is null gets reset with new memEnd values
                for (int j = 0; j < memBlock.freeSpace; ++j) // If there is free space, move elements from end into it
                {
                    if (i == memEndPtr) break; // If at end of where there is any used space, remaining free space is irrelevant as nothing can move there
                    Block endMemBlock = memCopy[memEndPtr];
                    movedDefragBlock ??= new(memCopy[memEndPtr].id, 0);

                    if (endMemBlock.size == 0) // Should only occur at the very start of iteration if end is by default full
                    {
                        --memEndPtr;
                        --j;
                        continue;
                    }

                    movedDefragBlock.space++;
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



        // convert free spaces in old memory into new 
        private DefragBlock[] GetFreeSpaceMemory()
        {
            Block[] memCopy = ArrayUtils.DeepCopyReferenceArray(memory);
            List<DefragBlock> freeSpaceMemory = [];

            foreach (Block block in memCopy)
            {
                if (block.size > 0) freeSpaceMemory.Add(new(block.id, block.size));
                if (block.freeSpace > 0) freeSpaceMemory.Add(new(-1, block.freeSpace));
            }

            return [.. freeSpaceMemory];
        }


        // needs to utilise Defrag block to assign similar to last function but instead use defrag block for free spaces
        private DefragBlock[] WholeFileDiskCompactor()
        {
            List<DefragBlock> freeSpaceMemory = [.. GetFreeSpaceMemory()];

            // Iterate back to front to attempt to move file partitions where it is possible to do so
            for (int i = freeSpaceMemory.Count - 1; i >= 0; --i)
            {
                DefragBlock block = freeSpaceMemory[i];
                if (block.id == -1) continue; // If just free space block, continue

                for (int j = 0; j < i; ++j)
                {
                    DefragBlock comparisonBlock = freeSpaceMemory[j];
                    if (comparisonBlock.id != -1) continue; // If comparison isn't possible free space to insert into, continue
                    if (comparisonBlock.space < block.space) continue; // If comparison free space is too small for block, continue

                    int remainingSpace = comparisonBlock.space - block.space;
                    
                    freeSpaceMemory[j] = block; // Overwrite free space with inserted data
                    freeSpaceMemory[i] = new(-1, block.space); // Replace inserted data from old position with free space

                    if(remainingSpace > 0) 
                    {
                        ++i;
                        freeSpaceMemory.Insert(j + 1, new(-1, remainingSpace)); // Insert remaining space afterwards
                    }
                    break;
                }
            }

            return [.. freeSpaceMemory];
        }



        private static long Checksum(DefragBlock[] compactedMemory)
        {
            long total = 0;
            int multiplier = 0;

            foreach (DefragBlock block in compactedMemory)
            {
                for (int i = 0; i < block.space; ++i)
                {
                    total += block.id * multiplier;
                    multiplier ++;
                }
            }
            return total;
        }


        private static long Checksum2(DefragBlock[] compactedMemory)
        {
            long total = 0;
            int multiplier = 0;

            foreach(DefragBlock block in compactedMemory)
            {
                for(int i = 0; i < block.space; ++i)
                {
                    if(block.id != -1) total += block.id * multiplier;
                    multiplier ++;
                }
            }

            return total;
        }


        public string? ExecPartA()
        {
            return Checksum(DiskCompactor()).ToString();
        }

        public string? ExecPartB()
        {
            return Checksum2(WholeFileDiskCompactor()).ToString();
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