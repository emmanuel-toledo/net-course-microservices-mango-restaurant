﻿using Mango.Web.App.Utility;
using System.ComponentModel.DataAnnotations;

namespace Mango.Web.App.Models
{
    /// <summary>
    /// This class contains the deffinition of a product in the DB.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Get and set the unique identifier of a product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Get and set the name of a product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get and set the price of a product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Get and set the description of a product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get and set the category name of a product.
        /// </summary>
        public string CategoryName { get; set; }

		/// <summary>
		/// Get and set the image url of a product.
		/// </summary>
		public string? ImageUrl { get; set; }

		/// <summary>
		/// Get and set the image local path of a product.
		/// </summary>
		public string? ImageLocalPath { get; set; }

        /// <summary>
        /// Get and set the image form file.
        /// </summary>
        [MaxFileSizeAttribute(1)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
		public IFormFile? Image { get; set; }

		/// <summary>
		/// Get and set the count of a product.
		/// </summary>
		[Range(1, 100)]
        public int Count { get; set; } = 1;
    }
}
