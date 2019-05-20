namespace SistemaEncuestas.LogicaNegocio.WebApiClient
{
    #region Namespace

    using Entidades.Entities;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    #endregion

    public class EncuestaClient
    {
        #region Attributes

        private const string GET_URI_RESPUESTA = "/api/encuesta/obtenerrespuestas";
        private const string GET_URI_RESPUESTA_ID = "/api/encuesta/obtenerrespuestasporid";
        private const string GET_URI_EMPRESA = "/api/encuesta/obtenerporidempresa";
        private const string GET_URI_EMPRESA_ENCUESTA = "/api/encuesta/obtenerporids";
        private const string GET_URI_ID = "/api/encuesta/obtenerporid";
        private const string POST_URI = "/api/encuesta/agregar";
        private const string PUT_URI = "/api/encuesta/editar";
        private const string DELETE_URI = "/api/encuesta/delete";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        #endregion

        #region Constructors

        public EncuestaClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        #endregion

        #region Public Methods

        public async Task<string> AddEncuestaAsync(Encuesta entity)
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

        public async Task<string> DeleteEncuestaByIdAsync(string idEncuesta)
        {
            string mensaje = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.DeleteAsync($"{DELETE_URI}/{idEncuesta}");

                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;
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

            return mensaje;
        }

        public async Task<string> EditEncuestaAsync(Encuesta entity)
        {
            string mensaje = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.PutAsync(PUT_URI, entity, new JsonMediaTypeFormatter());
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

        public async Task<Encuesta> GetEncuestaByIdAsync(string idEncuesta)
        {
            Encuesta encuesta = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_ID}/{idEncuesta}");
                string mensaje = string.Empty;
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        encuesta =await response.Content.ReadAsAsync<Encuesta>();
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

            return encuesta;
        }

        public async Task<List<Encuesta>> GetEncuestasByIdEmpresaAsync(string idEmpresa)
        {
            string mensaje = string.Empty;
            List<Encuesta> encuestas = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_EMPRESA}/{idEmpresa}");
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        encuestas = await response.Content.ReadAsAsync<List<Encuesta>>();
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

            return encuestas;
        }

        public async Task<Encuesta> GetEncuestaByIdEmpresaAndIdEncuestaAsync(string idEmpresa, string idEncuesta)
        {
            string mensaje = string.Empty;
            Encuesta encuesta = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_EMPRESA_ENCUESTA}/{idEmpresa}/{idEncuesta}");
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        encuesta = await response.Content.ReadAsAsync<Encuesta>();
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

            return encuesta;
        }

        public async Task<List<RespuestasEncuesta>> GetRespuestasPorEncuestaAsync(string idEncuesta)
        {
            string mensaje = string.Empty;
            List<RespuestasEncuesta> countRespuestas = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_RESPUESTA_ID}/{idEncuesta}");
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        countRespuestas = await response.Content.ReadAsAsync<List<RespuestasEncuesta>>();
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

            return countRespuestas;
        }

        #endregion
    }
}
