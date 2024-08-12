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



////

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace bytebank_GeradorChavePix
//{
//    /// <summary>
//    /// classe que gera chaves pix usando formato guid.
//    /// </summary>
//    public static class GeradorPix
//    {
//        /// <summary>
//        /// Método que retorna uma chave aleatória de PIX.
//        /// </summary>
//        /// <returns>Retorna uma chave PIX no formato String.</returns>
//        public static string GetChavePix()
//        {
//            return Guid.NewGuid().ToString();
//        }
//        /// <summary>
//        ///  Método que retorna uma lista aleatória de
//        ///  chaves Pix.
//        /// </summary>
//        /// <param name="numeroChaves"> Quantidade de chaves
//        /// a serem geradas.</param>
//        /// <returns>Lista de strings de chaves Pix.</returns>

//        public static List<string> GetChavePix(int numeroChaves)
//        {
//            if (numeroChaves <= 0)
//            {
//                return null;

//            }
//            else
//            {
//                var chaves = new List<string>();
//                for (int i = 0; i < numeroChaves; i++)
//                {
//                    chaves.Add(Guid.NewGuid().ToString());
//                }
//                return chaves;
//            }

//        }

//    }
//}
