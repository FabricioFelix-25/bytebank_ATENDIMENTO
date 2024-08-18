using bytebank.Modelos.ADM.Funcionarios;
using bytebank_ATENDIMENTO.bytbank.Atendimento;
using bytebank_GeradorChavePix;
using bytebank_Modelos.bytebank.Modelos.ADM.Utilitario;



Console.WriteLine("Boas Vindas ao ByteBank, Atendimento.");
//new ByteBankAtendimento().AtendimentoCliente();

//public class Estagiario : Funcionario
//{
//    public Estagiario(double salario, string cpf) : base(salario, cpf)
//    {

//    }

//    public override void AumentarSalario()
//    {
//        throw new NotImplementedException();
//    }

//    protected override double getBonificacao()
//    {
//        throw new NotImplementedException();
//    }
//}

Console.WriteLine(GeradorPix.GetChavePix());

var listaDeChaves = GeradorPix.GetChavePix(10);

foreach (var chave in listaDeChaves)
{
    Console.WriteLine(chave);


}
