namespace RagChat
{
    public static class TextChunker
    {
        public static List<string> Split(string texto, int tamanhoMaximo = 400, int sobreposicao = 60)
        {
            var paragrafos = texto
                .Split(["\r\n\r\n", "\n\n"], StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Where(p => p.Length > 0);

            var chunks = new List<string>();

            foreach (var paragrafo in paragrafos)
                chunks.AddRange(SplitPorTamanho(paragrafo, tamanhoMaximo, sobreposicao));

            return chunks;
        }

        private static IEnumerable<string> SplitPorTamanho(string texto, int tamanhoMaximo, int sobreposicao)
        {
            if (texto.Length <= tamanhoMaximo)
            {
                yield return texto;
                yield break;
            }

            var inicio = 0;
            while (inicio < texto.Length)
            {
                var tamanho = Math.Min(tamanhoMaximo, texto.Length - inicio);
                yield return texto.Substring(inicio, tamanho);

                if (inicio + tamanho >= texto.Length)
                    yield break;

                inicio += tamanhoMaximo - sobreposicao;
            }
        }
    }
}
