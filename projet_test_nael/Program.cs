using System;
using System.Text;
using System.Text.Json;
using projet_test_nael;
using projet_test_nael.Models;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();
JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);

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
        await Get_Bdd();
        break;
    case "U":
        Update_Bdd();
        break;
    case "fin":
        break;
}


void Insere_Bdd()
{
    var toInsert = new Text
    {
        Content = "To insert"
    };

    HttpRequestMessage requestTest = new(HttpMethod.Post, "url_here");

    string json = JsonSerializer.Serialize(toInsert, jsonOptions);

    requestTest.Content = new StringContent(json, Encoding.UTF8, "application/json");

    using (var ctx = new MyDbContext())
    {
        Console.WriteLine("Ecrire le texte à insérer:");
        Text t = new();
        t.Content = Console.ReadLine();

        Console.WriteLine($"t id: {t.Id}");

        var restApi = new HttpClient();
        ctx.Texts.Add(t);

        ctx.SaveChanges();

        Console.WriteLine($"t id: {t.Id}");
        Console.WriteLine("Sauvegarde du texte:");
        Console.WriteLine("* - - -  " + t.Content + "  - - -");
    }
}

async Task Get_Bdd()
{
    using (var ctx = new MyDbContext())
    {
        Console.WriteLine("Donner l'Id du texte à visualiser:");
        string? id = Console.ReadLine();
        if (id == null)
        {
            Console.WriteLine("No Id.");
            return;
        }

        string addr = $"https://localhost:7283/Text/api/Text/{id}";

        using var stream = await sendGetRequest(addr).Content.ReadAsStreamAsync(CancellationToken.None);

        Text? text = await JsonSerializer.DeserializeAsync<Text>(stream, jsonOptions, CancellationToken.None);

        if (text == null)
        {
            Console.WriteLine("text is null");
            return;
        }

        Console.WriteLine("Texte correspondant à l'Id:");
        Console.WriteLine("- - " + text.Content + " - -");
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

HttpResponseMessage? sendGetRequest(string? addr)
{
    //https://localhost:7283/Text/api/Text/
    Uri uri = new($"{addr}");

    HttpRequestMessage request = new(HttpMethod.Get, uri);

    var response =  client.Send(request, CancellationToken.None);

    response.EnsureSuccessStatusCode();
    return response;   
}

HttpResponseMessage? sendPostRequest(string? addr)
{
    //https://localhost:7283/Text/api/Text/
    Uri uri = new($"{addr}");

    HttpRequestMessage request = new(HttpMethod.Post, uri);

    var response = client.Send(request, CancellationToken.None);

    response.EnsureSuccessStatusCode();
    return response;
}