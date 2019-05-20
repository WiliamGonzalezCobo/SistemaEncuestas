namespace SistemaEncuestas.LogicaNegocio.WebApiClient
{
    #region Namespaces

    using Entidades.Entities;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    #endregion

    public class RespuestaClient
    {
        #region Attributes

        private const string POST_URI = "/api/respuesta/agregar";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        #endregion

        #region Constructors

        public RespuestaClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        #endregion

        #region Public Methods

        public async Task<string> AddRespuestaAsync(Respuesta entity)
        {
            string mensaje = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.PostAsync(POST_URI, entity, new JsonMediaTypeFormatter());
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;
                        break;
                    case HttpStatusCode.InternalServerError:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new HttpRequestException(mensaje);
                }
            }

            return mensaje;
        }

        #endregion
    }
}
