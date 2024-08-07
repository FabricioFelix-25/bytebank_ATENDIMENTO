
using bytebank_ATENDIMENTO.bytebank.Exceptions;
using bytebank.Modelos.Conta;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace bytebank_ATENDIMENTO.bytbank.Atendimento;

internal class ByteBankAtendimento
{
    
private List<ContaCorrente> _ListaDeContas = new List<ContaCorrente>()
{
    new ContaCorrente(95, "1234567-x") { Saldo = 100,Titular = new Cliente{Cpf = "11111",Nome ="fala"}},
    new ContaCorrente(95, "1234567-x") { Saldo = 1000,Titular = new Cliente{Cpf = "22222",Nome ="oi"} },
    new ContaCorrente(95, "1234567-x") { Saldo = 2200,Titular = new Cliente{Cpf = "33333",Nome ="falamansa"} },
};


public void AtendimentoCliente()
{

    try
    {
        char opcao = '0';
        while (opcao != '8')
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===       Atendimento       ===");
            Console.WriteLine("===1 - Cadastrar Conta      ===");
            Console.WriteLine("===2 - Listar Contas        ===");
            Console.WriteLine("===3 - Remover Conta        ===");
            Console.WriteLine("===4 - Ordenar Contas       ===");
            Console.WriteLine("===5 - Pesquisar Conta      ===");
            Console.WriteLine("===6 - Exportar Contas      ===");
            //Console.WriteLine("===7 - Exportar em XML      ===");
            Console.WriteLine("===8 - Sair do Sistema      ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n\n");
            Console.Write("Digite a opção desejada: ");
            try
            {
                opcao = Console.ReadLine()[0];
            }
            catch (Exception execao)
            {
                throw new ByteBankException(execao.Message);
            }
            
            switch (opcao)
            {
                case '1':
                    CadastrarConta();
                    break;
                case '2':
                    ListarContas();
                    break;
                case '3':
                    RemoverContas();
                    break;
                case '4':
                    OrdenarContas();
                    break;
                case '5':
                    PesquisarContas();
                    break;
                case '6':
                    ExportarContas();
                    break;
                 //case '7':
                 //   ExportarContasEmXML();
                 //   break;
                 case '8':
                     EncerrarAplicacao();
                     break;

                    default:
                    Console.WriteLine("Opcao não implementada.");
                    break;
            }
        }
    }
    catch (ByteBankException execao )
    {

        Console.WriteLine($"{execao.Message}");
        ;
    }
}

    //private void ExportarContasEmXML()
    //{
    //    Console.Clear();
    //    Console.WriteLine("===============================");
    //    Console.WriteLine("===     EXPORTAR CONTAS XML ===");
    //    Console.WriteLine("===============================");
    //    Console.WriteLine("\n");

    //    if (_ListaDeContas.Count <= 0)
    //    {
    //        Console.WriteLine("... Não existe dados para exportação...");
    //        Console.ReadKey();
    //    }
    //    else
    //    {
    //        //Serializar para XML
    //        var contasXML = new XmlSerializer(typeof(List<ContaCorrente>));

    //        try
    //        {
    //            FileStream fs = new FileStream(@"c:\temp\export\contas.xml", FileMode.Create);
    //            using (StreamWriter streamwriter = new StreamWriter(fs))
    //            {
    //                contasXML.Serialize(streamwriter, _ListaDeContas);
    //            }
    //            Console.WriteLine(@"Arquivo salvo em c:\temp\export\");
    //            Console.ReadKey();
    //        }
    //        catch (Exception excecao)
    //        {
    //            throw new ByteBankException(excecao.Message);
    //            Console.ReadKey();
    //        }

    //    }
    //}
    //pausa no projeto

    private void ExportarContas()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("===     EXPORTAR CONTAS     ===");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");

        if (_ListaDeContas.Count <= 0)
        {
            Console.WriteLine("... Não existe dados para exportação...");
            Console.ReadKey();
        }
        else
        {
            string json = JsonConvert.SerializeObject(_ListaDeContas, Formatting.Indented);
            try
            {
                FileStream fs = new FileStream(@"c:\tmp\export\contas.json",
                    FileMode.Create);
                using (StreamWriter streamwriter = new StreamWriter(fs))
                {
                    streamwriter.WriteLine(json);
                }
                Console.WriteLine(@"arquivo salvo em c:\tmp\export\");
                Console.ReadKey();
            }
            catch (Exception exececao)
            {
                throw new ByteBankException(exececao.Message);
                Console.ReadKey();
            }

        }
    }

    private void EncerrarAplicacao()
{
    Console.WriteLine(" .. encerrando aplicação ..");
    Console.ReadKey();
}


void PesquisarContas()
{
    Console.Clear();
    Console.WriteLine("===============================");
    Console.WriteLine("===    PESQUISAR CONTAS     ===");
    Console.WriteLine("===============================");
    Console.WriteLine("\n");
    Console.Write("Deseja pesquisar por (1) NUMERO DA CONTA ou (2)CPF TITULAR ou  (3) AGENCIA? ");
    switch (int.Parse(Console.ReadLine()))
    {
        case 1:
        {
            Console.Write("Informe o número da Conta: ");
            string _numeroConta = Console.ReadLine();
            ContaCorrente consultaConta = ConsultaPorNumeroConta(_numeroConta);
            Console.WriteLine(consultaConta.ToString());
            Console.ReadKey();
            break;
        }
        case 2:
        {
            Console.Write("Informe o CPF do Titular: ");
            string _cpf = Console.ReadLine();
            ContaCorrente consultaCpf = ConsultaPorCPFTitular(_cpf);
            Console.WriteLine(consultaCpf.ToString());
            Console.ReadKey();
            break;
        }
        case 3:
        {
            Console.Write("Informe o Nº da Agência: ");
            int _numeroAgencia = int.Parse(Console.ReadLine());
            var contasPorAgencia = ConsultaPorAgencia(_numeroAgencia);
            ExibirListaDeContas(contasPorAgencia);
            Console.ReadKey();
            break;
        }
        default:
            Console.WriteLine("Opção não implementada.");
            break;
    }

}


 void ExibirListaDeContas(List<ContaCorrente> contasPorAgencia)
{
    if (contasPorAgencia == null)
    {
        Console.WriteLine(" ... A consulta não retornou dados ...");
    }
    else
    {
        foreach (var item in contasPorAgencia)
        {
            Console.WriteLine(item.ToString());
        }
    }
}




  List<ContaCorrente> ConsultaPorAgencia(int numeroAgencia)
 {
     var consulta = (
         from conta in _ListaDeContas
         where conta.Numero_agencia == numeroAgencia
         select conta).ToList();
     return consulta;
 }

ContaCorrente ConsultaPorCPFTitular(string? cpf)
{
    // ContaCorrente conta = null;
    // for (int i = 0; i < _ListaDeContas.Count; i++)
    // {
    //     if (_ListaDeContas[i].Titular.Cpf.Equals(cpf))
    //     {
    //         conta = _ListaDeContas[i];
    //         
    //     }
    // }
//
    // return conta;
   return _ListaDeContas.Where(conta => conta.Titular.Cpf == cpf).FirstOrDefault();
}


ContaCorrente ConsultaPorNumeroConta(string? numeroConta)
{
    // ContaCorrente conta = null;
    // for (int i = 0; i < _ListaDeContas.Count; i++)
    // {
    //     if (_ListaDeContas[i].Conta.Equals(numeroConta))
    //     {
    //         conta = _ListaDeContas[i];
    //         
    //     }
    // }
    //
    // return conta;
    
    return _ListaDeContas.Where(conta => conta.Titular.Cpf == numeroConta).FirstOrDefault();
}


void OrdenarContas()
{
   _ListaDeContas.Sort();
   Console.WriteLine("... LISTA DE CONTAS ORDENADA ...");
   Console.ReadKey();
}


void RemoverContas()
{
    Console.Clear();
    Console.WriteLine("============================");
    Console.WriteLine("===   Remover Contas   =====");
    Console.WriteLine("============================");
    Console.WriteLine("\n");
    Console.Write("informe o numero da conta: ");
    string numeroConta = Console.ReadLine();
    ContaCorrente conta = null;
    foreach (var item in _ListaDeContas)
    {
        if (item.Conta.Equals(numeroConta))
        {
            conta = item;
            
        }
    }

    if (conta != null)
    {
        _ListaDeContas.Remove(conta);
        Console.WriteLine("... conta removida da lista ...");
        
        
    }
    else
    {
        Console.WriteLine(".. conta para remoção não encontrada ...");
    }

    Console.ReadKey();


}



void ListarContas()
{
    Console.Clear();
    Console.WriteLine("=======================");
    Console.WriteLine("=== lista de contas ===");
    Console.WriteLine("=======================");
    Console.WriteLine("\n");

    if (_ListaDeContas.Count <= 0)
    {
        Console.WriteLine("... nao ha contas cadastradas!");
        Console.ReadKey();
        return;
    }

    foreach (ContaCorrente item in _ListaDeContas)
    {
        // Console.WriteLine("=== Dados ====");
        // Console.WriteLine("Numero da conta: " + item.Conta);
        // Console.WriteLine("Saldo na conta: " + item.Saldo);
        // Console.WriteLine("Titular da conta: " + item.Conta);
        // Console.WriteLine("CPF: " + item.Conta);
        // Console.WriteLine("Profissao: " + item.Conta);
        Console.WriteLine(item.ToString());
        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        Console.ReadKey();
        

    }
}

void CadastrarConta()
{
    Console.Clear();
    Console.WriteLine("===============================");
    Console.WriteLine("===   CADASTRO DE CONTAS    ===");
    Console.WriteLine("===============================");
    Console.WriteLine("\n");
    Console.WriteLine("=== Informe dados da conta ===");
    Console.Write("Número da Agência: ");
    int numeroAgencia = int.Parse(Console.ReadLine());

    ContaCorrente conta = new ContaCorrente(numeroAgencia);
    Console.WriteLine($"numero da conta [ nova] : {conta.Conta}");
    Console.Write("Informe o saldo inicial: ");
    conta.Saldo = double.Parse(Console.ReadLine());

    Console.Write("Infome nome do Titular: ");
    conta.Titular.Nome = Console.ReadLine();

    Console.Write("Infome CPF do Titular: ");
    conta.Titular.Cpf = Console.ReadLine();

    Console.Write("Infome Profissão do Titular: ");
    conta.Titular.Profissao = Console.ReadLine();

    _ListaDeContas.Add(conta);
    
    
    Console.WriteLine("... Conta cadastrada com sucesso! ...");
    Console.ReadKey();
}
    

}