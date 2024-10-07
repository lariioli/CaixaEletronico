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
