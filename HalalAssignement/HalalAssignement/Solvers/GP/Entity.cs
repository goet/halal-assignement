using System;
using System.Collections.Generic;

namespace HalalAssignement.Solvers.GP
{
    public class Entity
    {
        public Heap Gene { get; set; }
        public double Value { get; set; }
        public double Fitness { get; set; }

        internal Entity Cross(Entity partner, Random gen)
        {
            var keepPercent = gen.NextDouble();

            var offspringHead = new List<Node>();
            for (int i = 0; i < Gene.Head.Length * keepPercent; i++)
            {
                offspringHead.Add(Gene.Head[i]);
            }
            for (int i = (int)(partner.Gene.Head.Length * keepPercent); i < partner.Gene.Head.Length; i++)
            {
                offspringHead.Add(partner.Gene.Head[i]);
            }


            var offSpringTail = new List<Node>();
            for (int i = 0; i < Gene.Tail.Length * keepPercent; i++)
            {
                offSpringTail.Add(Gene.Tail[i]);
            }
            for (int i = (int)(partner.Gene.Tail.Length * keepPercent); i < partner.Gene.Tail.Length; i++)
            {
                offSpringTail.Add(partner.Gene.Tail[i]);
            }

            var offspringGene = new Heap(0, 0);
            offspringGene.Head = offspringHead.ToArray();
            offspringGene.Tail = offSpringTail.ToArray();

            //// build head
            //for (int i = 0; i < partner.Head.Length; i++)
            //{
            //    if (i < keepPercent)
            //        offspringGene.Head[i] = Gene.Head[i];
            //    else
            //        offspringGene.Head[i] = partner.Gene.Head[i];
            //}

            //keepPercent = gen.Next(1, Gene.Tail.Length - 1);
            //// build tail
            //for (int i = 0; i < Gene.Tail.Length; i++)
            //{
            //    if (i < keepPercent)
            //        offspringGene.Tail[i] = Gene.Tail[i];
            //    else
            //        offspringGene.Tail[i] = partner.Gene.Tail[i];
            //}

            return new Entity()
            {
                Gene = offspringGene,
            };
        }

        public override string ToString()
        {
            return Gene.ToString();
        }
    }
}