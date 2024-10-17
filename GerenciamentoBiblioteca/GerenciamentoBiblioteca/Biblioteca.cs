using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoBiblioteca
{
    public class Biblioteca
    {
        public List<Livro> LivrosDisponiveis { get; set; }
        public List<Livro> LivrosEmprestados { get; set; }
        
        public Biblioteca() 
        {
            LivrosDisponiveis = new List<Livro>();
            LivrosEmprestados = new List<Livro>();
        }

        public void AdicionarLivro(Livro livro)
        {
            LivrosDisponiveis.Add(livro);
        }

        public void EmprestarLivro(Livro livro, Usuario usuario)
        {
            LivrosDisponiveis.Remove(livro);
            LivrosEmprestados.Add(livro);
            usuario.PegarEmprestado(livro);
        }

        public void DevolverLivro(Livro livro, Usuario usuario)
        {
            LivrosEmprestados.Remove(livro);
            LivrosDisponiveis.Add(livro);
            usuario.DevolverLivro(livro);
        }

        public List<Livro> ConsultarLivrosPorAutor(string autor)
        {
            return LivrosDisponiveis.Concat(LivrosEmprestados)
                                   .Where(livro => livro.Autor.Equals(autor, StringComparison.OrdinalIgnoreCase))
                                   .ToList();
        }
    }
}
