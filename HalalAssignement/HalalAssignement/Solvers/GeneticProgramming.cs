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
        public double MinAcceptableFitness { get; set; } = 10;
        public List<ValuePair> Inputs { get; set; }

        public Entity Leader { get; private set; }

        private Entity[] population;
        private HeapGenerator heapGenerator;
        private Random gen;

        public GeneticProgramming(Random gen, int headsize, string[] possibleInputVarNames)
        {
            heapGenerator = new HeapGenerator(headsize, possibleInputVarNames, gen);
            this.gen = gen;
        }

        public double Solve(int maxCycles)
        {
            CreateStarterPopulation();

            double bestFitness = double.MaxValue;
            for (int i = 0; i < maxCycles; i++)
            {
                ResetFitness();

                for (int j = 0; j < Inputs.Count; j++)
                {
                    Evaluate(Inputs[j]);
                    DetemineFitness(Inputs[j].Output);
                }

                bestFitness = DetermineBestIndividualFitness();
                Console.WriteLine($"{i}: lowest avg error: {bestFitness} from gene:");
                Console.WriteLine($"\t head: {Leader.ToString()}");

                if (bestFitness <= MinAcceptableFitness)
                    return bestFitness;

                //var survivors = SelectSurvivors();
                var survivors = SelectMates();
                var elites = GetElites();
                survivors.AddRange(elites);
                Reproduce(survivors);
                Mutate();
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

        private Entity[] Reproduce(List<Entity> succesfuls)
        {
            var start = succesfuls.Count;
            var newPopulation = new List<Entity>(succesfuls);
            for (int i = start; i < PopSize; i++)
            {
                var roll = gen.Next(0, succesfuls.Count - 1);
                var parentA = succesfuls[roll];

                roll = gen.Next(0, succesfuls.Count - 1);
                var parentB = succesfuls[roll];

                var offspring = parentA.Cross(parentB, gen);

                newPopulation.Add(offspring);
            }
            return newPopulation.ToArray();
        }

        private List<Entity> GetElites()
        {
            population = population.OrderBy(x => x.Fitness).ToArray();
            //var luckyOnes = gen.Next(0, population.Length - ElitismCount - 1 / 2);
            var elites = new Entity[ElitismCount];
            Array.Copy(population, 0, elites, 0, ElitismCount);
            return elites.ToList();
        }

        private List<Entity> SelectMates()
        {
            var possibleMates = new Entity[MateCount];
            for (int i = 0; i < possibleMates.Length; i++)
            {
                var roll = gen.Next(0, population.Length - 1);
                possibleMates[i] = population[roll];
            }

            var mates = new List<Entity>();
            for (int i = 0; i < possibleMates.Length; i++)
            {
                for (int j = 0; j < possibleMates.Length; j++)
                {
                    if (possibleMates[i].Fitness < possibleMates[j].Fitness)
                        mates.Add(possibleMates[i]);
                    else
                        mates.Add(possibleMates[j]);
                }
            }

            return mates;
        }

        private void DetemineFitness(double expected)
        {
            foreach (var entity in population)
            {
                var error = Math.Abs(expected - entity.Value);
                entity.Fitness += error;
            }
        }

        private void ResetFitness()
        {
            foreach (var entity in population)
            {
                entity.Fitness = 0;
            }
        }

        private double DetermineBestIndividualFitness()
        {
            var bestFitness = double.MaxValue;
            foreach (var entity in population)
            {
                entity.Fitness = entity.Fitness / Inputs.Count;
                if (entity.Fitness < bestFitness)
                {
                    Leader = entity;
                    bestFitness = entity.Fitness;
                }
            }

            return bestFitness;
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
                population[i] = new Entity()
                {
                    Gene = heaps[i]
                };
            }
        }
    }
}