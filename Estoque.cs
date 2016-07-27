using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

///utilizando OLEDB
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;



/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

 

public class Estoque : System.Web.Services.WebService {

    public Estoque () {


        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    // Operações SOA Encapsuladas neste Metodo : Verificar_Estoque; Verificar_Quadros; Verificar_Pneus; Verificar_OutrasPecas 
    // Tipo pode ser : B=Bikes;Q=Quadros;P=Pneus;O=Outras Peças
    // Quantidade : Quantidade total a ser verificada a disponibilidade no Estoque

    string BD = System.Configuration.ConfigurationSettings.AppSettings["BD"];



    [WebMethod(Description = "Verifica  a quantidade de produtos disponiveis - Tipo pode ser : B=Bikes;Q=Quadros;P=Pneus;O=Outras Peças", EnableSession = false)]
    public string Verificar_Estoque(string Tipo, int Quantidade) {


        if (Tipo != "B" && Tipo != "Q" & Tipo != "P" & Tipo != "O")
        {
            return "Tipo Invalido!";
        }
        else
        {

            int aux = Quantidade;

                    //cria a conexão com o banco de dados
                    OleDbConnection aConnection = new OleDbConnection(@BD);

                    //cria o objeto command and armazena a consulta SQL
                    OleDbCommand aCommand = new OleDbCommand("select Qtd_Peca from Estoque where Cod_Peca = '" + Tipo + "'", aConnection);


                    try
                    {
                        aConnection.Open();
                        //cria o objeto datareader para fazer a conexao com a tabela
                        OleDbDataReader aReader = aCommand.ExecuteReader();

                        //Faz a interação com o banco de dados lendo os dados da tabela
                        while (aReader.Read())
                        {
                            Quantidade = aReader.GetInt32(0);
                        }
                        //fecha o reader
                        aReader.Close();
                        //fecha a conexao 
                        aConnection.Close();
                    }
                    //Trata a exceção
                    catch (OleDbException e)
                    {
                        return "Error SQL Statement: {0}" + e.Errors[0].Message;
            
                    }

                    if (Quantidade >= aux)
                        return "OKEstoque";
                    else
                        return "NOKEstoque";
    }

    }


    [WebMethod(Description = "Verifica  a quantidade de produtos disponiveis - Tipo pode ser : B=Bikes;Q=Quadros;P=Pneus;O=Outras Peças", EnableSession = false)]
    public int Verificar_QuantidadeEstoque(string Tipo, int Quantidade)
    {


        int aux;

        if (Tipo != "B" && Tipo != "Q" & Tipo != "P" & Tipo != "O")
        {
            return 0;
        }
        else
        {

            //cria a conexão com o banco de dados
            OleDbConnection aConnection = new OleDbConnection(@BD);

            //cria o objeto command and armazena a consulta SQL
            OleDbCommand aCommand = new OleDbCommand("select Qtd_Peca from Estoque where Cod_Peca = '" + Tipo + "'", aConnection);


            try
            {
                aConnection.Open();
                //cria o objeto datareader para fazer a conexao com a tabela
                OleDbDataReader aReader = aCommand.ExecuteReader();

                //Faz a interação com o banco de dados lendo os dados da tabela
                while (aReader.Read())
                {
                    aux = aReader.GetInt32(0);
                    Quantidade = aux;
                }
                //fecha o reader
                aReader.Close();
                //fecha a conexao 
                aConnection.Close();
            }
            //Trata a exceção
            catch (OleDbException e)
            {
                return 0;

            }

            return Quantidade;
        
        }

    }


    [WebMethod(Description = "altera dados de um Usuario", EnableSession = false)]
    public int EfetuaBaixa(string Tipo, int Quantidade)
    {

        OleDbDataAdapter adapter = new OleDbDataAdapter();

        //cria a conexão com o banco de dados
        OleDbConnection aConnection = new OleDbConnection(@BD);
        //cria o objeto command and armazena a consulta SQL
        OleDbCommand aCommand = new OleDbCommand("Update Estoque set Qtd_Peca= " + Quantidade + " where Cod_Peca in('" + Tipo + "')", aConnection);


        try
        {

            aCommand.Parameters.Add(
             "Qtd_Peca", OleDbType.Integer);
            aCommand.Parameters.Add(
             "Cod_Peca", OleDbType.Integer);


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
