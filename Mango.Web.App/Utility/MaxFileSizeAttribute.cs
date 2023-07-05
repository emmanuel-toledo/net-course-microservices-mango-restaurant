using System.ComponentModel.DataAnnotations;

namespace Mango.Web.App.Utility
{
	/// <summary>
	/// This class attribute can help us to add custom validations to a class property that use this data annotation attribute.
	/// </summary>
	public class MaxFileSizeAttribute : ValidationAttribute
	{
		private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

		/// <summary>
		/// Check if the value of the property is valid. 
		/// <para>
		/// Check if the size of the file is lower or equal that the defined maximun file size (in this case will be 1MB).
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
				if (file.Length > (_maxFileSize * 1024 * 1024))
				{
					return new ValidationResult($"Maximun allowed file size is {_maxFileSize} MB.");
				}
			}
			return ValidationResult.Success;
		}
	}
}
