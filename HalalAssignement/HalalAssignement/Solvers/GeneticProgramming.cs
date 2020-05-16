using HalalAssignement.Solvers.GP;
using HalalAssignement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HalalAssignement.Solvers
{
    public class GeneticProgramming
    {
        public int PopSize { get; set; }
        public int MateCount { get; set; }
        public int ElitismCount { get; set; }
        public int MutationCount { get; set; }
        public double MinAcceptableFitness { get; set; } = 0.1;
        public List<ValuePair> Inputs { get; set; }

        public Entity Leader { get; private set; }

        private Entity[] population;
        private HeapGenerator heapGenerator;
        private Random gen;

        public GeneticProgramming(Random gen, int headsize)
        {
            heapGenerator = new HeapGenerator(headsize, gen);
            this.gen = gen;
        }

        public double Solve(int maxCycles)
        {
            CreateStarterPopulation();

            double bestFitness = double.MaxValue;
            for (int i = 0; i < maxCycles; i++)
            {
                for (int j = 0; j < Inputs.Count; j++)
                {
                    Evaluate(Inputs[j]);
                    DetermineFitness(Inputs[j].Output, ref bestFitness);

                    if (bestFitness <= MinAcceptableFitness)
                        return bestFitness;

                    var survivors = SelectSurvivals();
                    Reproduce(survivors);
                    Mutate();
                }
            }

            return bestFitness;
        }

        private void Mutate()
        {
            for (int i = 0; i < MutationCount; i++)
            {
                var roll = gen.Next(0, population.Length - 1);
                var mutation = heapGenerator.nodeGenerator.Generate();
                population[roll].Gene.ReplaceRandom(mutation, gen);
            }
        }

        private void Reproduce(List<Entity> survivors)
        {
            var start = survivors.Count;
            for (int i = start; i < PopSize; i++)
            {
                var roll = gen.Next(0, survivors.Count - 1);
                var parentA = survivors[roll];
                // make sure two seperate entities reproduce
                survivors.RemoveAt(roll);

                roll = gen.Next(0, survivors.Count - 1);
                var parentB = survivors[roll];

                var offspring = parentA.Cross(parentB, gen);

                population[i] = offspring;
                survivors.Add(parentA);
            }
        }

        private List<Entity> SelectSurvivals()
        {
            population = population.OrderByDescending(x => x.Fitness).ToArray();
            var luckyOnes = gen.Next(0, population.Length - ElitismCount - 1);
            var survivors = new Entity[ElitismCount + luckyOnes];
            Array.Copy(population, 0, survivors, 0, ElitismCount + luckyOnes);
            return survivors.ToList();
        }

        private void DetermineFitness(double expected, ref double bestFitness)
        {
            foreach (var entity in population)
            {
                var error = Math.Abs(expected - entity.Value);

                if (error < bestFitness)
                {
                    Leader = entity;
                    bestFitness = error;
                }
            }
        }

        private void Evaluate(ValuePair knownValue)
        {
            foreach (var entity in population)
            {
                entity.Gene.ApplyInputs(knownValue.Name, knownValue.Input);
                entity.Gene.ToValidTree(out Node root);
                entity.Value = root.CalculateValue();
            }
        }

        private void CreateStarterPopulation()
        {
            var heaps = heapGenerator.Generate(PopSize);
            population = new Entity[PopSize];

            for (int i = 0; i < population.Length; i++)
            {
                population[i].Gene = heaps[i];
            }
        }
    }
}