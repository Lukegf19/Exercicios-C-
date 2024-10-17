using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoBiblioteca
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Livro> LivrosEmprestados { get; set; }

        public Usuario()
        {
            LivrosEmprestados = new List<Livro>();
        }

        public void PegarEmprestado(Livro livro)
        {
            LivrosEmprestados.Add(livro);
        }

        public void DevolverLivro(Livro livro)
        {
            LivrosEmprestados.Remove(livro);
        }
    }
}
