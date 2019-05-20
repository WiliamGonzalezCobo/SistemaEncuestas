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

    public class PagoPlanClient
    {
        #region Attributes

        private const string GET_URI = "/api/pagoplan/obtener";
        private const string GET_URI_ID = "/api/pagoplan/obtenerporid";
        private const string POST_URI = "/api/pagoplan/agregar";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        #endregion

        #region Constructors

        public PagoPlanClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        #endregion

        #region Public Methods

        public async Task<List<PagoPlan>> GetPagoPlanAsync()
        {
            List<PagoPlan> pagosPlanes = null;

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
                        pagosPlanes = response.Content.ReadAsAsync<List<PagoPlan>>().Result;
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

            return pagosPlanes;
        }

        public async Task<PagoPlan> GetPagoPlanByIdAsync(string idPago)
        {
            PagoPlan pagoPlan = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_ID}/{idPago}");
                string mensaje = string.Empty;
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        pagoPlan = response.Content.ReadAsAsync<List<PagoPlan>>().Result.FirstOrDefault();
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

            return pagoPlan;
        }

        public async Task<string> AddPagoPlanAsync(PagoPlan entity)
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

        #endregion
    }
}
