using _2024.src.Interfaces;
using _2024.src.Types;
using _2024.src.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace _2024.src.Solutions
{
    public partial class Day14 : ISolution
    {
        public ushort DayNumber => 14;
        
        private class Robot(Vector2Int pos, Vector2Int velocity) : IDeepCopyable<Robot>
        {
            public Vector2Int pos = pos;
            public Vector2Int velocity = velocity;

            public Robot DeepCopy() => new(pos, velocity);
        }

        private const int XLEN = 101;
        private const int YLEN = 103;
        private Robot[] robots = [];
        
        private const int SCALE_FACTOR = 1;
        private readonly static string imagePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "src", "images"));

        private static void CalculateNewRobotPos(Robot r, int seconds = 1)
        {
            if(seconds <= 0) throw new ArgumentException("Invalid time");

            Vector2Int pos = r.pos;
            for(int i = 0; i < seconds; ++i)
            {
                pos += r.velocity;

                // Wrap around
                if(pos.x >= XLEN) pos.x -= XLEN;
                if(pos.x < 0) pos.x += XLEN;
                if(pos.y >= YLEN) pos.y -= YLEN;
                if(pos.y < 0) pos.y += YLEN;
            }

            r.pos = pos;
        }

        private static void CalculateRobotPositions(Robot[] robots, int seconds = 1)
        {
            foreach(Robot r in robots)
            {
                CalculateNewRobotPos(r, seconds);
            }
        }


        private int CalculateQuadrantTotal()
        {
            Vector2Int centre = new(XLEN / 2, YLEN / 2);
            int TL = 0, TR = 0, BL = 0, BR = 0;

            foreach(Robot robot in robots)
            {
                Vector2Int p = robot.pos;
                if(p.x == centre.x || p.y == centre.y) continue; // If in intersection of quadrants ignore

                if(p.x < centre.x && p.y < centre.y) TL ++;
                else if(p.x < centre.x && p.y > centre.y) BL ++;
                else if(p.y < centre.y) TR ++;
                else BR ++;
            }

            return TL * TR * BL * BR;
        }

        private static void GenerateBitMap(Robot[] robots, string name)
        {
            using var map = new Image<L8>(XLEN * SCALE_FACTOR, YLEN * SCALE_FACTOR);
            
            for(int x = 0; x < XLEN; ++x)
            {
                for(int y = 0; y < YLEN; ++y)
                {
                    map[x * SCALE_FACTOR, y * SCALE_FACTOR] = new(0);
                }
            }

            foreach(Robot r in robots)
            {
                map[r.pos.x * SCALE_FACTOR, r.pos.y * SCALE_FACTOR] = new(255);
            }

            map.SaveAsBmp(Path.Combine(imagePath, name + ".bmp"));
        }


        public string? ExecPartA()
        {
            Robot[] movedRobots = ArrayUtils.DeepCopyArray(robots);
            CalculateRobotPositions(movedRobots);
            return CalculateQuadrantTotal().ToString();
        }
        public string? ExecPartB()
        {
            Robot[] movedRobots = ArrayUtils.DeepCopyArray(robots);
            // for(int i = 5000; i <= 10000; ++i)
            // {
            //     CalculateRobotPositions(movedRobots);
            //     GenerateBitMap(movedRobots, i.ToString());
            //     Console.WriteLine($"Image {i} Generated");
            // }

            return null;
        }
        public void Setup(string[] input, string continuousInput) 
        {
            List<Robot> robotList = [];
            foreach(string line in input)
            {
               int[] digits = ParsingUtils.ParseDigits(line).ToArray();
               if(digits.Length != 4) continue;

                robotList.Add(new(new(digits[0], digits[1]), new(digits[2], digits[3])));
            }

            robots = [.. robotList];
        }
    }
}