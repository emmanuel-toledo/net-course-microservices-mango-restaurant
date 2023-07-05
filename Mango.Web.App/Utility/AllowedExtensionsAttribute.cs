using System.ComponentModel.DataAnnotations;

namespace Mango.Web.App.Utility
{
	/// <summary>
	/// This class attribute can help us to add custom validations to a class property that use this data annotation attribute.
	/// </summary>
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

		/// <summary>
		/// Check if the value of the property is valid. 
		/// <para>
		/// Check if the extension of the selected file is one of the extensions collection.
		/// </para>
		/// </summary>
		/// <param name="value">Property's value.</param>
		/// <param name="validationContext">Validation context.</param>
		/// <returns>Validation Result.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile; 
			if (file != null) 
			{
				var extension = Path.GetExtension(file.FileName);
				if (!_extensions.Contains(extension.ToLower()))
				{
					// If the extension is not one of the extensions collection we throw an error.
					return new ValidationResult("This photo extension is not allowed!");
				}
			}
			return ValidationResult.Success;
		}
	}
}
