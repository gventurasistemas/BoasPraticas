using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BoasPraticas_01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region STRINGS
            Console.WriteLine("String Jeito Rápido");
            JeitoRapidoStringBuilder();
            Console.WriteLine("String Jeito Demorado");
            JeitoDemoradoString();

            Console.WriteLine("*********************************");
            Console.WriteLine("");

            Console.WriteLine("Comparando string Má Prática");
            CompararStringJeitoMaPratica();
            Console.WriteLine("Comparando string Boa Prática");
            CompararStringJeitoBoaPratica();

            Console.WriteLine("*********************************");
            Console.WriteLine("");

            Console.WriteLine("Usando o  isNull e também o ponto de interrogação");
            IsNullJeitoBoaPratica();

            Console.WriteLine("*********************************");
            Console.WriteLine("");
            #endregion STRINGS


            #region COLLECTIONS

            Console.WriteLine("ForEach Má Prática");
            CollectionsForEachMaPratica();
            Console.WriteLine("ForEach boa Prática (AddRange)");
            CollectionsForEachBoaPratica();

            Console.WriteLine("*********************************");
            Console.WriteLine("");

            Console.WriteLine("Carregando uma lista Má Prática");
            CarregaProdutosMaPratica();
            Console.WriteLine("Carregando uma lista boa Prática, usando o IEnumerable");
            CarregaProdutosBoaPratica();

            Console.WriteLine("*********************************");
            Console.WriteLine("");

            Console.WriteLine("Contando itens com condição Má prática (usando count)");
            CountMaPratica();
            Console.WriteLine("Contando itens com condição boa prática (usando any)");
            CountAnyBoaPratica();

            Console.WriteLine("*********************************");
            Console.WriteLine("");

            #endregion COLLECTIONS

            ArrayInteriros();
            Console.WriteLine("*********************************");
            Console.WriteLine("");

            ValidaProdutosAtivos();
            Console.WriteLine("*********************************");
            Console.WriteLine("");
            

            SyntacticSugar();
            Console.WriteLine("*********************************");
            Console.WriteLine("");

            SyntacticSugar2();
            Console.WriteLine("*********************************");
            Console.WriteLine("");

            Console.ReadKey();
        }


        #region Strings
        static void JeitoDemoradoString()
        {
            var start = DateTime.Now;
            var content = "comeco";
            for (int i = 0; i < 10000; i++)
                content += "conteudo_" + 1;

            var end = DateTime.Now;
            var total = (end - start).TotalMilliseconds;
            Console.WriteLine(total);
            //Console.ReadKey();
        }
        static void JeitoRapidoStringBuilder()
        {
            var start = DateTime.Now;
            var builder = new StringBuilder("comeco");
            //for (int i = 0; i < 10000; i++)
            //    builder.Append("conteudo_" + i);

            for (int i = 0; i < 10000; i++)
                builder.AppendFormat($"conteudo_{i}");

            var end = DateTime.Now;
            var total = (end - start).TotalMilliseconds;
            Console.WriteLine(total);
            //Console.ReadKey();
        }
        static void CompararStringJeitoMaPratica()
        {
            var start = DateTime.Now;

            for (int i = 0; i < 100000; i++)
            {
                var name = "Rodolfo";
                var nameCompare = "rodolfo";
                var result = name.ToLower() == nameCompare;
            }
            var end = DateTime.Now;
            var total = (end - start).TotalMilliseconds;

            Console.WriteLine(total);
        }
        static void CompararStringJeitoBoaPratica()
        {
            var start = DateTime.Now;

            for (int i = 0; i < 100000; i++)
            {
                var name = "Rodolfo";
                var nameCompare = "rodolfo";
                var result = string.Equals(name, nameCompare, StringComparison.InvariantCultureIgnoreCase);
            }
            var end = DateTime.Now;
            var total = (end - start).TotalMilliseconds;

            Console.WriteLine(total);


        }
        static void IsNullJeitoBoaPratica()
        {
            var name = "Pedro";
            name = null;

            var result = name?.Split('o');//Se for nulo, não vai atribuir a variável
            var lenght = name?.Length;//Se for nulo, não vai atribuir a variável           

            TesteModel testeModel;
            testeModel = null;
            if (testeModel?.NumeroTeste == 2)//Se testeModel for diferente de nulo e testeModel.NumeroTeste igual a 2
                Console.WriteLine("");
            else
                Console.WriteLine("testeModel é nulo");

            //////////////////////////////////////////////////////////////////////
            testeModel = new TesteModel();
            testeModel.NumeroTeste = 2;
            if (testeModel?.NumeroTeste == 2) //Se testeModel for diferente de nulo e testeModel.NumeroTeste igual a 2
                Console.WriteLine("NumeroTeste é igual a 2 e testeModel não é nulo");


            //////////////////////////////////////////////////////////////////////

            testeModel.NumeroTeste = 4;
            if (testeModel?.NumeroTeste == 2)//Se testeModel for diferente de nulo e testeModel.NumeroTeste igual a 2
                Console.WriteLine("");

            else
                Console.WriteLine("testeModel não é nulo, mas NumeroTeste não é igual a 4");

            //////////////////////////////////////////////////////////////////////

            testeModel.NumeroTeste = 4;
            if (testeModel?.NumeroTeste != 2)//Se testeModel for diferente de nulo e testeModel.NumeroTeste diferente de 2
                Console.WriteLine("testeModel não é nulo, e NumeroTeste é diferente de 2");

            testeModel.NumeroTeste = null;

            if ((testeModel?.NumeroTeste ?? 0) == 0) //Se testeModel for diferente de nulo e testeModel.NumeroTeste igual a nulo ou 0
                Console.WriteLine("testeModel não é nulo, mas NumeroTeste é nulo ou com valor 0");

            if ((testeModel?.NumeroTeste ?? 0) != 0) //Se testeModel for diferente de nulo e testeModel.NumeroTeste diferente de nulo ou diferente de 0
                Console.WriteLine("");
            else
                Console.WriteLine("testeModel não é nulo, e NumeroTeste é nulo ou com valor 0 (else)");

            if (testeModel?.NumeroTeste == 0)
                Console.WriteLine("NumeroTeste não é nulo, e é diferente de 2");

            Console.WriteLine(result);
        }
        #endregion Strings

        #region Collections
        static void CollectionsForEachMaPratica()
        {
            List<Produto> produtos = CarregaProdutosMaPratica(1000000);
            List<Produto> produtosParaAdicionar = CarregaProdutosMaPratica(1000000);
            var inicio = DateTime.Now;
            foreach (var item in produtosParaAdicionar)
            {
                produtos.Add(item);
            }
            var fim = DateTime.Now;
            var total = (fim - inicio).TotalMilliseconds;
            Console.WriteLine($"{total} ms");

        }
        static void CollectionsForEachBoaPratica()
        {
            List<Produto> produtos = CarregaProdutosMaPratica(1000000);
            List<Produto> produtosParaAdicionar = CarregaProdutosMaPratica(1000000);
            var inicio = DateTime.Now;
            produtos.AddRange(produtosParaAdicionar);
            var fim = DateTime.Now;
            var total = (fim - inicio).TotalMilliseconds;
            Console.WriteLine($"{total} ms");

        }

        static void CarregaProdutosMaPratica()
        {
            var inicio = DateTime.Now;
            List<Produto> produtos = CarregaProdutosMaPratica(1000000);
            List<Produto> produtosParaAdicionar = CarregaProdutosMaPratica(1000000);


            var fim = DateTime.Now;
            var total = (fim - inicio).TotalMilliseconds;
            Console.WriteLine($"{total} ms");
        }
        static void CarregaProdutosBoaPratica()
        {
            var inicio = DateTime.Now;
            IEnumerable<Produto> produtos = CarregaProdutosBoaPratica(1000000).ToList();
            IEnumerable<Produto> produtosParaAdicionar = CarregaProdutosBoaPratica(1000000).ToList();


            var fim = DateTime.Now;
            var total = (fim - inicio).TotalMilliseconds;
            Console.WriteLine($"{total} ms");

        }

        static void ArrayInteriros()
        {
            List<Int32> li = new List<int>();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 10000; i++)
            {
                li.Add(i);
            }
            sw.Stop();
            Console.Write($"Usando um List<object> -> tempo gasto : { sw.ElapsedTicks }  \n");

            sw.Reset();
            sw.Start();
            Int32[] a = new Int32[10000];
            for (int i = 0; i < 10000; i++)
            {
                a[i] = i;
            }
            sw.Stop();
            Console.Write($"Usando Array de Inteiros (Integer Array)  -> tempo gasto : { sw.ElapsedTicks } \n");

        }

        private static List<Produto> CarregaProdutosMaPratica(int total)
        {
            var produtos = new List<Produto>();
            for (int i = 0; i < total; i++)
            {
                produtos.Add(new Produto()
                {
                    Id = i,
                    Nome = $"{i} livro asp.net",
                    Preco = 18.99M
                });
            }
            return produtos;
        }
        private static IEnumerable<Produto> CarregaProdutosBoaPratica(int total)
        {
            for (int i = 0; i < total; i++)
            {
                var categoria = i % 2 == 0 ? "Livros" : "Cursos";
                yield return new Produto()
                {
                    Id = i,
                    Categoria = categoria,
                    Ativo = true,
                    Nome = $"{i} livro asp.net",
                    Preco = 18.99M
                };
            }
        }

        private static void CountMaPratica()
        {
            var produtos = CarregaProdutosBoaPratica(100000).ToList();
            produtos[300].Ativo = true;
            var inicio = DateTime.Now;

            for (int i = 0; i < 1000; i++)
            {
                if (produtos.Count(a => a.Ativo == true) > 0)
                {
                    //manda email com os produtos com desconto
                }
            }

            var fim = DateTime.Now;
            var total = (fim - inicio).TotalMilliseconds;
            Console.WriteLine($"{total} ms");

        }

        private static void CountAnyBoaPratica()
        {
            var produtos = CarregaProdutosBoaPratica(100000).ToList();
            produtos[300].Ativo = true;
            var inicio = DateTime.Now;

            for (int i = 0; i < 1000; i++)
            {
                if (produtos.Any(a => a.Ativo == true))
                {
                    //manda email com os produtos com desconto
                }
            }

            var fim = DateTime.Now;
            var total = (fim - inicio).TotalMilliseconds;
            Console.WriteLine($"{total} ms");

        }

        private static void ValidaProdutosAtivos()
        {
            var produtos = CarregaProdutosBoaPratica(100000).ToList();
            Stopwatch sw = new Stopwatch();
            sw.Start();


            if (produtos.All(a => a.Ativo))
            {

            }


            sw.Stop();

            Console.WriteLine("Boa prática usando All");
            Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
            Console.WriteLine("*********************************");
            Console.WriteLine("");

            sw.Start();
            if (produtos.Count() == produtos.Count(a => a.Ativo))
            {

            }
            sw.Stop();

            Console.WriteLine("Má prática usando Count");
            Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
        }


        #endregion Collections


        #region Syntactic sugar

        static void SyntacticSugar()
        {
            //?? é para testar se a variável nomePrincipal não é nula (null)
            // e se caso for, então atribuir a variável nomeAlternativo para a variável nome.

            string nomePrincipal = null;
            string nomeAlternativo = "Eduardo";

            // se nomePrincipal é nulll, então nome é igual ao nomeAlternativo,
            // senão, nome é igual a nomePrincipal
            string nome = nomePrincipal ?? nomeAlternativo;
            Console.WriteLine($"Nome: {nome}");

            Console.WriteLine("");
            Console.WriteLine("");

            string nome1 = "          João Figues da Silva      ";
            string nome2 = "Marcos Roberto";
           
            string nome3 = nome1.Trim() ?? nome2;
            Console.WriteLine($"Nome 3: {nome3}");





        }
        static void SyntacticSugar2()
        {
            string nomePrincipal = null;
            string nomeAlternativo = null;
            string nomeQualquer = null;

            string nome = nomePrincipal ?? nomeAlternativo ?? nomeQualquer ?? "Sem Nome";

            // Resultado: Nome = "Sem Nome"
            Console.WriteLine($"Nome: {nome}");

            Console.WriteLine("");
            Console.WriteLine("");

            string nomePrincipal2 = null;
            string nomeAlternativo2 = "Arthur V. Pereira      ";
            string nomeQualquer2 = null;

            string nome2 = nomePrincipal2 ?? nomeAlternativo2 ?? nomeQualquer2 ?? "Sem Nome";

            // Resultado: Nome = "Sem Nome"
            Console.WriteLine($"Nome 2: {nome2}");

            int valor1 = 0;

            int valorResult = valor1 > 0 ? valor1: 10;

            // Resultado: Nome = "Sem Nome"
            Console.WriteLine($"Não é aplicado para os tipos int: {valorResult}");


        }


        #endregion Syntactic sugar

        public class TesteModel
        {
            public Nullable<int> NumeroTeste { get; set; }
        }



    }
}
