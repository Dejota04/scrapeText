using HtmlAgilityPack;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ScrapeText 
{
    class Program
    {
        static void Main(String[] args)
        {
            char continuar;
            do
            {
                //Tema
                Console.Write("Tema: ");
                String tema = Console.ReadLine();

                //links
                Console.WriteLine("Urls: ");
                string links = Console.ReadLine();
                String[] url = links.Split(" ");
                Console.Clear();
                foreach (string i in url)
                {
                    //conexão com site da Folha
                    var httpClient = new HttpClient();
                    var html = httpClient.GetStringAsync(i).Result;
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    //data
                    var dataElement = htmlDocument.DocumentNode.SelectSingleNode("//time[@class='c-more-options__published-date']");
                    var data = "ao vivo";
                    if (dataElement == null)
                    {
                       
                    }
                    else
                    {
                         data = dataElement.InnerText.Trim();
                    }
                    



                    //titulo
                    var tituloElement = htmlDocument.DocumentNode.SelectSingleNode("//h1[@itemprop='headline']");
                    var titulo = tituloElement.InnerText.Trim();


                    //autor
                    var autorElement = htmlDocument.DocumentNode.SelectSingleNode("//strong[@class='c-signature__author']");
                    if (autorElement == null)
                    {
                        Console.WriteLine("FSP, " + data + ", " + titulo + ", " + tema);
                    }
                    else
                    {
                        var autor = autorElement.InnerText.Trim();
                        Console.WriteLine("FSP, " + data + ", " + titulo + ", " + autor + ", " + tema);
                    }



                    //Corpo
                    var corpoElement = htmlDocument.DocumentNode.SelectNodes("//div[@class='c-news__body']/p");
                    
                    Console.WriteLine(" ");
                    foreach (var node in corpoElement)
                    {
                        Console.WriteLine(node.InnerText.Trim());
                    }
                    Console.WriteLine(" ");
                    Console.WriteLine("-----------------------------------------------------------------");

                }

               continuar =  char.Parse(Console.ReadLine().ToUpper());
                Console.Clear();
            } while (continuar == 'S');
        }
    }
}

