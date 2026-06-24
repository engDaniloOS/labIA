namespace Comum.Models
{
    public record DietaRefeicao(string NomeRefeicao, string DescricaRefeicao, MacroRefeicao MacroRefeicao);

    public record MacroRefeicao(int Calorias, int GramasProteina, int GramasLipidios, int GramasCarboidratos);
}
