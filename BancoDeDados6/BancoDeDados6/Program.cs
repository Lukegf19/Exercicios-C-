using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-UPHNHIB\\SQLEXPRESS;Database=estudos;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    adicionar(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                    Console.ReadLine();
                }
            }

        }
        static void adicionar(SqlConnection connection)
        {
            Console.WriteLine("Informe o nome do contato");
            string nome = Console.ReadLine();

            Console.WriteLine("Informe o numero do contato");
            string numero = Console.ReadLine();

            Console.WriteLine("Informe o e-mail do contato");
            string email = Console.ReadLine();

            string sql = $"insert into contatos (nome,telefone,email) values (@nome,@numero,@email)";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.Parameters.AddWithValue("@email", email);

                    int linhasAfetadas = command.ExecuteNonQuery();
                    Console.WriteLine($"Contato adicionado com sucesso. Linhas afetadas: {linhasAfetadas}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void remover(SqlConnection connection)
        {
            Console.WriteLine("Qual contato você quer remover");
            string nome = Console.ReadLine();

            string sql = $"Delete from contatos where nome = @nome";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@nome", nome);

                int linhasAfetadas = command.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine($"Contato excluido com sucesso. Linhas afetadas : {linhasAfetadas}");
                }
                else
                {
                    Console.WriteLine("Contato nao encontrado");
                }
            }
        }

        static void buscar(SqlConnection connection)
        {
            Console.WriteLine("Informe o nome do cliente que está procurado: ");
            string nome = Console.ReadLine();

            string sql = $"select nome, telefone, email from contatos where nome = @nome";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@nome", nome);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string Nome = reader.GetString(0);
                        string telefone = reader.GetString(1);
                        string email = reader.GetString(2);

                        Console.WriteLine($"Nome: {Nome}");
                        Console.WriteLine($"Telefone: {telefone}");
                        Console.WriteLine($"Email; {email}");
                    }
                    else
                    {
                        Console.WriteLine("Contato nao encontrado");
                    }
                }
            }
        }
    }
}