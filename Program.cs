using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Criar conta com dados fornecidos pelo usuário
        Console.Write("Digite o nome do titular da conta: ");
        string titular = Console.ReadLine();

        Console.Write("Digite o tipo de conta (Corrente/Poupança): ");
        string tipoConta = Console.ReadLine();

        string senha;
        do
        {
            Console.Write("Crie uma senha para sua conta (mínimo 6 dígitos): ");
            senha = Console.ReadLine();
        } while (senha.Length < 6);

        Console.Write("Digite o número da conta: ");
        string numeroConta = Console.ReadLine();

        Console.Write("Informe o limite da conta: ");
        double limite = double.Parse(Console.ReadLine());

        Conta conta = new Conta(titular, tipoConta, senha, numeroConta, limite);

        // Solicitar senha para acesso
        Console.Write("Digite a senha para acessar a conta: ");
        string senhaInput = Console.ReadLine();

        // Verificar autenticação
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
            Console.WriteLine("5. Aplicações Financeiras");
            Console.WriteLine("6. Sair");
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
                    Conta contaDestino = new Conta(destinatario, "Corrente", "0000", "0002", 0);
                    conta.Transferir(contaDestino, valorTransferencia);
                    break;
                case 5:
                    Console.WriteLine("Aplicações financeiras:");
                    Console.WriteLine("1. Poupança");
                    Console.WriteLine("2. CDB");
                    int tipoAplicacao = int.Parse(Console.ReadLine());
                    Console.Write("Digite o valor da aplicação: ");
                    double valorAplicacao = double.Parse(Console.ReadLine());
                    conta.AplicacaoFinanceira(tipoAplicacao, valorAplicacao);
                    break;
                case 6:
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
    private double limite;
    private string senha;
    private string numeroConta;

    public Conta(string titular, string tipoConta, string senha, string numeroConta, double limite)
    {
        this.titular = titular;
        this.tipoConta = tipoConta;
        this.saldo = 0;
        this.senha = senha;
        this.numeroConta = numeroConta;
        this.limite = limite;
    }

    public bool Autenticar(string senhaInput)
    {
        return senhaInput == senha;
    }

    public void Sacar(double valor)
    {
        if (valor > saldo + limite)
        {
            Console.WriteLine("Saldo insuficiente e limite excedido.");
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
        Console.WriteLine($"Limite: {limite:C}");
    }

    public void Transferir(Conta destino, double valor)
    {
        double taxa = valor * 0.0005;
        double totalTransferencia = valor + taxa;

        if (totalTransferencia > saldo + limite)
        {
            Console.WriteLine("Saldo insuficiente para transferência.");
        }
        else if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo -= totalTransferencia;
            destino.Depositar(valor);
            Console.WriteLine($"Transferência de {valor:C} para {destino.titular} realizada com sucesso.");
            Console.WriteLine($"Taxa de {taxa:C} cobrada.");
        }
    }

    public void AplicacaoFinanceira(int tipo, double valor)
    {
        if (valor > saldo)
        {
            Console.WriteLine("Saldo insuficiente para aplicação.");
        }
        else if (tipo == 1)
        {
            saldo -= valor;
            double rendimento = valor * 0.005; // Simulando rendimento de 0,5% ao mês
            saldo += rendimento;
            Console.WriteLine($"Aplicação de {valor:C} na Poupança realizada. Rendimento: {rendimento:C}");
        }
        else if (tipo == 2)
        {
            saldo -= valor;
            double rendimento = valor * 0.01; // Simulando rendimento de 1% ao mês no CDB
            saldo += rendimento;
            Console.WriteLine($"Aplicação de {valor:C} no CDB realizada. Rendimento: {rendimento:C}");
        }
        else
        {
            Console.WriteLine("Tipo de aplicação inválida.");
        }
    }

    public void SalvarExtratoEmArquivo()
    {
        string nomeArquivo = $"{titular}_extrato.txt";
        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            writer.WriteLine($"Titular: {titular}");
            writer.WriteLine($"Tipo de Conta: {tipoConta}");
            writer.WriteLine($"Número da Conta: {numeroConta}");
            writer.WriteLine($"Saldo Atual: {saldo:C}");
            writer.WriteLine($"Limite: {limite:C}");
            writer.WriteLine($"Data: {DateTime.Now:dd/MM/yyyy}");
            writer.WriteLine($"Hora: {DateTime.Now:HH:mm:ss}");
        }
        Console.WriteLine($"Extrato salvo em: {nomeArquivo}");
    }
}