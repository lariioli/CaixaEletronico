using System;
using System.IO;

class Program
{ 
    static void Main(string[] args)
    {
        Console.Write("Digite o nome do titular da conta: ");
        string titular = Console.ReadLine();

        Console.Write("Digite o tipo de conta (Corrente/Poupança): ");
        string tipoConta = Console.ReadLine();

        Console.Write("Crie uma senha para sua conta: ");
        string senha = Console.ReadLine();

        Console.Write("Digite o número da conta: ");
        string numeroConta = Console.ReadLine();

        Conta conta = new Conta(titular, tipoConta, senha, numeroConta);

        Console.Write("Digite a senha para acessar a conta: ");
        string senhaInput = Console.ReadLine();

         if (!conta.Autenticar(senhaInput))
        {
            Console.WriteLine("Senha incorreta. Acesso negado.");
            return;
        }

        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("Bem-vindo ao Caixa Eletrônico!");
            Console.WriteLine("1. Saque");
            Console.WriteLine("2. Depósito");
            Console.WriteLine("3. Extrato");
            Console.WriteLine("4. Transferência");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                Console.Write("Valor do saque: ");
                double valorSaque = double.Parse(Console.ReadLine());
                conta.Sacar(valorSaque);
                break;
                case 2:
                Console.Write("Valor do depósito: ");
                double valorDeposito = double.Parse(Console.ReadLine());
                conta.Depositar(valorDeposito);
                break;
                case 3:
                conta.ExibirExtrato();
                conta.SalvarExtratoEmArquivo();
                break;
                case 4:
                Console.Write("Digite o nome do destinatário: ");
                string destinatario = Console.ReadLine();
                Console.Write("Digite o valor da transferência: ");
                double valorTransferencia = double.Parse(Console.ReadLine());
                Conta contaDestino = new Conta(destinatario, "Corrente", "0000", "0002");
                conta.Transferir(contaDestino, valorTransferencia);
                break;
                case 5:
                continuar = false;
                Console.WriteLine("Saindo...");
                break;
                default:
                Console.WriteLine("Opção inválida.");
                break;
            }
             if (continuar)
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
class Conta
{
    private string titular;
    private string tipoConta;
    private double saldo;
    private string senha;
    private string numeroConta;

    public Conta(string titular, string tipoConta, string senha, string numeroConta)
    {
    this.titular = titular;
    this.tipoConta = tipoConta;
    this.saldo = 0;
    this.senha = senha;
    this.numeroConta = numeroConta;
    }
     public bool Autenticar(string senhaInput)
    {
        return senhaInput == senha;
    }

    public void Sacar(double valor)
    {
        if (valor > saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
        }
        else if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo -= valor;
            Console.WriteLine($"Saque de {valor:C} realizado com sucesso.");
        }
    }

    public void Depositar(double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
         {
            saldo += valor;
            Console.WriteLine($"Depósito de {valor:C} realizado com sucesso.");
        }
    }

    public void ExibirExtrato()
    {
        Console.WriteLine($"Titular: {titular}");
        Console.WriteLine($"Tipo de Conta: {tipoConta}");
        Console.WriteLine($"Número da Conta: {numeroConta}");
        Console.WriteLine($"Saldo Atual: {saldo:C}");
    }
    public void Transferir(Conta destino, double valor)
    {
        if (valor > saldo)
        {
            Console.WriteLine("Saldo insuficiente para transferência.");
        }
        else if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo -= valor;
            destino.Depositar(valor);
            Console.WriteLine($"Transferência de {valor:C} para {destino.titular} realizada com sucesso.");
        }
    }