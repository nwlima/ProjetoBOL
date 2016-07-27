using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

//utilizando DataSet
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Usuario
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Usuario : System.Web.Services.WebService {

    public Usuario () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod(Description = "Retorna Dados de diversos Usuarios", EnableSession = false)]
    public DataSet RetornaUsuarios()
    {


        //cria a conexão com o banco de dados
        OleDbConnection aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\dados\BOL.mdb");

        //cria o objeto command and armazena a consulta SQL
        OleDbCommand aCommand = new OleDbCommand("select Nome, Email from Usuario", aConnection);


        DataSet custDS = new DataSet();

        OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(aCommand);

        aConnection.Open();
        myDataAdapter.Fill(custDS, "Usuario");

        aConnection.Close();


        return custDS;
    }


    [WebMethod(Description = "Retorna Dados de um Usuario", EnableSession = false)]
    public string RetornaUsuario(string nome)
    {


        //cria a conexão com o banco de dados
        OleDbConnection aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\dados\BOL.mdb");

        //cria o objeto command and armazena a consulta SQL
        OleDbCommand aCommand = new OleDbCommand("select Id, Nome, Email from Usuario Where Nome like '%" + nome + "%'", aConnection);

        try
        {
            aConnection.Open();
            //cria o objeto datareader para fazer a conexao com a tabela
            OleDbDataReader aReader = aCommand.ExecuteReader();

            //Faz a interação com o banco de dados lendo os dados da tabela
            while (aReader.Read())
            {

                nome = "Id: " + aReader.GetValue(0) + " / Nome: " + aReader.GetString(1) + " / Email: " + aReader.GetString(2);
            }
            //fecha o reader
            aReader.Close();
            //fecha a conexao 
            aConnection.Close();
        }
        //Trata a exceção
        catch (OleDbException e)
        {
            Console.WriteLine("Error: {0}", e.Errors[0].Message);
        }

        return nome;

    }


    [WebMethod(Description = "Inclui Dados de um Usuario", EnableSession = false)]
    public int IncluirUsuario(string nome, string email, string password)
    {

        OleDbDataAdapter adapter = new OleDbDataAdapter();

        //cria a conexão com o banco de dados
        OleDbConnection aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\dados\BOL.mdb");
        //cria o objeto command and armazena a consulta SQL
        OleDbCommand aCommand = new OleDbCommand("insert into Usuario (Nome, Email, Password)" + "values('" + nome + "','" + email + "'"+ "'," + password + "')", aConnection);


        try
        {

            aCommand.Parameters.Add(
             "Nome", OleDbType.Char, 20, "Nome");
            aCommand.Parameters.Add(
                "Email", OleDbType.VarChar, 40, "Email");
            aCommand.Parameters.Add(
                "Password", OleDbType.VarChar, 40, "Password");


            aConnection.Open();
            aCommand.CommandType = CommandType.Text;
            aCommand.ExecuteNonQuery();

            //fecha a conexao 
            aConnection.Close();
            aConnection.Dispose();
            aCommand.Dispose();


        }
        //Trata a exceção
        catch (OleDbException e)
        {
            Console.WriteLine("Error: {0}", e.Errors[0].Message);
            return 0;
        }

        return 1;

    }


    [WebMethod(Description = "excluir um Usuario", EnableSession = false)]
    public int ExcluirUsuario(int id)
    {

        OleDbDataAdapter adapter = new OleDbDataAdapter();

        //cria a conexão com o banco de dados
        OleDbConnection aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\dados\BOL.mdb");
        //cria o objeto command and armazena a consulta SQL
        OleDbCommand aCommand = new OleDbCommand("Delete from Usuario where Id in(" + id + ")", aConnection);


        try
        {

            aCommand.Parameters.Add(
             "Id", OleDbType.Integer);

            aConnection.Open();
            aCommand.CommandType = CommandType.Text;
            aCommand.ExecuteNonQuery();

            //fecha a conexao 
            aConnection.Close();
            aConnection.Dispose();
            aCommand.Dispose();


        }
        //Trata a exceção
        catch (OleDbException e)
        {
            Console.WriteLine("Error: {0}", e.Errors[0].Message);
            return 0;

        }


        return 1;

    }


    [WebMethod(Description = "altera dados de um Usuario", EnableSession = false)]
    public int AlterarUsuario(int id, string nome, string email)
    {

        OleDbDataAdapter adapter = new OleDbDataAdapter();

        //cria a conexão com o banco de dados
        OleDbConnection aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\dados\BOL.mdb");
        //cria o objeto command and armazena a consulta SQL
        OleDbCommand aCommand = new OleDbCommand("Update Usuario set Nome='" + nome + "',Email='" + email + "'" + "where Id in(" + id + ")", aConnection);


        try
        {

            aCommand.Parameters.Add(
             "Nome", OleDbType.Integer);
            aCommand.Parameters.Add(
             "Email", OleDbType.Integer);
            aCommand.Parameters.Add(
             "Id", OleDbType.Integer);


            aConnection.Open();
            aCommand.CommandType = CommandType.Text;
            aCommand.ExecuteNonQuery();

            //fecha a conexao 
            aConnection.Close();
            aConnection.Dispose();
            aCommand.Dispose();


        }
        //Trata a exceção
        catch (OleDbException e)
        {
            Console.WriteLine("Error: {0}", e.Errors[0].Message);
            return 0;

        }


        return 1;

    }



}
