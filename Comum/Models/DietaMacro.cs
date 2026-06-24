namespace Comum.Models
{
    public record DietaMacro(
        int TaxaMetabolicaBasal, 
        int CaloriasDieta, 
        int GramasProteina, 
        int GramasLipidios, 
        int GramasCarboidratos
    );
}
