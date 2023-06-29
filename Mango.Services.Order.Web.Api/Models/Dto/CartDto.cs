namespace Mango.Services.Order.Web.Api.Models.Dto
{
    /// <summary>
    /// This class represents a cart object with all the details.
    /// </summary>
    public class CartDto
    {
        /// <summary>
        /// Get and set cart header object.
        /// </summary>
        public CartHeaderDto CartHeader { get; set; }

        /// <summary>
        /// Get and set cart details collection.
        /// </summary>
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
