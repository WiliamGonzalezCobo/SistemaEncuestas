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

    public class PlanEncuestaClient
    {
        #region Attributes

        private const string GET_URI = "/api/plan/obtener";
        private const string GET_URI_ID = "/api/plan/obtenerporid";
        private const string POST_URI = "/api/plan/agregar";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        #endregion

        #region Constructors

        public PlanEncuestaClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        #endregion

        #region Public Methods

        public async Task<List<PlanEncuesta>> GetPlanesAsync()
        {
            List<PlanEncuesta> planes = null;

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
                        planes = await response.Content.ReadAsAsync<List<PlanEncuesta>>();
                        break;
                    case HttpStatusCode.NotFound:
                        data = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                        mensaje = data.Message;
                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                        mensaje = data.Message;
                        throw new HttpRequestException(mensaje);
                }
            }

            return planes;
        }

        public async Task<PlanEncuesta> GetPlanByIdAsync(string idPlan)
        {
            PlanEncuesta plan = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_ID}/{idPlan}");
                string mensaje = string.Empty;
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        plan = response.Content.ReadAsAsync<PlanEncuesta>().Result;
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

            return plan;
        }

        public async Task<string> AddPlanAsync(PlanEncuesta entity)
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
