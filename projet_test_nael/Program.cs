using System;
using projet_test_nael;
using projet_test_nael.Models;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//HttpClient client = new HttpClient();

//client.SendAsync();

Console.WriteLine("Action à réaliser (I/G/U) :");
String act = Console.ReadLine();
if (act == null)
{
    Console.WriteLine("Aucune action saisie");
    return;
}
switch (act)
{
    case "I":
        Insere_Bdd();
        break;
    case "G":
        Get_Bdd();
        break;
    case "U":
        Update_Bdd();
        break;
    case "fin":
        break;
}



/*using (var ctx = new MyDbContext())
{
    Text? text = ctx.Set<Text>().Find(1);

    var query = ctx.Texts.ToList();

    if (text == null)
    {
        Console.WriteLine("Text not found");
        return;
    }

    text.Content = "Hello, World! From Nael";

    //Console.WriteLine($"t id: {t.Id}");

    //ctx.Texts.Add(t);

    ctx.SaveChanges();

    //Console.WriteLine($"t id: {t.Id}");
}*/

static void Insere_Bdd()
{
    using (var ctx = new MyDbContext())
    {
        Console.WriteLine("Ecrire le texte à insérer:");
        Text t = new();
        t.Content = Console.ReadLine();

        Console.WriteLine($"t id: {t.Id}");

        ctx.Texts.Add(t);

        ctx.SaveChanges();

        Console.WriteLine($"t id: {t.Id}");
        Console.WriteLine("Sauvegarde du texte:");
        Console.WriteLine("* - - -  " + t.Content + "  - - -");
    }
}

static void Get_Bdd()
{
    using (var ctx = new MyDbContext())
    {
        Console.WriteLine("Donner l'Id du texte à visualiser:");
        string? i1 = Console.ReadLine();
        if (i1 == null)
        {
            Console.WriteLine("No Id.");
            return;
        }
        int i2 = int.Parse(i1);
        Text? text = ctx.Set<Text>().Find(i2);
        if (text == null)
        {
            Console.WriteLine("Text not found");
            return;
        }
        Console.WriteLine("Texte correspondant à l'Id:");
        Console.WriteLine("- - "+text.Content+" - -");
    }
}

static void Update_Bdd()
{
    using (var ctx = new MyDbContext())
    {
        Console.WriteLine("Donner l'Id du texte à modifier:");
        string? i1 = Console.ReadLine();
        if (i1 == null)
        {
            Console.WriteLine("No Id.");
            return;
        }
        int i2 = int.Parse(i1);
        Text? text = ctx.Set<Text>().Find(i2);
        if (text == null)
        {
            Console.WriteLine("Text not found");
            return;
        }
        Console.WriteLine("Nouveau texte:");
        text.Content = Console.ReadLine();
        ctx.SaveChanges();
        Console.WriteLine("Texte modifié!");
    }
}