using System;
using Scenarios.PriorityQueue;
using Structures;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== PRIORITY QUEUE =====\n");
            FindKLargestElements.Go(5, 100);

            Console.WriteLine("\n");

            AStarPathfinding.Go(15);

            Console.WriteLine("\n");

            HuffmanCompression.Go("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer faucibus at magna at feugiat. Suspendisse ultrices tellus eget ligula tincidunt, in efficitur magna tempor. Donec porta tellus at quam pellentesque venenatis. Ut ultricies sed justo et condimentum. Mauris varius tristique blandit. Maecenas imperdiet elit vitae gravida tincidunt. Fusce id laoreet sem. Suspendisse vehicula tellus metus, ac fermentum lacus ultrices ut. Nam tempor magna eu augue pulvinar malesuada. Nam venenatis est quis ornare semper. Donec vitae libero ut sem cursus rhoncus. Praesent convallis lorem quis sem commodo rutrum. Fusce ac elit a nunc viverra posuere vel eu metus.");
        }
    }
}
