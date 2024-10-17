using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bancoDeDados3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-UPHNHIB;Database=estudos;Trusted_Connection=True;";
            int opc = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    do
                    {

                        menu();
                        opc = int.Parse(Console.ReadLine());

                        switch (opc)
                        {
                            case 1:
                                matricularAluno(connection);
                                break;
                            case 2:
                                procurarAlunoPorRa(connection);
                                break;
                            case 3:
                                desmatricularAlunoPorRa(connection);
                                break;
                            case 4:
                                atualizarAluno(connection);
                                break;
                            default:
                                Console.WriteLine("Até a proxima");
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
                    connection.Close();
                    Console.ReadLine();
                }
            }
        }

        //Função Menu
        public static void menu()
        {
            Console.WriteLine();
            Console.WriteLine("1- Matricular aluno");
            Console.WriteLine("2- Buscar aluno (Por RA)");
            Console.WriteLine("3- Desmatricular aluno (Busca por RA)");
            Console.WriteLine("4- Atualizar aluno (Busca por RA)");
            Console.WriteLine("9- Sair");
        }

        //Função para matricular o aluno
        public static void matricularAluno(SqlConnection connection)
        {
            try
            {
                //Obter dados do aluno
                Console.WriteLine("\nInforme o Nome do aluno");
                string nome = Console.ReadLine();

                Console.WriteLine("Informe o RA do aluno");
                string ra = Console.ReadLine();

                Console.WriteLine("Informe a idade do aluno");
                int idade = int.Parse(Console.ReadLine());

                Console.WriteLine("Informe qual curso o aluno está matriculado");
                string curso = Console.ReadLine();

                //Comando sql para inserir os dados
                string sql = $"INSERT INTO ALUNOS (ra,nome,idade,curso) VALUES ( '{ra}' , '{nome}' , '{idade}' , '{curso}' )";


                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    Console.WriteLine("Aluno matriculado com sucesso!");
                    Console.WriteLine($"Linhas afetadas: {linhasAfetadas}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Função para desmatricular o aluno
        public static void desmatricularAlunoPorRa(SqlConnection connection)
        {
            try
            {
                //Obter o RA que deseja desmatricular o aluno
                Console.WriteLine("\nInforme o RA do aluno que deseja desmatricular");
                string ra = Console.ReadLine();

                //Comando SQL para remover o aluno do Banco de Dados
                string sql = $"DELETE FROM ALUNOS where ra = @ra";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ra", ra);
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine($"Aluno desmatriculado com sucesso, {linhasAfetadas} linhas afetadas");
                    }
                    else
                    {
                        Console.WriteLine("ALuno não encontrado!");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Função para buscar dados do aluno pelo RA
        public static void procurarAlunoPorRa(SqlConnection connection)
        {
            try
            {
                //Obter o RA do aluno que está procurando
                Console.WriteLine("\nInforme o RA do aluno que você está procurando");
                string ra = Console.ReadLine();

                //Comando SQL para buscar no banco de dados
                string sql = $"SELECT ra,nome,idade,curso FROM alunos WHERE ra = @ra ";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ra", ra);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string RA = reader.GetString(0);
                            string nome = reader.GetString(1);
                            int idade = reader.GetInt32(2);
                            string curso = reader.GetString(3);

                            //imprimir os dados caso o aluno seja encontrado
                            Console.WriteLine($"\nRA: {RA}");
                            Console.WriteLine($"Nome: {nome}");
                            Console.WriteLine($"Idade: {idade} anos");
                            Console.WriteLine($"curso: {curso}");
                        }
                        else
                        {
                            //Mensagem caso não seja encontrado
                            Console.WriteLine("Aluno não encontrado");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Função para atualizar algum dado do aluno
        public static void atualizarAluno(SqlConnection connection)
        {
            try
            {
                Console.WriteLine("Informe o RA do aluno que você deseja atualizar: ");
                string ra = Console.ReadLine();

                Console.WriteLine("Qual informação você deseja atualizar: ");
                Console.WriteLine("1- Nome");
                Console.WriteLine("2- Idade");
                Console.WriteLine("3- Curso");
                int opc = int.Parse(Console.ReadLine());

                string sqlUpdate = "";

                switch (opc)
                {
                    case 1:
                        Console.WriteLine("Qual nome você deseja colocar: ");
                        string nome = Console.ReadLine();

                        sqlUpdate = $"UPDATE alunos SET nome = @nome where ra = @ra";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, connection))
                        {
                            cmd.Parameters.AddWithValue("@nome", nome);
                            cmd.Parameters.AddWithValue("@ra", ra);
                            cmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Nome atualizado com sucesso!");
                        break;

                    case 2:
                        Console.WriteLine("Qual idade você deseja colocar: ");
                        int idade = int.Parse(Console.ReadLine());

                        sqlUpdate = $"UPDATE alunos SET idade = @idade where ra = @ra";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, connection))
                        {
                            cmd.Parameters.AddWithValue("@idade", idade);
                            cmd.Parameters.AddWithValue("@ra", ra);
                            cmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Idade atualizada com sucesso!");
                        break;

                    case 3:
                        Console.WriteLine("Qual curso você deseja colocar: ");
                        string curso = Console.ReadLine();

                        sqlUpdate = $"UPDATE alunos SET curso = @curso where ra = @ra";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, connection))
                        {
                            cmd.Parameters.AddWithValue("@curso", curso);
                            cmd.Parameters.AddWithValue("@ra", ra);
                            cmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Curso atualizado com sucesso!");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao tentar atualizar os dados: " + ex.Message);
            }
        }



    }
}