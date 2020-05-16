using System;

namespace HalalAssignement.Solvers.GP
{
    public class HeapGenerator
    {
        public int HeadSize { get; set; }

        public NodeGenerator nodeGenerator;
        private Random gen;

        public HeapGenerator(int headSize, string[] possibleInputVarNames, Random gen)
        {
            HeadSize = headSize;
            nodeGenerator = new NodeGenerator(gen, possibleInputVarNames);
            this.gen = gen;
        }

        public Heap[] Generate(int n)
        {
            var result = new Heap[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = Generate();
            }

            return result;
        }

        public Heap Generate()
        {
            var genomeSize = gen.Next(3, HeadSize);
            var heap = new Heap(genomeSize, genomeSize * 2);

            // heads
            for (int i = 0; i < genomeSize; i++)
            {
                var roll = gen.NextDouble();

                if (roll < .7)
                {
                    heap.Head[i] = nodeGenerator.GenerateFunction();
                }
                else
                {
                    var n = nodeGenerator.GenerateTerminal();
                    heap.Head[i] = n;

                    if (n.InputId != default)
                    {
                        heap.Inputs.Add(n);
                    }
                }
            }

            // tail
            for (int i = 0; i < genomeSize * 2; i++)
            {
                var n = nodeGenerator.GenerateTerminal();
                heap.Tail[i] = n;

                if (n.InputId != default)
                {
                    heap.Inputs.Add(n);
                }
            }

            return heap;
        }
    }
}