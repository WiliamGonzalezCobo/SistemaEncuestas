namespace SistemaEncuestas.Utilidades.Serializer
{
    #region Namespaces

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    #endregion

    public class Json<T> where T : class
    {
        #region Public Methods

        public static List<T> JSONStringToList(string JSONSerializer)
        {
            return JsonConvert.DeserializeObject<List<T>>(JSONSerializer);
        }

        public static T JSONStringToObject(string JSONSerializer)
        {
            return JsonConvert.DeserializeObject<T>(JSONSerializer);
        }

        public static string ObjectToJSON(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public static string ListObjectToJSON(List<T> listEntity)
        {
            return JsonConvert.SerializeObject(listEntity);
        }

        #endregion
    }
}
