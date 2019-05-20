namespace SistemaEncuestas.LogicaNegocio.WebApiClient
{
    #region Namespaces

    using Entidades.Entities;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    #endregion

    public class AuditoriaIngresoClient
    {
        #region Attributes

        private const string GET_URI = "/api/auditoriaingreso/obtener";
        private const string GET_URI_ID = "/api/auditoriaingreso/obtenerporid";
        private const string POST_URI = "/api/auditoriaingreso/agregar";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        #endregion

        #region Constructors

        public AuditoriaIngresoClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        #endregion

        #region Public Methods

        public async Task<string> AddItemPreguntaAsync(AuditoriaIngreso entity)
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
                        mensaje = data.Message;
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

        public async Task<List<AuditoriaIngreso>> GetAuditoriaIngresoAsync()
        {
            List<AuditoriaIngreso> auditoriasIngreso = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync(GET_URI);
                string mensaje = string.Empty;
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        auditoriasIngreso = response.Content.ReadAsAsync<List<AuditoriaIngreso>>().Result;
                        break;
                    case HttpStatusCode.NotFound:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new HttpRequestException(mensaje);
                }
            }

            return auditoriasIngreso;
        }

        public async Task<AuditoriaIngreso> GetAuditoriaIngresoByIdAsync(string idAuditoria)
        {
            AuditoriaIngreso auditoriaIngreso = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_ID}/{idAuditoria}");
                string mensaje = string.Empty;
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        auditoriaIngreso = response.Content.ReadAsAsync<List<AuditoriaIngreso>>().Result.FirstOrDefault();
                        break;
                    case HttpStatusCode.NotFound:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new HttpRequestException(mensaje);
                }
            }

            return auditoriaIngreso;
        }

        #endregion
    }
}
