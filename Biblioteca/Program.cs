﻿class Program
{
    static void Main()
    {
        Console.WriteLine("");

        switch (Console.ReadLine())
        {
            case "1": CadastrarAluno(); break;
            case "2": CadastrarProfessor(); break;
            case "3": ExibirAluno(); break;
            case "0": sair(); break;
            default:
                Console.WriteLine("Opção inválida");
                continuar();
                break;
        }
    }
        static void CadastrarAluno()
        {
            Aluno aluno1 = new Aluno();
            Console.WriteLine("Digite o Nome do Aluno");
            aluno1.Nome = Console.ReadLine();
            Console.WriteLine("Digite a Idade do Aluno");
            aluno1.Idade = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a Matrícula do Aluno");
            aluno1.Matricula = int.Parse(Console.ReadLine());
        }

        static void CadastrarProfessor()
        {
            Professor prof1 = new Professor();
            Console.WriteLine("Digite o Nome do Professor");
            prof1.Nome = Console.ReadLine();
            Console.WriteLine("Digite a Idade do Professor");
            prof1.Idade = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a Disciplina do Professor");
            prof1.Disciplina = Console.ReadLine();
        }

        static void ExibirAluno()
        {
            Console.WriteLine("=== Aluno ===");
            aluno1.ExibirInfo();
        }

        static void ExibirProfessor()
        {
            Console.WriteLine("=== Professor ===");
            prof1.ExibirInfo();
        }

        class Livro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }
        public bool Disponivel { get; private set; } = true;

        public void Emprestar()
        {
            if (Disponivel)
            {
                Disponivel = false;
                Console.WriteLine($"{Titulo} foi emprestado.");
            }
            else
            {
                Console.WriteLine($"{Titulo} já está emprestado.");
            }
        }

        public void Devolver()
        {
            if (!Disponivel)
            {
                Disponivel = true;
                Console.WriteLine($"{Titulo} foi devolvido.");
            }
            else
            {
                Console.WriteLine($"{Titulo} já estava disponível.");
            }
        }
    }

        abstract class Pessoa
        {
            public string Nome { get; set; }
            public int Idade { get; set; }

            public abstract void ExibirInfo();
        }

        class Aluno : Pessoa
        {
            public int Matricula { get; set; }

            public override void ExibirInfo()
            {
                Console.WriteLine(Nome);
                Console.WriteLine(Idade);
                Console.WriteLine(Matricula);
            }
        }

        class Professor : Pessoa
        {
            public string Disciplina { get; set; }

            public override void ExibirInfo()
            {
                Console.WriteLine(Nome);
                Console.WriteLine(Idade);
                Console.WriteLine(Disciplina);
            }
        }

    
    static void sair()
    {
        Console.WriteLine("Saindo...");
        Environment.Exit(0);
    }
    static void continuar()
    {
        Console.WriteLine("Clique para continuar...");
        Console.ReadKey();
        Console.Clear();
        Main();
    }
}