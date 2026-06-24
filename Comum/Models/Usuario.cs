namespace Comum.Models
{
    public class Usuario
    {
        public string? Genero { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
        public double BodyFat { get; set; }
        public int DiasExercicios { get; set; }
        public string? Objetivo { get; set; }

        public override string ToString()
        {
            return $@"Genero {Genero}, {Altura}m, {Peso}Kg, {BodyFat}% de BF. 
                    Pratica exercicios {DiasExercicios} vezes por semana. Objetivo: {Objetivo}";
        }

        public static Usuario GetUsuarioTeste()
        {
            return new Usuario
            {
                Genero = "Masculino",
                Altura = 1.75,
                Peso = 80,
                BodyFat = 20,
                DiasExercicios = 3,
                Objetivo = "Perder peso"
            };
        }
    }
}