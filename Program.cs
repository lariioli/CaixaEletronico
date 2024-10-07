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
            
