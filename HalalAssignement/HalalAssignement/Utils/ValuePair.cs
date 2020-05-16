namespace HalalAssignement.Utils
{
    public class ValuePair
    {
        public string Name { get; set; }
        public float Input { get; set; }
        public float Output { get; set; }

        public ValuePair(float input, float output)
        {
            Input = input;
            Output = output;
        }
    }
}