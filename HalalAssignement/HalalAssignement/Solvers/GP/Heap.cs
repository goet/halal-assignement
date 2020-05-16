using System.Collections.Generic;

namespace HalalAssignement.Solvers.GP
{
    public class Heap
    {
        public Node[] Head { get; set; }
        public Node[] Tail { get; set; }

        public List<Node> Inputs { get; set; } = new List<Node>();

        public Heap(int headSize, int tailSize)
        {
            Head = new Node[headSize];
            Tail = new Node[tailSize];
        }

        public void ApplyInputs(string inputId, double value)
        {
            foreach (var input in Inputs)
            {
                if (input.InputId == inputId)
                    input.ConstantValue = value;
            }
        }

        public void ToValidTree(out Node root)
        {
            root = Head[0];

            var remainingHeads = new Queue<Node>(Head);
            var remainingTails = new Queue<Node>(Tail);

            var headsWithoutTails = new Queue<Node>();
            headsWithoutTails.Enqueue(root);

            while (headsWithoutTails.Count != 0)
            {
                var p0 = headsWithoutTails.Dequeue();

                var p1 = GetHeadOrTail(remainingHeads, remainingTails);
                p0.Left = p1;
                if (!p1.IsTerminal)
                    headsWithoutTails.Enqueue(p1);

                var p2 = GetHeadOrTail(remainingHeads, remainingTails);
                p0.Right = p2;
                if (!p2.IsTerminal)
                    headsWithoutTails.Enqueue(p2);
                
            }
        }

        private Node GetHeadOrTail(Queue<Node> heads, Queue<Node> tails)
        {
            if (heads.Count != 0)
                return heads.Dequeue();

            return tails.Dequeue();
        }
    }
}