using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<Pessoa> filaBanco = new Queue<Pessoa>();
            int idade = 0;
            int opc;

            do
            {
                menu();
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    enfileirar(filaBanco);
                }
                else if (opc == 2)
                {
                    desenfileirar(filaBanco);
                }
                else if (opc == 3)
                {
                    imprimirFila(filaBanco);
                }
                else if (opc == 4)
                {
                    esvaziarFila(filaBanco);
                }
                else if (opc == 5)
                {
                    quantasPessoas(filaBanco);
                }
                else if (opc == 6)
                {
                    quantasPessoasMaiorDeIdade(filaBanco);
                }
                else if (opc == 7)
                {
                    retornarQuemEMenorDeIdade(filaBanco);
                }
                else
                {
                    Console.WriteLine("Até a proxima");
                }

            } while (opc != 9);
            Console.ReadLine();
        }

        public static void menu()
        {

            Console.WriteLine("\nMenu de opções");
            Console.WriteLine("1-) Enfileirar");
            Console.WriteLine("2-) Desenfileirar");
            Console.WriteLine("3-) Imprimir fila");
            Console.WriteLine("4-) Ver quantas pessoas tem na fila");
            Console.WriteLine("5-) Limpar fila");
            Console.WriteLine("6-) Ver quantas pessoas sao maior de idade");
            Console.WriteLine("7-) Ver Nome de quem é menor de idade");
            Console.WriteLine("9-) sair");
            Console.WriteLine("Escolha a opção que deseja:");
        }

        public static void enfileirar(Queue<Pessoa> filaBanco)
        {
            Console.WriteLine("\nInforme o nome da pessoa");
            string nome = Console.ReadLine();

            Console.WriteLine("Informe a idade da pessoa");
            int idade = int.Parse(Console.ReadLine());

            Pessoa pessoa = new Pessoa
            {
                Nome = nome,
                Idade = idade
            };

            filaBanco.Enqueue(pessoa);
            Console.WriteLine($"Pronto: {nome} entrou na fila");
        }

        public static void desenfileirar(Queue<Pessoa> filaBanco)
        {

            if (filaBanco.Count == 0)
            {
                Console.WriteLine("Fila vazia");
            }
            else
            {
                Pessoa pessoaDesenfileirada = filaBanco.Dequeue();
                Console.WriteLine($"Pessoa '{pessoaDesenfileirada.Nome}' saiu da fila");

            }
        }

        public static void imprimirFila(Queue<Pessoa> filaBanco)
        {
            if (filaBanco.Count == 0)
            {
                Console.WriteLine("\nFila vazia");
            }
            else
            {
                foreach (var pessoa in filaBanco)
                {
                    Console.WriteLine($"\nNome: {pessoa.Nome},\nIdade: {pessoa.Idade} anos");
                }
            }
        }

        public static void esvaziarFila(Queue<Pessoa> filaBanco)
        {
            if (filaBanco.Count == 0)
            {
                Console.WriteLine("A fila já está vazia");
            }
            else
            {
                filaBanco.Clear();
                Console.WriteLine("Fila vazia");
            }

        }

        public static void quantasPessoas(Queue<Pessoa> filaBanco)
        {
            Console.WriteLine($"Tem {filaBanco.Count()} na fila");
        }

        public static void quantasPessoasMaiorDeIdade(Queue<Pessoa> filaBanco)
        {
            int maioridade = 0;

            if (filaBanco.Count == 0)
            {
                Console.WriteLine("A fila está vázia");
            }
            else
            {
                foreach (Pessoa pessoa in filaBanco)
                {
                    if (pessoa.Idade >= 18)
                    {
                        maioridade++;
                    }
                }
                Console.WriteLine($"\n{maioridade} pessoas que estão na fila são maior de idade");
            }
        }

        public static void retornarQuemEMenorDeIdade(Queue<Pessoa> filaBanco)
        {
            int menorDeIdade = 0;
            if (filaBanco.Count == 0)
            {
                Console.WriteLine("A fila está vazia");
            }
            else
            {
                foreach (Pessoa pessoa in filaBanco)
                {
                    if(pessoa.Idade < 18)
                    {
                        Console.WriteLine($"{pessoa.Nome} é menor de idade");
                    }
                }
            }
        }
    }
}