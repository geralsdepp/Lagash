using System;
using System.Collections.Generic;
using BajadorDeVideos.Common;
using HtmlAgilityPack;
using System.Net;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.Net.Security;
using System.IO;

namespace BajadorDeVideos.PluginEjemplo
{
    public class DummyPlugin : IPlugin
    {
        public List<Video> misVideos;
        string urlListado;
        string urlIframe;
        public DummyPlugin(string urlListado)
        {
            misVideos = new List<Video>();
            this.urlListado = urlListado;
        }
        public void Bajar()
        {
            Console.WriteLine("Bajar Videos");

            foreach (Video item in this.misVideos)
            {
                using(WebClient client = new WebClient())
                {
                    string htmlVideo = client.DownloadString("https://www.commoncraft.com"+item.Link);
                    HtmlDocument docVideo = new HtmlDocument();
                    docVideo.LoadHtml(htmlVideo);

                    foreach (HtmlNode node in docVideo.DocumentNode.SelectNodes("//iframe[@src]"))
                    {
                        urlIframe = node.GetAttributeValue("src","");
                    }

                    string htmlIframe = client.DownloadString("https:"+urlIframe);
                    Uri url = new Uri(urlIframe);
                    client.DownloadStringAsync(url);
                    JsonConvert.DeserializeObject()
                    Console.WriteLine(htmlIframe);
                    
                    client.DownloadStringTaskAsync(urlListado);
                    HtmlDocument docIframe = new HtmlDocument();
                    docIframe.LoadHtml(htmlIframe);
                    
                    /*foreach (HtmlNode node2 in docIframe.DocumentNode.SelectNodes("//video[@src]"))
                    {
                        foreach (HtmlNode node3 in node2.SelectNodes("//source[@src]"))
                        {
                            client.DownloadFile(node3.GetAttributeValue("src",""),@"C:\Users\geraldinem_lu\Documents\misVideos");
                        }
                        //"//video[@src]"
                        
                    }*/


                }
        }
    }

        public void ListarVideosDisponibles(string urlListado)
        {
            //Llamo a la pagina y obtengo el html generado

            using (WebClient client = new WebClient())
            {
                //Descargo mi html en un string
                string html = client.DownloadString(urlListado);
                //Instancio mi HtmlDocument(docuemento html)
                HtmlDocument doc = new HtmlDocument();
                //Le paso mi string html a cargar
                doc.LoadHtml(html);
                
                //ya puedo trabajar con el y utilizo la clase HtmlNode para acceder a mis etiquetas.
                //Recorro mi span
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='field-content']"))
                {
                    //Recorro mi a ref(dentro esta mi link y mi titulo)
                    foreach (HtmlNode node2 in node.SelectNodes(".//a[@href]"))
                    { 
                        //1. Extraigo mediante node2.InnerText el titulo del video.
                        //2. Extraigo mediante node2.GetAttributeValue el link pedido.
                        //3. Creo mi nuevo video y le paso los parametros correspondientes.
                        //4. Añado el video a mi lista "misVideos".   
                        if(node2.GetAttributeValue("href","") != "/join" && node2.InnerText != "Select")

                            this.misVideos.Add(new Video(node2.InnerText,node2.GetAttributeValue("href","")));    
                    }       
                }
                foreach (Video item in this.misVideos)
                {
                    Console.WriteLine("Titulo: {0}",item.Titulo);
                    Console.WriteLine("Link: {0}",item.Link);
                }
            }
        }       
    }
}
