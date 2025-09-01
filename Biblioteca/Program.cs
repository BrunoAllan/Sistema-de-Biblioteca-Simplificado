using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Aluno> alunos = new List<Aluno>();
    static List<Professor> professores = new List<Professor>();
    static Biblioteca biblioteca = new Biblioteca();

    static void Main()
    {
        Console.WriteLine("=== Bem-vindo à Biblioteca! ===");
        Console.WriteLine("1 - Cadastrar Aluno");
        Console.WriteLine("2 - Cadastrar Professor");
        Console.WriteLine("3 - Cadastrar Livro");
        Console.WriteLine("4 - Listar Livros Disponíveis");
        Console.WriteLine("5 - Realizar Empréstimo");
        Console.WriteLine("6 - Realizar Devolução");
        Console.WriteLine("7 - Exibir Alunos");
        Console.WriteLine("8 - Exibir Professores");
        Console.WriteLine("0 - Sair");

        switch (Console.ReadLine())
        {
            case "1": CadastrarAluno(); break;
            case "2": CadastrarProfessor(); break;
            case "3": CadastrarLivro(); break;
            case "4": biblioteca.ListarLivrosDisponiveis(); Continuar(); break;
            case "5": RealizarEmprestimo(); break;
            case "6": RealizarDevolucao(); break;
            case "7": ExibirAlunos(); break;
            case "8": ExibirProfessores(); break;
            case "0": Sair(); break;
            default:
                Console.WriteLine("Opção inválida");
                Continuar();
                break;
        }
    }

    static void CadastrarAluno()
    {
        Aluno aluno = new Aluno();
        Console.WriteLine("Digite o Nome do Aluno:");
        aluno.Nome = Console.ReadLine();
        Console.WriteLine("Digite a Idade do Aluno:");
        aluno.Idade = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite a Matrícula do Aluno:");
        aluno.Matricula = int.Parse(Console.ReadLine());

        alunos.Add(aluno);
        Console.WriteLine("Aluno cadastrado com sucesso!");
        Continuar();
    }

    static void CadastrarProfessor()
    {
        Professor prof = new Professor();
        Console.WriteLine("Digite o Nome do Professor:");
        prof.Nome = Console.ReadLine();
        Console.WriteLine("Digite a Idade do Professor:");
        prof.Idade = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite a Disciplina do Professor:");
        prof.Disciplina = Console.ReadLine();

        professores.Add(prof);
        Console.WriteLine("Professor cadastrado com sucesso!");
        Continuar();
    }

    static void CadastrarLivro()
    {
        Livro livro = new Livro();
        Console.WriteLine("Digite o Título do Livro:");
        livro.Titulo = Console.ReadLine();
        Console.WriteLine("Digite o Autor do Livro:");
        livro.Autor = Console.ReadLine();
        Console.WriteLine("Digite o Ano de Publicação:");
        livro.AnoPublicacao = int.Parse(Console.ReadLine());
        livro.Disponivel = true;

        biblioteca.AdicionarLivro(livro);
        Continuar();
    }

    static void ExibirAlunos()
    {
        Console.WriteLine("=== Lista de Alunos ===");
        foreach (var aluno in alunos)
        {
            aluno.ExibirInfo();
            Console.WriteLine("----------------------");
        }
        Continuar();
    }

    static void ExibirProfessores()
    {
        Console.WriteLine("=== Lista de Professores ===");
        foreach (var prof in professores)
        {
            prof.ExibirInfo();
            Console.WriteLine("----------------------");
        }
        Continuar();
    }

    static void RealizarEmprestimo()
    {
        if (biblioteca.Livros.Count == 0)
        {
            Console.WriteLine("Não há livros cadastrados.");
            Continuar();
            return;
        }

        Console.WriteLine("Digite o nome da pessoa que irá pegar o livro:");
        string nome = Console.ReadLine();
        Pessoa pessoa = alunos.Concat<Pessoa>(professores).FirstOrDefault(p => p.Nome == nome);

        if (pessoa == null)
        {
            Console.WriteLine("Pessoa não encontrada.");
            Continuar();
            return;
        }

        Console.WriteLine("Digite o título do livro a ser emprestado:");
        string titulo = Console.ReadLine();
        Livro livro = biblioteca.Livros.FirstOrDefault(l => l.Titulo == titulo);

        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado.");
        }
        else
        {
            biblioteca.RegistrarEmprestimo(pessoa, livro);
        }

        Continuar();
    }

    static void RealizarDevolucao()
    {
        Console.WriteLine("Digite o nome da pessoa que irá devolver o livro:");
        string nome = Console.ReadLine();
        Pessoa pessoa = alunos.Concat<Pessoa>(professores).FirstOrDefault(p => p.Nome == nome);

        if (pessoa == null)
        {
            Console.WriteLine("Pessoa não encontrada.");
            Continuar();
            return;
        }

        Console.WriteLine("Digite o título do livro a ser devolvido:");
        string titulo = Console.ReadLine();
        Livro livro = biblioteca.Livros.FirstOrDefault(l => l.Titulo == titulo);

        if (livro == null)
        {
            Console.WriteLine("Livro não encontrado.");
        }
        else
        {
            biblioteca.RegistrarDevolucao(pessoa, livro);
        }

        Continuar();
    }

    static void Sair()
    {
        Console.WriteLine("Saindo...");
        Environment.Exit(0);
    }

    static void Continuar()
    {
        Console.WriteLine("Clique para continuar...");
        Console.ReadKey();
        Console.Clear();
        Main();
    }
}

// ======================== CLASSES ========================

class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnoPublicacao { get; set; }
    public bool Disponivel { get; set; }

    public void Emprestar() => Disponivel = false;
    public void Devolver() => Disponivel = true;
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
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Idade: {Idade}");
        Console.WriteLine($"Matrícula: {Matricula}");
    }
}

class Professor : Pessoa
{
    public string Disciplina { get; set; }
    public override void ExibirInfo()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Idade: {Idade}");
        Console.WriteLine($"Disciplina: {Disciplina}");
    }
}

class Emprestimo
{
    public Livro livro { get; set; }
    public Pessoa pessoa { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucao { get; set; }

    public void RegistrarEmprestimo()
    {
        if (livro.Disponivel)
        {
            livro.Emprestar();
            DataEmprestimo = DateTime.Now;
            Console.WriteLine($"Empréstimo registrado para {pessoa.Nome} no livro {livro.Titulo}.");
        }
        else
        {
            Console.WriteLine($"O livro {livro.Titulo} não está disponível.");
        }
    }

    public void RegistrarDevolucao()
    {
        if (!livro.Disponivel)
        {
            livro.Devolver();
            DataDevolucao = DateTime.Now;
            Console.WriteLine($"Devolução registrada para {pessoa.Nome} no livro {livro.Titulo}.");
        }
        else
        {
            Console.WriteLine($"O livro {livro.Titulo} já estava disponível.");
        }
    }
}

class Biblioteca
{
    public List<Livro> Livros { get; set; } = new List<Livro>();
    private List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();

    public void AdicionarLivro(Livro l)
    {
        Livros.Add(l);
        Console.WriteLine($"Livro '{l.Titulo}' adicionado à biblioteca.");
    }

    public void ListarLivrosDisponiveis()
    {
        Console.WriteLine("------------- Livros disponíveis -------------");
        foreach (var livro in Livros)
        {
            if (livro.Disponivel)
                Console.WriteLine($"{livro.Titulo} - {livro.Autor} ({livro.AnoPublicacao})");
        }
    }

    public void RegistrarEmprestimo(Pessoa p, Livro l)
    {
        if (!l.Disponivel)
        {
            Console.WriteLine($"O livro '{l.Titulo}' não está disponível.");
            return;
        }

        Emprestimo e = new Emprestimo { livro = l, pessoa = p };
        e.RegistrarEmprestimo();
        Emprestimos.Add(e);
    }

    public void RegistrarDevolucao(Pessoa p, Livro l)
    {
        var emprestimo = Emprestimos.FirstOrDefault(e => e.livro == l && e.pessoa == p && !l.Disponivel);
        if (emprestimo != null)
            emprestimo.RegistrarDevolucao();
        else
            Console.WriteLine($"Nenhum empréstimo ativo encontrado para {p.Nome} com o livro '{l.Titulo}'.");
    }
}
