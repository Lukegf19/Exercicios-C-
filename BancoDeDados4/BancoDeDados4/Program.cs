using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados4
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
                    int opc = 0;
                    connection.Open();
                    do
                    {
                        menu();
                        opc = int.Parse(Console.ReadLine());

                        switch (opc)
                        {
                            case 1:
                                adicionarProduto(connection);
                                break;
                            case 2:
                                removerProduto(connection);
                                break;
                            case 3:
                                procurarPeloId(connection);
                                break;
                            case 4:
                                procurarPeloNome(connection);
                                break;
                            default:
                                Console.WriteLine("Até a proxima!");
                                break;

                        }
                    } while (opc != 9);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ReadLine();
                }
            }
        }

        public static void menu()
        {
            Console.WriteLine("\n1- Adicionar produto");
            Console.WriteLine("2- Remover produto");
            Console.WriteLine("3- Procurar produto pelo nome");
            Console.WriteLine("4- Procurar produto pelo ID");
            Console.WriteLine("5- Alterar produto");
            Console.WriteLine("9- Sair");
        }

        public static void adicionarProduto(SqlConnection connection)
        {
            Console.WriteLine("\nInforme o id do produto:");
            int id = int.Parse(Console.ReadLine());

            // Verificar se o ID já existe na tabela
            string verificaExistenciaSql = "SELECT COUNT(*) FROM produtos WHERE id = @id";
            bool idExiste = false;

            try
            {
                using (SqlCommand cmdVerifica = new SqlCommand(verificaExistenciaSql, connection))
                {
                    cmdVerifica.Parameters.AddWithValue("@id", id);
                    int count = (int)cmdVerifica.ExecuteScalar();
                    idExiste = (count > 0);
                }

                if (idExiste)
                {
                    Console.WriteLine("Já existe um produto com este ID na base de dados. Operação cancelada.");
                }
                else
                {
                    Console.WriteLine("Nome do produto:");
                    string nome = Console.ReadLine();

                    Console.WriteLine("Informe o valor do produto:");
                    decimal preco = decimal.Parse(Console.ReadLine());

                    string sql = "INSERT INTO produtos (id, nome, preco) VALUES (@id, @nome, @preco)";

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@nome", nome);
                            cmd.Parameters.AddWithValue("@preco", preco);

                            int linhasAfetadas = cmd.ExecuteNonQuery();
                            Console.WriteLine($"Produto cadastrado com sucesso, {linhasAfetadas} linhas afetadas.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro ao converter valor. Certifique-se de inserir um número válido para o ID.");
                Console.WriteLine(ex.Message);
            }
        }


        public static void removerProduto(SqlConnection connection)
        {
            Console.WriteLine("Informe o id do produto que deseja remover: ");
            int id = int.Parse(Console.ReadLine());

            string sql = $"DELETE FROM produtos WHERE id = @id";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Produto removido com sucesso!");
                }
                else
                {
                    Console.WriteLine("Produto não encontrado");
                }
            }
        }

        public static void procurarPeloId(SqlConnection connection)
        {

            Console.WriteLine("\nInforme o id do produto que está procurando: ");
            int id = int.Parse(Console.ReadLine());

            string sql = $"SELECT id, nome, preco FROM produtos WHERE id = @id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int ID = reader.GetInt32(0);
                            string nome = reader.GetString(1);
                            decimal preco = reader.GetDecimal(2);

                            Console.WriteLine($"\nId do produto: {id}");
                            Console.WriteLine($"Nome Produto: {nome}");
                            Console.WriteLine($"Preço Produto: R$ {preco}");
                        }
                        else
                        {
                            Console.WriteLine("\nProduto nao encontrado");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void procurarPeloNome(SqlConnection connection)
        {
            Console.WriteLine("Informe o nome do produto que esteja procurando: ");
            string nome = Console.ReadLine();

            string sql = $"SELECT nome, preco FROM produtos WHERE nome = @nome";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string Nome = reader.GetString(0);
                            decimal preco = reader.GetDecimal(1);

                            Console.WriteLine($"\n{nome} está custando R$ {preco}");
                        }
                        else
                        {
                            Console.WriteLine("\nProduto nao encontrado");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}