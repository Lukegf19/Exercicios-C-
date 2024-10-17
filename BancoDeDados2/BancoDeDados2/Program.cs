using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados2
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
                    int opc = 0;

                    do
                    {
                        menu();
                        opc = int.Parse(Console.ReadLine());

                        if (opc == 1)
                        {
                            AdicionarCliente(connection);
                        }
                        else if (opc == 2)
                        {
                            removerPorId(connection);
                        }
                        else if (opc == 3)
                        {
                            procurarPorId(connection);
                        }

                    } while (opc != 6);
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
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1. Adicionar cliente");
            Console.WriteLine("2. Remover cliente por id");
            Console.WriteLine("3. Procurar cliente por ID");
            Console.WriteLine("4. Sair");
            Console.WriteLine("Qual opção deseja escolher: ");
        }



        public static void AdicionarCliente(SqlConnection connection)
        {
            try
            {
                Console.WriteLine("Informe o nome do cliente: ");
                string nome = Console.ReadLine();

                Console.WriteLine("Informe o sobrenome do cliente: ");
                string sobrenome = Console.ReadLine();

                Console.WriteLine("Informe o e-mail do cliente: ");
                string email = Console.ReadLine();

                Console.WriteLine("Informe o endereço do cliente: ");
                string endereco = Console.ReadLine();

                //Comando Sql
                string sql = $"insert into clientes (nome, sobrenome, email, endereco) values ('{nome}', '{sobrenome}', '{email}','{endereco}')";

                //Criar objeto SQL
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Cliente adicionado com sucesso. Linhas afetadas: {rowsAffected}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void removerPorId(SqlConnection connection)
        {
            try
            {
                Console.WriteLine("Informe o id do cliente que deseja remover:");
                string id = Console.ReadLine();

                string sql = $"Delete from clientes where id = {id}";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine("CLiente removido com sucesso: ");
                    Console.WriteLine($"Linhas afetadas: {rowsAffected}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void procurarPorId(SqlConnection connection)
        {
            try
            {
                Console.WriteLine("\nInforme o id do cliente que está procurando:");
                string id = Console.ReadLine();

                string sql = $"select nome, sobrenome, email, endereco from clientes where id = {id}";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nome = reader.GetString(0);
                            string sobrenome = reader.GetString(1);
                            string email = reader.GetString(2);
                            string endereco = reader.GetString(3);
                
                            Console.WriteLine($"\nNome: {nome} {sobrenome}");
                            Console.WriteLine($"Email: {email}");
                            Console.WriteLine($"Endereço: {endereco}");
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}