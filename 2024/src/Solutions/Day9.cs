using _2024.src.Interfaces;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day9 : ISolution
    {
        public ushort DayNumber => 9;

        private class DefragBlock(int id, int space) : IDeepCopyable<DefragBlock>
        {
            public int id = id; // -1 If free space
            public int space = space;

            public DefragBlock DeepCopy()
            {
                return new(id, space);
            }
        }

        private DefragBlock[] memory = [];



        private static int GetNewEndPtr(List<DefragBlock> list, int currentEndPtr)
        {
            if(list[currentEndPtr].id != -1 ) return currentEndPtr;
            return GetNewEndPtr(list, currentEndPtr - 1);
        }


        private DefragBlock[] DiskCompactor()
        {
            List<DefragBlock> listMemory = [.. ArrayUtils.DeepCopyReferenceArray(memory)];

            int endMemPtr = GetNewEndPtr(listMemory, listMemory.Count - 1);
            for(int i = 0; i < listMemory.Count - 1; ++i)
            {
                if(listMemory[i].id != -1) continue; // If not free space, continue, as cannot insert anything into there

                DefragBlock? newBlock = null; // Will be filled with contiguous ids
                while(listMemory[i].space > 0) // While a free space still has space, move elements at end of memory to space
                {
                    if(i >= endMemPtr) break; // If index has passed end, unable to do anymore
                    newBlock ??= new(listMemory[endMemPtr].id, 0);
                    newBlock.space ++;
                    listMemory[endMemPtr].space --;
                    listMemory[i].space --;

                    if(listMemory[endMemPtr].space == 0) // When end block has ran out of ids to give, overwrite the free space with the block
                    {
                        listMemory.Insert(i + 1, new(-1, listMemory[i].space)); // Insert new free space after to be overwritten block
                        listMemory[i] = newBlock;
                        newBlock = null;
                        ++i; // Increment to point to new free space

                        listMemory[endMemPtr + 1].id = -1; // + 1 as inserted value has bumped right array indexes up by 1
                        endMemPtr = GetNewEndPtr(listMemory, endMemPtr);
                    }
                }

                // When out of free space at i, if any ids ready to move, overwrite them in previous free space slot
                if(newBlock != null)  listMemory[i] = newBlock;
            }

            return [.. listMemory];
        }



        private DefragBlock[] WholeFileDiskCompactor()
        {
            List<DefragBlock> listMemory = [.. ArrayUtils.DeepCopyReferenceArray(memory)];

            // Iterate back to front to attempt to move file partitions where it is possible to do so
            for (int i = listMemory.Count - 1; i >= 0; --i)
            {
                DefragBlock block = listMemory[i];
                if (block.id == -1) continue; // If just free space block, continue

                for (int j = 0; j < i; ++j)
                {
                    DefragBlock comparisonBlock = listMemory[j];
                    if (comparisonBlock.id != -1) continue; // If comparison isn't possible free space to insert into, continue
                    if (comparisonBlock.space < block.space) continue; // If comparison free space is too small for block, continue

                    int remainingSpace = comparisonBlock.space - block.space;
                    
                    listMemory[j] = block; // Overwrite free space with inserted data
                    listMemory[i] = new(-1, block.space); // Replace inserted data from old position with free space

                    if(remainingSpace > 0) 
                    {
                        ++i;
                        listMemory.Insert(j + 1, new(-1, remainingSpace)); // Insert remaining space afterwards
                    }
                    break;
                }
            }

            return [.. listMemory];
        }

        private static long Checksum(DefragBlock[] compactedMemory)
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


        public string? ExecPartA() => Checksum(DiskCompactor()).ToString();
        public string? ExecPartB() => Checksum(WholeFileDiskCompactor()).ToString();

        public void Setup(string[] input, string continuousInput)
        {
            List<DefragBlock> listMemory = [];
            for (int i = 0; i < continuousInput.Length; i += 2)
            {
                listMemory.Add(new(i / 2, continuousInput[i] - '0')); // Add used space to memory
                if(i != continuousInput.Length - 1) listMemory.Add(new(-1, continuousInput[i + 1] - '0')); // Unless at end, add free space to memory
            }

            memory = [.. listMemory];
        }
    }
}