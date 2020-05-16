using System;

namespace HalalAssignement.Solvers.GP
{
    public class Entity
    {
        public Heap Gene { get; set; }
        public double Value { get; set; }
        public double Fitness { get; set; }

        internal Entity Cross(Entity partner, Random gen)
        {
            var offspringGene = new Heap(Gene.Head.Length, Gene.Tail.Length);

            var cutoffStart = gen.Next(1, Gene.Head.Length - 1);

            // build head
            for (int i = 0; i < Gene.Head.Length; i++)
            {
                if (i < cutoffStart)
                    offspringGene.Head[i] = Gene.Head[i];
                else
                    offspringGene.Head[i] = partner.Gene.Head[i];
            }

            cutoffStart = gen.Next(1, Gene.Tail.Length - 1);
            // build tail
            for (int i = 0; i < Gene.Tail.Length; i++)
            {
                if (i < cutoffStart)
                    offspringGene.Tail[i] = Gene.Tail[i];
                else
                    offspringGene.Tail[i] = partner.Gene.Tail[i];
            }

            return new Entity()
            {
                Gene = offspringGene,
            };
        }
    }
}