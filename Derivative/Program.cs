using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivative
{
    class Program
    {
        static bool CheckForPlusAndMinus(string term)
        {
            int index = 0;
            char[] operators = { '+', '-' };
            foreach (char charachter in operators)
            {
                index = term.IndexOf(charachter);
                if (index != -1)
                {
                    return true;
                }
            }
            return false;


        }

        static int VariableIndex(string term)
        {
            int index = 0;
            string newTerm = term.ToLower();
            char[] possibleVariables = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            foreach (char letter in possibleVariables)
            {
                index = newTerm.IndexOf(letter);
                if (index != -1)
                {
                    return index;
                }
            }
            return -1;

        }
        static int CarrotIndex(string term)
        {
            int index = 0;
            char carrot = '^';
            index = term.IndexOf(carrot);
            return index;

        }

        static List<string> SplitTerms(string derivIn)
        {
            List<string> splitTerms = new List<string>();
            splitTerms.AddRange(derivIn.Split(' '));
            return splitTerms;
        }

        static string Derivative(string derivIn)
        {
            List<string> splitTerms = SplitTerms(derivIn);
            for (int i = 0; i < splitTerms.Count; i++)
            {
                int variableIndex = VariableIndex(splitTerms[i]);
                int carrotIndex = CarrotIndex(splitTerms[i]);
                string coeficientString = "";
                double coeficientNumber = 0;
                string exponentString = "";
                double exponentNumber = 0;
                string newTerm = "";
                string newExponent = "";
                string newCoeficient = "";
                if (carrotIndex == -1 && !CheckForPlusAndMinus(splitTerms[i]) && variableIndex == -1)
                {
                    newTerm = "";
                    splitTerms[i] = newTerm;
                    continue;
                }
                else if (variableIndex != -1)
                {
                    for (int j = 0; j < variableIndex; j++)
                    {
                        coeficientString += splitTerms[i][j];
                    }
                    if (coeficientString.Length == 0)
                    {
                        coeficientString = "1";
                    }
                    if (carrotIndex != -1)
                    {
                        for (int j = carrotIndex + 1; j < splitTerms[i].Length; j++)
                        {
                            exponentString += splitTerms[i][j];
                        }
                        exponentNumber = double.Parse(exponentString);
                        newExponent = (exponentNumber - 1).ToString();
                        coeficientNumber = double.Parse(coeficientString);
                        newCoeficient = (coeficientNumber * exponentNumber).ToString();
                        if (newExponent == "1")
                        {
                            newTerm = newCoeficient + splitTerms[i][variableIndex];
                        }
                        else
                        {
                            newTerm = newCoeficient + splitTerms[i][variableIndex] + splitTerms[i][carrotIndex] + newExponent;
                        }

                    }
                    else
                    {
                        newTerm = coeficientString;
                    }
                    splitTerms[i] = newTerm;

                }
            }
            string derivative = "";
            if (splitTerms[splitTerms.Count - 1] == "")
            {
                for (int i = 0; i < splitTerms.Count - 2; i++)
                {
                    derivative += splitTerms[i];
                    derivative += " ";
                }
            }
            else
            {
                foreach (string term in splitTerms)
                {
                    derivative += term;
                    derivative += " ";
                }
            }
            return derivative;

        }
        static void Main(string[] args)
        {
            string function = Console.ReadLine();
            Console.WriteLine(Derivative(function));

            Console.ReadKey();
        }
    }
}
