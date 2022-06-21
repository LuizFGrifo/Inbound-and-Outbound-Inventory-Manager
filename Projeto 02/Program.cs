﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto_02
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;

        }

        static List<Cliente> clientes = new List<Cliente>();
        enum Menu { Listagem = 1,Adicionar,Remover,Sair }

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {

                Console.WriteLine("Sistema de clientes - Bem vindo(a)!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {

                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }

        static void Adicionar()
        {

            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro do cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("O cadastro foi realizado com sucesso ! Aperte ENTER para sair. ");
            Console.ReadLine();

        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID que deseja remover:");
            int id = int.Parse (Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("O ID digitado não existe, tente novamente! ");
                Console.ReadLine();
            }
        }

        static void Listagem()
        {

            if(clientes.Count > 0) // Se tem pelo menos um cliente
            {
                Console.WriteLine("Lista de clienes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("==================================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Não tem nenhum cliente cadastrado!");
            }

            Console.WriteLine("Aperte ENTER para sair:");
            Console.ReadLine();

        }
        static void Salvar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch(Exception e)
            {
                clientes = new List<Cliente>();
            }
            
            stream.Close();

        }

    }
}
