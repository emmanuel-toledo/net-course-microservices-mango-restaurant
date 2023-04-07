namespace Mango.Web.App.Models
{
    /// <summary>
    /// Modelo que servirá como respuesta para peticiones HTTP del servicio de Productos.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Bandera para saber si la respuesta es exitosa o no.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Resultado de la respuesta (Tipo object ya que pudieramos enviar más de un tipo de objeto).
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// Mensaje general de la respuesta.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Lista de errores especificos de la respuesta.
        /// </summary>
        public List<string> ErrorMessages { get; set; }
    }
}
