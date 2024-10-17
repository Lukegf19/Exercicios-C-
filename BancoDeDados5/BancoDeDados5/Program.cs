using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BancoDeDados5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-UPHNHIB;Database=estudos;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //incluirContato(connection);
                    //removerContatoPorId(connection);
                    //buscarContatoPorId(connection);
                    //buscarContatoPorEmail(connection);
                    //atualizarContato(connection);
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

        public static void menu()
        {
            Console.WriteLine("1- Incluir");
            Console.WriteLine("2- Remover (por id)");
            Console.WriteLine("2- Buscar (por id)");
            Console.WriteLine("3- Buscar (por email)");
            Console.WriteLine("4- Editar contato");
        }

        public static void incluirContato(SqlConnection connection)
        {
            Console.WriteLine("\nInforme o nome do contato: ");
            string nome = Console.ReadLine();

            Console.WriteLine("\nInforme o numero de telefone: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("\nInforme o e-mail do contato:");
            string email = Console.ReadLine();

            string sql = $"INSERT INTO contatos(nome,telefone,email) VALUES (@nome, @telefone, @email)";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@telefone", telefone);
                    command.Parameters.AddWithValue("@email", email);
                    int linhasAfetadas = command.ExecuteNonQuery();

                    Console.WriteLine($"Contato adicionado com sucesso. Linhas afetadas: {linhasAfetadas}");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        public static void removerContatoPorId(SqlConnection connection)
        {

            Console.WriteLine("\nInforme o id do contato que deseja remover: ");
            int id = int.Parse(Console.ReadLine());

            string sql = $"DELETE FROM Contatos WHERE id = @id";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@id", id);
                    int linhasAfetadas = command.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine("\nContato removido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine($"\nContado com id '{id}' não encontrado");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void buscarContatoPorId(SqlConnection connection)
        {
            Console.WriteLine("\nInforme o id do contato que você está procurando: ");
            int id = int.Parse(Console.ReadLine());

            string sql = $"SELECT nome, telefone, email FROM contatos WHERE id = @id";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nome = reader.GetString(0);
                            string telefone = reader.GetString(1);
                            string email = reader.GetString(2);

                            Console.WriteLine($"Nome: {nome}");
                            Console.WriteLine($"Telefone: {telefone}");
                            Console.WriteLine($"Email: {email}");
                        }
                        else
                        {
                            Console.WriteLine($"Contato com id '{id}' não encontrado");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void buscarContatoPorEmail(SqlConnection connection)
        {
            Console.WriteLine("\nInforme o e-mail do contato que está procurando:");
            string email = Console.ReadLine();

            string sql = $"SELECT nome, telefone, email FROM contatos WHERE email = @email";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nome = reader.GetString(0);
                            string telefone = reader.GetString(1);
                            string Email = reader.GetString(2);

                            Console.WriteLine($"Nome: {nome}");
                            Console.WriteLine($"Telefone: {telefone}");
                            Console.WriteLine($"Email: {email}");
                        }
                        else
                        {
                            Console.WriteLine($"Contato com id '{email}' não encontrado");
                        }
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void atualizarContato(SqlConnection connection)
        {
            Console.WriteLine("\nInforme o ID do contato que você deseja atualizar:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("\nQual dado você deseja atualizar:");
            Console.WriteLine("1- Nome");
            Console.WriteLine("2- E-mail");
            Console.WriteLine("3- Telefone");
            int opc = int.Parse(Console.ReadLine());

            string sql = "";

            try
            {

                switch (opc)
                {
                    case 1:

                        sql = $"UPDATE Contatos SET nome = @nome WHERE id=@id ";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            Console.WriteLine("\nInforme o nome que deseja atualizar:");

                            string nome = Console.ReadLine();

                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@nome", nome);

                            int linhasAfetadas = command.ExecuteNonQuery();

                            if (linhasAfetadas > 0)
                            {
                                Console.WriteLine($"\nNome atualizado com sucesso. Linhas afetadas: {linhasAfetadas}");
                            }
                            else
                            {
                                Console.WriteLine("Contato não encontrado");
                            }
                            break;
                        }

                    case 2:

                        Console.WriteLine("\nInforme o novo e-mail:");
                        string email = Console.ReadLine();

                        sql = $"UPDATE contatos SET email = @email WHERE id = @id";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@email", email);

                            int linhasAfetadas = command.ExecuteNonQuery();

                            if (linhasAfetadas > 0)
                            {
                                Console.WriteLine($"\nE-mail atualizado com sucesso: Linhas Afetadas: {linhasAfetadas}");
                            }
                            else
                            {
                                Console.WriteLine("\nContato não encontrado");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("\nInforme o novo telefone: ");
                        string telefone = Console.ReadLine();

                        sql = $"UPDATE contatos SET telefone = @telefone WHERE id = @id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@telefone",telefone);
                            int linhasAfetadas = command.ExecuteNonQuery();

                            if(linhasAfetadas > 0)
                            {
                                Console.WriteLine($"\nTelefone atualizado com sucesso. Linhas Afetadas: {linhasAfetadas}");
                            }
                            else
                            {
                                Console.WriteLine("\nContato não encontrado");
                            }
                        }
                        break;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
