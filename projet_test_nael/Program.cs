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
    using (var ctx = new MyDbContext())
    {
        Console.WriteLine("Ecrire le texte à insérer:");
        Text t = new();
        t.Content = Console.ReadLine();

        Console.WriteLine($"t id: {t.Id}");

        var restApi = new HttpClient();
        sendPostRequest(t);

        Console.WriteLine($"t id: {t.Id}");
        Console.WriteLine("Sauvegarde du texte:");
        Console.WriteLine("* - - -  " + t.Content + "  - - -");
    }
}

async Task Get_Bdd()
{
    //using (var ctx = new MyDbContext())
    //{
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
    //}
}

void Update_Bdd()
{
    using (var ctx = new MyDbContext())
    {
        Console.WriteLine("Donner l'Id du texte à modifier:");
        string? id = Console.ReadLine();
        if (id == null)
        {
            Console.WriteLine("No Id.");
            return;
        }
        string addr = $"https://localhost:7283/Text/api/Text/{id}";

        Console.WriteLine("Ecrire le nouveau texte:");
        string newText = Console.ReadLine();


        var restApi = new HttpClient();
        sendPutRequest(addr, newText);

        Console.WriteLine("Texte modifié!");
    }
}

HttpResponseMessage? sendGetRequest(string? addr)
{
    Uri uri = new($"{addr}");

    HttpRequestMessage request = new(HttpMethod.Get, uri);

    var response =  client.Send(request, CancellationToken.None);

    response.EnsureSuccessStatusCode();
    return response;   
}

async Task sendPostRequest(Text? toInsert)
{
    //try
    //{
        Uri uri = new("https://localhost:7283/Text/api/Text");
        HttpRequestMessage requestTest = new(HttpMethod.Post, uri);

        string json = JsonSerializer.Serialize(toInsert, jsonOptions);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(uri, content, CancellationToken.None);

        response.EnsureSuccessStatusCode();
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.ToString());
    //    throw;
    //}
}

async Task sendPutRequest(string addr, string toInsert)
{
    try
    {
        Uri uri = new(addr);
        HttpRequestMessage requestTest = new(HttpMethod.Post, uri);

        string json = JsonSerializer.Serialize(toInsert, jsonOptions);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync(uri, content, CancellationToken.None);

        response.EnsureSuccessStatusCode();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        throw;
    }
}