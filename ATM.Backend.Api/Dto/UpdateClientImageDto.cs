using System.ComponentModel.DataAnnotations;

namespace ATM.Backend.Api.Dto;

public class UpdateClientImageDto
{
    [Required(ErrorMessage = "ProfileImage is required.")]
    public string ProfileImage { get; set; }
    
    /// <summary>
    /// Valida se a string é Base64 válida e se o tamanho não excede o limite.
    /// </summary>
    public bool IsValid(out string errorMessage)
    {
        errorMessage = string.Empty;
        
        // Verifica se a string não está vazia
        if (string.IsNullOrWhiteSpace(ProfileImage))
        {
            errorMessage = "ProfileImage cannot be empty.";
            return false;
        }
        
        // Valida formato Base64
        try
        {
            // Remove prefixo data:image se existir
            string base64String = ProfileImage;
            if (base64String.Contains(","))
            {
                base64String = base64String.Split(',')[1];
            }
            
            byte[] imageBytes = Convert.FromBase64String(base64String);
            
            // Valida tamanho máximo (500KB = 512000 bytes)
            const int maxSizeBytes = 512000;
            if (imageBytes.Length > maxSizeBytes)
            {
                errorMessage = $"Image size exceeds maximum allowed size of {maxSizeBytes / 1024}KB.";
                return false;
            }
        }
        catch (FormatException)
        {
            errorMessage = "Invalid Base64 format.";
            return false;
        }
        
        return true;
    }
}
