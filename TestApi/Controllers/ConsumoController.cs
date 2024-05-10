using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestApi.NewFolder;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("Consumo")]
    public class ConsumoController : Controller
    {
        
        [HttpGet]
        [Route("ObtenerObjeto")]
        public async Task<dynamic> getInformacion()
        {
            List<objRecortado> lista = new List<objRecortado>();
            int itemcount = lista.Count;

            var client = new HttpClient();
            

            while (itemcount < 25)
            {

                var respuesta = await client.GetAsync("https://api.chucknorris.io/jokes/random");
                var objeto = respuesta.Content.ReadAsStringAsync();

                var result = objeto.Result;
                var json = JsonConvert.DeserializeObject<ObjetoApi>(result);


                objRecortado obj = lista.FirstOrDefault(x => x.id == json.id);

                if (obj == null)
                {
                    lista.Add(new objRecortado
                    {
                        id = json.id,
                        url = json.url,
                        value = json.value
                    });
                }

                itemcount = lista.Count;
            }

           

            return lista;
        }
    }
}
