using System;

namespace GerenciamentoBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Criando livros
            Livro livro1 = new Livro { Titulo = "Aprendendo C#", Autor = "Camara", AnoPublicacao = 2023 };
            Livro livro2 = new Livro { Titulo = "POO para iniciantes", Autor = "Cesar", AnoPublicacao = 2022 };
            Livro livro3 = new Livro { Titulo = "Aprendendo Java", Autor = "Clayton", AnoPublicacao = 2023 };
            Livro livro4 = new Livro { Titulo = "Aprendendo Kotlin", Autor = "Clayton", AnoPublicacao = 2023 };
            Livro livro5 = new Livro { Titulo = "Aprendendo Golang", Autor = "Fernando", AnoPublicacao = 2023 };
            Livro livro6 = new Livro { Titulo = "Aprendendo Banco de dados", Autor = "Saito", AnoPublicacao = 2023 };

            // Criando usuários
            Usuario usuario1 = new Usuario { Nome = "Lucas", Id = 1 };
            Usuario usuario2 = new Usuario { Nome = "João", Id = 2 };
            Usuario usuario3 = new Usuario { Nome = "Maria", Id = 3 };

            // Criando biblioteca
            Biblioteca biblioteca = new Biblioteca();

            // Adicionando livros à biblioteca
            biblioteca.AdicionarLivro(livro1);
            biblioteca.AdicionarLivro(livro2);
            biblioteca.AdicionarLivro(livro3);
            biblioteca.AdicionarLivro(livro4);
            biblioteca.AdicionarLivro(livro5);
            biblioteca.AdicionarLivro(livro6);

            // Realizando empréstimos
            biblioteca.EmprestarLivro(livro1, usuario2);
            biblioteca.EmprestarLivro(livro2, usuario1);
            biblioteca.EmprestarLivro(livro3, usuario3);

            // Exibindo livros emprestados
            Console.WriteLine("Livros Emprestados:");
            Console.WriteLine($"O livro '{livro1.Titulo}' foi emprestado para {usuario2.Nome}");
            Console.WriteLine($"O livro '{livro2.Titulo}' foi emprestado para {usuario1.Nome}");
            Console.WriteLine($"O livro '{livro3.Titulo}' foi emprestado para {usuario3.Nome}");

            // Exibindo livros disponíveis
            Console.WriteLine("\nLivros Disponíveis:");
            foreach (var livro in biblioteca.LivrosDisponiveis)
            {
                Console.WriteLine($"Título: {livro.Titulo}, Autor: {livro.Autor}, Ano de Publicação: {livro.AnoPublicacao}");
            }

            // Consultando livros por autor utilizando LINQ
            string autorConsultado = "Clayton";
            Console.WriteLine($"\nLivros do autor '{autorConsultado}':");
            var livrosDoAutor = biblioteca.ConsultarLivrosPorAutor(autorConsultado);
            foreach (var livro in livrosDoAutor)
            {
                Console.WriteLine($"Título: '{livro.Titulo}', Autor: {livro.Autor}, Ano de Publicação: {livro.AnoPublicacao}");
            }

            Console.ReadLine();
        }
    }
}
