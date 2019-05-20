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

    public class AdministradorClient
    {
        #region Attributes

        private const string POST_URI = "/api/administrador/validarcredenciales";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        #endregion

        #region Constructors

        public AdministradorClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        #endregion

        #region Public Methods

        public async Task<Administrador> ValidarAccesoAsync(Administrador entity)
        {
            Administrador administradorEntityResult = null;
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
                        administradorEntityResult = response.Content.ReadAsAsync<Administrador>().Result;
                        break;
                    case HttpStatusCode.InternalServerError:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;

                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;

                        throw new HttpRequestException(mensaje);
                    case HttpStatusCode.NotFound:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;

                        throw new MemberAccessException(mensaje);
                    case HttpStatusCode.Unauthorized:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;

                        throw new MemberAccessException(mensaje);
                }
            }

            return administradorEntityResult;
        }

        #endregion
    }
}
