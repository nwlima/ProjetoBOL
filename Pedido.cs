using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

//utilizando OLEDB
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;



/// <summary>
/// Summary description for Pedido
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Pedido : System.Web.Services.WebService
{

    public Pedido()
    {



    }

    // Operações SOA : Montar_Pedido  
    // Modelo : Sempre B (Bikes), Quantidade variada, ValorFinal;

    string BD = System.Configuration.ConfigurationSettings.AppSettings["BD"];

    [WebMethod(Description = "Efetua o registro do pedido e retorna o numero do pedido", EnableSession = false)]
    public string Montar_Pedido(string Modelo, int Quantidade, int ValorFinal, int Usuario) {

        string auxPedido = "";

        //Verifica se é Bike para incluir o pedido
        if (Modelo != "B") {       
            return "Modelo Invalido";
        }

        //Inclui na tabela de Pedido
        else { 
        
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            //cria a conexão com o banco de dados
            OleDbConnection aConnection = new OleDbConnection(@BD);
            //cria o objeto command and armazena a consulta SQL
            OleDbCommand aCommand = new OleDbCommand("insert into Pedido (Modelo, Quantidade, ValorFinal, Usuario)" + "values('" + Modelo + "'," + Quantidade +","+ValorFinal+","+ Usuario + ")", aConnection);

            try
                    {

                        aCommand.Parameters.Add(
                         "Modelo", OleDbType.Char, 1, "Modelo");

                        aCommand.Parameters.Add(
                        "Quantidade", OleDbType.Double);

                        aCommand.Parameters.Add(
                        "ValorFinal", OleDbType.Double);

                        aCommand.Parameters.Add(
                         "Usuario", OleDbType.Double);


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
                        return "NOKPedido";
                    }
            // Retorna o numero do pedido
            //cria a conexão com o banco de dados
            OleDbConnection aConnection2 = new OleDbConnection(@BD);
            //cria o objeto command and armazena a consulta SQL
            OleDbCommand aCommand2 = new OleDbCommand("select max(Id) from Pedido", aConnection2);

            try
            {
                aConnection2.Open();
                //cria o objeto datareader para fazer a conexao com a tabela
                OleDbDataReader aReader = aCommand2.ExecuteReader();

                //Faz a interação com o banco de dados lendo os dados da tabela
                while (aReader.Read())
                {
                    auxPedido = "Cod. Pedido = " + aReader.GetValue(0);
                }
                //fecha o reader
                aReader.Close();
                //fecha a conexao 
                aConnection2.Close();
                aConnection2.Dispose();
                aCommand2.Dispose();
            }
            //Trata a exceção
            catch (OleDbException e)
            {
                Console.WriteLine("Error: {0}", e.Errors[0].Message);
                return "NOKPedido";
            }

        {

            //Verifica se existe estoque disponivel e retorna a quantidade
            string aux1;
            int aux2;

            Estoque x = new Estoque();
            aux1 = x.Verificar_Estoque("B", Quantidade);

            if (aux1 == "NOKEstoque") {
                return "NOKPedido";
            }
            else {
                aux2 = x.Verificar_QuantidadeEstoque("B", Quantidade);
            }

            //Efetua a Baixa no Estoque
            if (aux2 > 0)
                x.EfetuaBaixa("B", aux2 - Quantidade);

            {








            }
 
            //Final do Serviço
            return auxPedido;


        }

       
    }
    
}
}
